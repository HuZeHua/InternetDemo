using System;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.IO;
using Yqblog.General;
using resUploadify = Resource.Uploadify.Uploadify;
using System.Globalization;

namespace Yqblog.Controllers
{
    public class UploadifyController : BaseController
    {
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase fileData)
        {
            Response.Charset = "UTF-8";
            const string uploadPath = "/upload/photo";
            var attachdir = Url.Content("~"+uploadPath);
            const int dirtype =2; // 1,by day 2,by month 3, by extendname, 4, tmp folder
            const int maxattachsize = 2097152;
            const string upext = "jpg,gif,png";
            byte[] file;
            string localname;
            var disposition = Request.ServerVariables["HTTP_CONTENT_DISPOSITION"];

            var err = "";
            var msg = "''";

            if (disposition != null)
            {
                file = Request.BinaryRead(Request.TotalBytes);
                localname = Server.UrlDecode(Regex.Match(disposition, "filename=\"(.+?)\"").Groups[1].Value);
            }
            else
            {
                localname = fileData.FileName;
                file = new Byte[fileData.ContentLength];
                var stream = fileData.InputStream;
                stream.Read(file, 0, fileData.ContentLength);
                stream.Close();
            }

            if (file.Length == 0) err = resUploadify.Uploadify_NoData_Tip;
            else
            {
                if (file.Length > maxattachsize) err = resUploadify.Uploadify_Size_Tip + maxattachsize + resUploadify.Byte;
                else
                {
                    var extension = GetFileExt(localname);

                    if (Array.IndexOf(upext.Split(','), extension) == -1) err = resUploadify.Uploadify_Extension_Tip + upext;
                    else
                    {
                        string attachSubdir;
                        switch (dirtype)
                        {
                            case 2:
                                attachSubdir = "month_" + DateTime.Now.ToString("yyMM");
                                break;
                            case 3:
                                attachSubdir = "ext_" + extension;
                                break;
                            case 4:
                                attachSubdir = "TmpUploadPhoto";
                                break;
                            default:
                                attachSubdir = "day_" + DateTime.Now.ToString("yyMMdd");
                                break;
                        }
                        var attachDir = attachdir + "/" + attachSubdir + "/";

                        new Random(DateTime.Now.Millisecond);
                        var filename = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + "." + extension;

                        var target = attachDir + filename;
                        try
                        {
                            var folder = Server.MapPath(attachDir);
                            WebUtils.CreateFolder(folder);
                            var fs = new FileStream(Server.MapPath(target), FileMode.Create, FileAccess.Write);
                            fs.Write(file, 0, file.Length);
                            fs.Flush();
                            fs.Close();
                            WebUtils.CreateThumbnail(target, attachDir, filename);
                        }
                        catch (Exception ex)
                        {
                            err = ex.Message;
                        }
                        var uploadUrl = JsonString(uploadPath + "/" + attachSubdir + "/" + filename);
                        msg = "{'url':'" + uploadUrl + "','localname':'" + JsonString(localname) + "'}";
                    }
                }
            }

            return Content("{'err':'" + JsonString(err) + "','msg':" + msg + "}");
        }

        static string JsonString(string str)
        {
            str = str.Replace("\\", "\\\\");
            str = str.Replace("/", "\\/");
            str = str.Replace("'", "\\'");
            return str;
        }

        static string GetFileExt(string fullPath)
        {
            return fullPath != "" ? fullPath.Substring(fullPath.LastIndexOf('.') + 1).ToLower() : "";
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ContentResult Import(HttpPostedFileBase fileData, string folder, string name)
        {
            var result = "";
            if (null != fileData)
            {
                try
                {
                    var s = Path.GetExtension(fileData.FileName);
                    if (s != null)
                    {
                        var extension = s.ToLower();
                        new Random(DateTime.Now.Millisecond);
                        var filename = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + "." + extension;

                        SaveFile(fileData, filename);
                    }
                }
                catch
                {
                    result = "";
                }
            }
            return Content(result);
        }

        [NonAction]
        private void SaveFile(HttpPostedFileBase postedFile, string saveName)
        {
            var phyPath = Request.MapPath("~/upload/photo/");
            if (!Directory.Exists(phyPath))
            {
                Directory.CreateDirectory(phyPath);
            }
            try
            {
                postedFile.SaveAs(phyPath + saveName);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }
    }
}