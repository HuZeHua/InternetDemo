<%@ WebHandler Language="C#" Class="Ajax" %>
using System;
using System.Web;
using System.IO;

public class Ajax : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        var uid = context.Request.QueryString["input"];
        if (!string.IsNullOrEmpty(context.Request["Filename"]) && !string.IsNullOrEmpty(context.Request["Upload"]))
        {
            ResponseText(UploadTempAvatar(uid));
        }
        if (string.IsNullOrEmpty(context.Request["avatar1"]) || string.IsNullOrEmpty(context.Request["avatar2"]) ||
            string.IsNullOrEmpty(context.Request["avatar3"])) return;
        CreateDir(uid);
        if (!(SaveAvatar("avatar1", uid) && SaveAvatar("avatar2", uid) && SaveAvatar("avatar3", uid)))
        {
            File.Delete(GetMapPath("~\\upload\\avatar\\upload\\avatars\\" + uid + ".jpg"));
            ResponseText("<?xml version=\"1.0\" ?><root><face success=\"0\"/></root>");
            return;
        }
        File.Delete(GetMapPath("~\\upload\\avatar\\upload\\avatars\\" + uid + ".jpg"));
        ResponseText("<?xml version=\"1.0\" ?><root><face success=\"1\"/></root>");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private static void CreateDir(string uid)
    {
        var avatarDir = string.Format("~/upload/avatar/upload/avatars/{0}",
             uid);
        if (!Directory.Exists(GetMapPath(avatarDir)))
            Directory.CreateDirectory(GetMapPath(avatarDir));
    }

    private static void ResponseText(string text)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(text);
        HttpContext.Current.Response.End();
    }

    private static string UploadTempAvatar(string uid)
    {
        var filename = uid + ".jpg";
        var uploadUrl = GetRootUrl("upload/avatar/") + "upload/avatars";
        var uploadDir = GetMapPath("~\\upload\\avatar\\upload\\avatars");
        if (!Directory.Exists(uploadDir + "temp\\"))
        { Directory.CreateDirectory(uploadDir + "temp\\"); }

        filename = "temp/" + filename;
        if (HttpContext.Current.Request.Files.Count > 0)
        {
            HttpContext.Current.Request.Files[0].SaveAs(uploadDir + filename);
        }

        return uploadUrl + filename;
    }

    private static byte[] FlashDataDecode(string s)
    {
        var r = new byte[s.Length / 2];
        var l = s.Length;
        for (var i = 0; i < l; i = i + 2)
        {
            var k1 = s[i] - 48;
            k1 -= k1 > 9 ? 7 : 0;
            var k2 = s[i + 1] - 48;
            k2 -= k2 > 9 ? 7 : 0;
            r[i / 2] = (byte)(k1 << 4 | k2);
        }
        return r;
    }

    private static bool SaveAvatar(string avatar, string uid)
    {
        
        var b = FlashDataDecode(HttpContext.Current.Request[avatar]);
        if (b.Length == 0)
            return false;
        string size;
        switch (avatar)
        {
            case "avatar1":
                size = "large";
                break;
            case "avatar2":
                size = "medium";
                break;
            default:
                size = "small";
                break;
        }
        var avatarFileName = string.Format("~/upload/avatar/upload/avatars/{0}/{1}.jpg",
            uid, size);
        var fs = new FileStream(GetMapPath(avatarFileName), FileMode.Create);
        fs.Write(b, 0, b.Length);
        fs.Close();
        return true;
    }

    public static string GetRootUrl(string forumPath)
    {
        var applicationPath = HttpContext.Current.Request.ApplicationPath != "/" ? HttpContext.Current.Request.ApplicationPath : string.Empty;
        var port = HttpContext.Current.Request.Url.Port;
        return string.Format("{0}://{1}{2}{3}/{4}",
                             HttpContext.Current.Request.Url.Scheme,
                             HttpContext.Current.Request.Url.Host,
                             (port == 80 || port == 0) ? "" : ":" + port,
                             applicationPath,
                             forumPath);
    }

    public static string GetMapPath(string strPath)
    {
        if (HttpContext.Current != null)
        {
            return HttpContext.Current.Server.MapPath(strPath);
        }
        strPath = strPath.Replace("/", "\\").Replace("~", "");
        if (strPath.StartsWith("\\"))
        {
            strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
        }
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
    }
}