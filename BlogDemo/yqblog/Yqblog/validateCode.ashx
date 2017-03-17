<%@ WebHandler Language="C#" Class="ValidationCode" %>

using System;
using System.Web;
using System.Drawing;
using System.Reflection;
using System.Drawing.Imaging;
using System.Linq;
using System.Web.SessionState;

public class ValidationCode : IHttpHandler, IReadOnlySessionState
{
    static Random r = new Random(Guid.NewGuid().GetHashCode());
    static PropertyInfo[] colors = (typeof(Brushes).GetProperties(BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Static)).Where(p => p.Name != "Black" && p.Name != "Transparent").Select(p => p).ToArray();
    static PropertyInfo[] linecolors = (typeof(Pens).GetProperties(BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Static)).Where(p => p.Name != "Black").Select(p => p).ToArray();
    static object colorobj = typeof(Brushes).GetConstructor(BindingFlags.NonPublic, null, Type.EmptyTypes, null);
    static object penobj = typeof(Pens).GetConstructor(BindingFlags.NonPublic, null, Type.EmptyTypes, null);

    int pernumberWidth = 20;
    int pernumberHeight = 30;
    int randomStart = 4;
    int randomEnd = 10;

    public void ProcessRequest(HttpContext context)
    {
        int reqNum = 5;
        if (context.Request.QueryString["n"] != null)
        {
            int.TryParse(context.Request.QueryString["n"], out reqNum);
        }

        if (context.Request.QueryString["p"] != null && context.Request.QueryString["p"].Split(',').Length==4)
        {
            string[] arrParas = context.Request.QueryString["p"].Split(',');
            int.TryParse(arrParas[0], out pernumberWidth);
            int.TryParse(arrParas[1], out pernumberHeight);
            int.TryParse(arrParas[2], out randomStart);
            int.TryParse(arrParas[3], out randomEnd);
        }

        Bitmap bt = new Bitmap((int)(pernumberWidth * reqNum), (int)pernumberHeight);
        Graphics g = Graphics.FromImage(bt);

        string numbers = "";

        for (int i = 1; i <= reqNum; i++)
        {
            numbers += r.Next(0, 9).ToString();
            var color = (PropertyInfo)colors.GetValue(r.Next(0, colors.Length));
            context.Response.Write(color.Name + "<br/>");
            Brush randomcolor = (Brush)color.GetValue(colorobj, null);
            g.DrawString(numbers[i - 1].ToString(), new Font("Arial Narrow", pernumberWidth), randomcolor, new PointF((i - 1) * pernumberWidth, 0f));
        }

        context.Session["validationCode"] = numbers;

        int linenum = r.Next(randomStart, randomEnd);
        for (int i = 1; i <= linenum; i++)
        {
            var linecolor = (PropertyInfo)linecolors.GetValue(r.Next(0, colors.Length));
            Pen randomcolor = (Pen)linecolor.GetValue(penobj, null);
            g.DrawLine(randomcolor, new PointF((float)(r.NextDouble() * pernumberHeight * reqNum), (float)(r.NextDouble() * pernumberHeight)), new PointF((float)(r.NextDouble() * pernumberWidth * reqNum), (float)(r.NextDouble() * pernumberHeight)));
        }
        g.Dispose();
        context.Response.Clear();
        context.Response.ContentType = "image/jpeg";
        bt.Save(context.Response.OutputStream, ImageFormat.Jpeg);
        bt.Dispose();
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}