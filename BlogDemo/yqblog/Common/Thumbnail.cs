using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;

namespace Common
{
    public class Thumbnail
    {
        private Image _srcImage;

        private string _srcFileName;

        public bool SetImage(string fileName)
        {
            _srcFileName = Utils.GetMapPath(fileName);
            try
            {
                _srcImage = Image.FromFile(_srcFileName);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool ThumbnailCallback()
        {
            return false;
        }

        public Image GetImage(int width, int height)
        {
            Image.GetThumbnailImageAbort callb = ThumbnailCallback;
            Image img = _srcImage.GetThumbnailImage(width, height, callb, IntPtr.Zero);
            return img;
        }

        public void SaveThumbnailImage(int width, int height)
        {
            string extension = Path.GetExtension(_srcFileName);
            if (extension != null)
                switch (extension.ToLower())
                {
                    case ".png":
                        SaveImage(width, height, ImageFormat.Png);
                        break;
                    case ".gif":
                        SaveImage(width, height, ImageFormat.Gif);
                        break;
                    default:
                        SaveImage(width, height, ImageFormat.Jpeg);
                        break;
                }
        }

        public void SaveImage(int width, int height, ImageFormat imgformat)
        {
            if ((Equals(imgformat, ImageFormat.Gif) || (_srcImage.Width <= width)) && (_srcImage.Height <= height))
                return;
            Image.GetThumbnailImageAbort callb = ThumbnailCallback;
            Image img = _srcImage.GetThumbnailImage(width, height, callb, IntPtr.Zero);
            _srcImage.Dispose();
            img.Save(_srcFileName, imgformat);
            img.Dispose();
        }

        public static ImageFormat GetFormat(string name)
        {
            string ext = name.Substring(name.LastIndexOf(".", StringComparison.Ordinal) + 1);
            switch (ext.ToLower())
            {
                case "jpg":
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "bmp":
                    return ImageFormat.Bmp;
                case "png":
                    return ImageFormat.Png;
                case "gif":
                    return ImageFormat.Gif;
                default:
                    return ImageFormat.Jpeg;
            }
        }

        public static void MakeSquareImage(Image image, string newFileName, int newSize)
        {
            int width = image.Width;
            int height = image.Height;

            var b = new Bitmap(newSize, newSize);

            try
            {
                Graphics g = Graphics.FromImage(b);
                g.InterpolationMode = InterpolationMode.High;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.Clear(Color.Transparent);
                g.DrawImage(image, new Rectangle(0, 0, newSize, newSize),
                            width < height
                                ? new Rectangle(0, (height - width)/2, width, width)
                                : new Rectangle((width - height)/2, 0, height, height), GraphicsUnit.Pixel);

                SaveImage(b, newFileName, GetCodecInfo("image/" + GetFormat(newFileName).ToString().ToLower()));
            }
            finally
            {
                image.Dispose();
                b.Dispose();
            }
        }

        public static void MakeSquareImage(string fileName, string newFileName, int newSize)
        {
            MakeSquareImage(Image.FromFile(fileName), newFileName, newSize);
        }

        public static void MakeRemoteSquareImage(string url, string newFileName, int newSize)
        {
            Stream stream = GetRemoteImage(url);
            if (stream == null)
                return;
            Image original = Image.FromStream(stream);
            stream.Close();
            MakeSquareImage(original, newFileName, newSize);
        }

        public static void MakeThumbnailImage(Image original, string newFileName, int maxWidth, int maxHeight)
        {
            Size newSize = ResizeImage(original.Width, original.Height, maxWidth, maxHeight);
            Image displayImage = new Bitmap(original, newSize);

            try
            {
                displayImage.Save(newFileName, original.RawFormat);
            }
            finally
            {
                original.Dispose();
            }
        }

        public static void MakeThumbnailImage(string fileName, string newFileName, int maxWidth, int maxHeight)
        {
            MakeThumbnailImage(Image.FromFile(fileName), newFileName, maxWidth, maxHeight);
        }

        public static void MakeRemoteThumbnailImage(string url, string newFileName, int maxWidth, int maxHeight)
        {
            Stream stream = GetRemoteImage(url);
            if (stream == null)
                return;
            Image original = Image.FromStream(stream);
            stream.Close();
            MakeThumbnailImage(original, newFileName, maxWidth, maxHeight);
        }

        private static void SaveImage(Image image, string savePath, ImageCodecInfo ici)
        {
            var parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, ((long) 100));
            image.Save(savePath, ici, parameters);
            parameters.Dispose();
        }

        private static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] codecInfo = ImageCodecInfo.GetImageEncoders();
            return codecInfo.FirstOrDefault(ici => ici.MimeType == mimeType);
        }

        private static Size ResizeImage(int width, int height, int maxWidth, int maxHeight)
        {
            var MAX_WIDTH = (decimal) maxWidth;
            var MAX_HEIGHT = (decimal) maxHeight;
            decimal aspectRatio = MAX_WIDTH/MAX_HEIGHT;
            var originalWidth = (decimal) width;
            var originalHeight = (decimal) height;
            int newWidth, newHeight;
            if (originalWidth > MAX_WIDTH || originalHeight > MAX_HEIGHT)
            {
                decimal factor;

                if (originalWidth/originalHeight > aspectRatio)
                {
                    factor = originalWidth/MAX_WIDTH;
                    newWidth = Convert.ToInt32(originalWidth/factor);
                    newHeight = Convert.ToInt32(originalHeight/factor);
                }
                else
                {
                    factor = originalHeight/MAX_HEIGHT;
                    newWidth = Convert.ToInt32(originalWidth/factor);
                    newHeight = Convert.ToInt32(originalHeight/factor);
                }
            }
            else
            {
                newWidth = width;
                newHeight = height;
            }
            return new Size(newWidth, newHeight);
        }

        private static Stream GetRemoteImage(string url)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "GET";
            request.ContentLength = 0;
            request.Timeout = 20000;
            try
            {
                var response = (HttpWebResponse) request.GetResponse();
                return response.GetResponseStream();
            }
            catch
            {
                return null;
            }
        }
    }
}
