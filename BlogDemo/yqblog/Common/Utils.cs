using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;

namespace Common
{
    public class Utils
    {
        public static string GetFileSource(string path)
        {
            try
            {
                using (var myFile = new StreamReader(HttpContext.Current.Server.MapPath(path), Encoding.Default))
                {
                    return myFile.ReadToEnd();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetJson<T>(T obj)
        {
            var json = new DataContractJsonSerializer(obj.GetType());
            using (var stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);
                string szJson = Encoding.UTF8.GetString(stream.ToArray());
                return szJson;
            }
        }

        public static T ParseFromJson<T>(string szJson)
        {
            var obj = Activator.CreateInstance<T>();
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                var serializer = new DataContractJsonSerializer(obj.GetType());
                return (T) serializer.ReadObject(ms);
            }
        }

        public static bool FileExists(string filename)
        {
            return File.Exists(filename);
        }

        public static string FileterStr(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                str = Regex.Replace(str, @"(<script>)", "&lt;script&gt;", RegexOptions.IgnoreCase);
                str = Regex.Replace(str, @"(</script>)", "&lt;/script&gt;", RegexOptions.IgnoreCase);
                str = Regex.Replace(str, @"(&nbsp;){4,}", "&nbsp;&nbsp;&nbsp;&nbsp;", RegexOptions.IgnoreCase);
                str = Regex.Replace(str, @"(<br>){1,}|(<br/>){1,}", "<br>", RegexOptions.IgnoreCase);
                str = Regex.Replace(str, @"(<br>){1,}", "<br>", RegexOptions.IgnoreCase);
            }
            else
            {
                str = string.Empty;
            }
            return str;
        }

        public static string FileterScript(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                str = Regex.Replace(str, @"(<script>)", "&lt;script&gt;", RegexOptions.IgnoreCase);
                str = Regex.Replace(str, @"(</script>)", "&lt;/script&gt;", RegexOptions.IgnoreCase);
            }
            else
            {
                str = "";
            }
            return str;
        }

        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            strPath = strPath.Replace("/", "\\").Replace("~", "").TrimStart('\\');
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }

        public static string GetTrueWebPath()
        {
            string webPath = HttpContext.Current.Request.Path;
            webPath = webPath.LastIndexOf("/", StringComparison.Ordinal) !=
                      webPath.IndexOf("/", StringComparison.Ordinal)
                          ? webPath.Substring(webPath.IndexOf("/", StringComparison.Ordinal),
                                              webPath.LastIndexOf("/", StringComparison.Ordinal) + 1)
                          : "/";

            return webPath;
        }

        public static string GetRootUrl(string forumPath)
        {
            int port = HttpContext.Current.Request.Url.Port;
            return string.Format("{0}://{1}{2}{3}",
                                 HttpContext.Current.Request.Url.Scheme,
                                 HttpContext.Current.Request.Url.Host,
                                 (port == 80 || port == 0) ? "" : ":" + port,
                                 forumPath);
        }

        public static string CleanInvalidFileName(string fileName)
        {
            fileName = fileName + "";
            fileName = InvalidFileNameChars.Aggregate(fileName, (current, c) => current.Replace(c + "", ""));

            if (fileName.Length > 1)
                if (fileName[0] == '.')
                    fileName = "dot" + fileName.TrimStart('.');

            return fileName;
        }

        public static string DownLoadImg(string url, string folderPath, bool isoldname = false)
        {
            try
            {
                string fileName = url.Substring(url.LastIndexOf("/", StringComparison.Ordinal) + 1);
                if (fileName.IndexOf("?", StringComparison.Ordinal) > -1)
                {
                    fileName = fileName.Substring(0, fileName.IndexOf("?", StringComparison.Ordinal));
                }
                fileName = CleanInvalidFileName(fileName);
                if (!isoldname)
                {
                    string extension = fileName.Split('.')[1];
                    fileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + "." +
                               extension;
                }
                string fileFolder = HttpContext.Current.Server.MapPath(folderPath);
                DownloadImage(url, fileFolder + fileName);
                return folderPath.Trim('~') + fileName;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string RefreshImageUrl(string htmlContent, string webUrl)
        {
            string newContent = htmlContent;
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);
            HtmlNodeCollection imgs = doc.DocumentNode.SelectNodes("//img");
            if (imgs != null && imgs.Count > 0)
            {
                foreach (HtmlNode child in imgs)
                {
                    if (child.Attributes["src"] == null)
                    {
                        continue;
                    }

                    string imgurl = child.Attributes["src"].Value;

                    if (imgurl.IndexOf(webUrl, StringComparison.Ordinal) > -1 ||
                        imgurl.IndexOf("http://", StringComparison.Ordinal) > -1)
                    {
                        continue;
                    }

                    string newimgurl = webUrl + imgurl;
                    newContent = newContent.Replace(imgurl, newimgurl);
                }
            }
            return newContent;
        }

        public static void DownloadImage(string remotePath, string filePath)
        {
            var w = new WebClient();
            try
            {
                w.DownloadFile(remotePath, filePath);
            }
            finally
            {
                w.Dispose();
            }
        }

        public static string DownloadImages(string htmlContent, string folderPath, string webUrl)
        {
            string newContent = htmlContent;
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);
            HtmlNodeCollection imgs = doc.DocumentNode.SelectNodes("//img");
            if (imgs != null && imgs.Count > 0)
            {
                if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(folderPath)))
                { Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(folderPath)); }
                foreach (HtmlNode child in imgs)
                {
                    if (child.Attributes["src"] == null)
                    {
                        continue;
                    }

                    string imgurl = child.Attributes["src"].Value;

                    if (imgurl.IndexOf(webUrl, StringComparison.Ordinal) > -1 ||
                        imgurl.IndexOf("http://", StringComparison.Ordinal) == -1)
                    {
                        continue;
                    }

                    string newimgurl = DownLoadImg(imgurl, folderPath);
                    if (newimgurl != "")
                    {
                        newContent = newContent.Replace(imgurl, webUrl + newimgurl);
                    }
                }
            }
            return newContent;
        }

        public static string GetIp()
        {
            string result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }

            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (string.IsNullOrEmpty(result) || !IsIp(result))
            {
                return "127.0.0.1";
            }

            return result;
        }

        public static bool IsIp(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        private static readonly Regex RegexBr = new Regex(@"(\r\n)", RegexOptions.IgnoreCase);

        public static string ClearBr(string str)
        {
            Match m;
            for (m = RegexBr.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "");
            }
            return str;
        }

        public static Regex RegImg =
            new Regex(
                @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>",
                RegexOptions.IgnoreCase);

        public static string GetImg(string str)
        {
            Match m = RegImg.Match(str);
            return m.Success ? m.Groups["imgUrl"].ToString() : "";
        }

        public static string GetImgs(string str)
        {
            MatchCollection matches = RegImg.Matches(str);
            string strTemp = matches.Cast<Match>()
                                    .Aggregate("",
                                               (current, match) => current + ("'" + match.Groups["imgUrl"].Value + "',"));

            return strTemp.Trim(',');
        }

        public static string ClearImgs(string str)
        {
            Match m;

            for (m = RegImg.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "");
            }
            return str;
        }

        public static string GetSourceTextByUrl(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Timeout = 20000;
                WebResponse response = request.GetResponse();

                Stream resStream = response.GetResponseStream();
                if (resStream != null)
                {
                    var sr = new StreamReader(resStream);
                    return sr.ReadToEnd();
                }
            }
            catch
            {
            }
            return string.Empty;
        }

        public static string RemoveHtml(string content)
        {
            return Regex.Replace(content, @"<[^>]*>", string.Empty, RegexOptions.IgnoreCase);
        }

        public static string RemoveUnsafeHtml(string content)
        {
            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2",
                                    RegexOptions.IgnoreCase);
            return content;
        }

        public static string GetTextFromHtml(string html)
        {
            var regEx = new Regex(@"</?(?!br|/?p|img)[^>]*>", RegexOptions.IgnoreCase);
            return regEx.Replace(html, "");
        }

        public static T GetEnum<T>(string value, T defValue)
        {
            try
            {
                return (T) Enum.Parse(typeof (T), value, true);
            }
            catch (ArgumentException)
            {
                return defValue;
            }
        }

        public static string RTrim(string str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].ToString().Equals(" ") || str[i].ToString().Equals("\r") || str[i].ToString().Equals("\n"))
                {
                    str = str.Remove(i, 1);
                }
            }
            return str;
        }

        public static string CutString(string str, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length = length*-1;
                    if (startIndex - length < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        startIndex = startIndex - length;
                    }
                }

                if (startIndex > str.Length)
                {
                    return "";
                }
            }
            else
            {
                if (length < 0)
                {
                    return "";
                }
                if (length + startIndex > 0)
                {
                    length = length + startIndex;
                    startIndex = 0;
                }
                else
                {
                    return string.Empty;
                }
            }

            if (str.Length - startIndex < length)
            {
                length = str.Length - startIndex;
            }

            return str.Substring(startIndex, length);
        }

        public static string CutString(string str, int startIndex)
        {
            return CutString(str, startIndex, str.Length);
        }

        public static string CutString(string str, int cutLength,string addedString)
        {
            if (str.Length <= cutLength)
            {
                return str;
            }
            return str.Substring(0, cutLength) + addedString;
        }

        public static int ObjectToInt(object expression)
        {
            return ObjectToInt(expression, 0);
        }

        public static int ObjectToInt(object expression, int defValue)
        {
            return expression != null ? StrToInt(expression.ToString(), defValue) : defValue;
        }

        public static int StrToInt(string str)
        {
            return StrToInt(str, 0);
        }

        public static int StrToInt(string str, int defValue)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 11 ||
                !Regex.IsMatch(str.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
            {
                return defValue;
            }

            int rv;
            return Int32.TryParse(str, out rv) ? rv : Convert.ToInt32(StrToFloat(str, defValue));
        }

        public static float StrToFloat(string strValue, float defValue)
        {
            if ((strValue == null) || (strValue.Length > 10))
            {
                return defValue;
            }

            float intValue = defValue;
            bool isFloat = Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
            if (isFloat)
            {
                float.TryParse(strValue, out intValue);
            }
            return intValue;
        }

        public static string GetSpace(int count, string str)
        {
            string tmp = "";
            for (int i = 1; i < count; i++)
            {
                tmp += str;
            }
            return tmp;
        }

        public static string[] DistinctStringArray(string[] strArray, int maxElementLength)
        {
            var h = new Hashtable();
            foreach (string s in strArray)
            {
                string k = s;
                if (maxElementLength > 0 && k.Length > maxElementLength)
                {
                    k = k.Substring(0, maxElementLength);
                }
                h[k.Trim()] = s;
            }
            var result = new string[h.Count];
            h.Keys.CopyTo(result, 0);

            return result;
        }

        public static string[] DistinctStringArray(string[] strArray)
        {
            return DistinctStringArray(strArray, 0);
        }

        public static int RegexCount(string regstr, string str)
        {
            var r = new Regex(regstr, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection m = r.Matches(str);
            return m.Count;
        }

        public static string CleanInvalidXmlChars(string text)
        {
            const string re = @"[^\x09\x0A\x0D\x20-\xD7FF\xE000-\xFFFD\x10000-x10FFFF]";
            return Regex.Replace(text, re, "");
        }

        public static void CreateHtml(string url, string outpath)
        {
            FileStream fs;
            if (File.Exists(outpath))
            {
                File.Delete(outpath);
                fs = File.Create(outpath);
            }
            else
            {
                fs = File.Create(outpath);
            }
            byte[] bt = Encoding.UTF8.GetBytes(GetSourceTextByUrl(url));
            fs.Write(bt, 0, bt.Length);
            fs.Close();
        }

        public static void DeleteHtml(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static readonly char[] InvalidFileNameChars = new[]
                                                                  {
                                                                      '"',
                                                                      '<',
                                                                      '>',
                                                                      '|',
                                                                      '\0',
                                                                      '\u0001',
                                                                      '\u0002',
                                                                      '\u0003',
                                                                      '\u0004',
                                                                      '\u0005',
                                                                      '\u0006',
                                                                      '\a',
                                                                      '\b',
                                                                      '\t',
                                                                      '\n',
                                                                      '\v',
                                                                      '\f',
                                                                      '\r',
                                                                      '\u000e',
                                                                      '\u000f',
                                                                      '\u0010',
                                                                      '\u0011',
                                                                      '\u0012',
                                                                      '\u0013',
                                                                      '\u0014',
                                                                      '\u0015',
                                                                      '\u0016',
                                                                      '\u0017',
                                                                      '\u0018',
                                                                      '\u0019',
                                                                      '\u001a',
                                                                      '\u001b',
                                                                      '\u001c',
                                                                      '\u001d',
                                                                      '\u001e',
                                                                      '\u001f',
                                                                      ':',
                                                                      '*',
                                                                      '?',
                                                                      '\\',
                                                                      '/'
                                                                  };
    }
}