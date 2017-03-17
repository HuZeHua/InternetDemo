using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ImageResizer;
using XCode.Core;
using XCode.Core.Data;
using XCode.Core.Domain.Media;

namespace XCode.Services.Media
{
    /// <summary>
    /// ͼƬ
    /// </summary>
    public partial class PictureService : IPictureService
    {
        #region ˽���ֶ�

        private const int MULTIPLE_THUMB_DIRECTORIES_LENGTH = 3;

        private static readonly object s_lock = new object();

        private readonly IRepository<Picture> _pictureRepository;
        private readonly IWebHelper _webHelper;

        #endregion

        #region ���캯��

        public PictureService(IRepository<Picture> pictureRepository,IWebHelper webHelper)
        {
            this._pictureRepository = pictureRepository;
            this._webHelper = webHelper;
        }

        #endregion

        #region ���÷���

        /// <summary>
        /// �ü�ͼƬ
        /// </summary>
        /// <param name="originalSize">ԭͼ��С</param>
        /// <param name="targetSize">Ŀ���С</param>
        /// <param name="resizeType">�ü���ʽ</param>
        /// <param name="ensureSizePositive">ȷ������ֵ</param>
        /// <returns></returns>
        protected virtual Size CalculateDimensions(Size originalSize, int targetSize, 
            ResizeType resizeType = ResizeType.LongestSide, bool ensureSizePositive = true)
        {
            var newSize = new Size();
            switch (resizeType)
            {
                case ResizeType.LongestSide:
                    if (originalSize.Height > originalSize.Width)
                    {
                        newSize.Width = (int)(originalSize.Width * (float)(targetSize / (float)originalSize.Height));
                        newSize.Height = targetSize;
                    }
                    else 
                    {
                        newSize.Height = (int)(originalSize.Height * (float)(targetSize / (float)originalSize.Width));
                        newSize.Width = targetSize;
                    }
                    break;
                case ResizeType.Width:
                    newSize.Height = (int)(originalSize.Height * (float)(targetSize / (float)originalSize.Width));
                    newSize.Width = targetSize;
                    break;
                case ResizeType.Height:
                    newSize.Width = (int)(originalSize.Width * (float)(targetSize / (float)originalSize.Height));
                    newSize.Height = targetSize;
                    break;
                default:
                    throw new Exception("Not supported ResizeType");
            }

            if (ensureSizePositive)
            {
                if (newSize.Width < 1)
                    newSize.Width = 1;
                if (newSize.Height < 1)
                    newSize.Height = 1;
            }

            return newSize;
        }
        
        /// <summary>
        /// �ļ�����׺
        /// </summary>
        /// <param name="mimeType">��׺</param>
        /// <returns>�ļ�����׺</returns>
        protected virtual string GetFileExtensionFromMimeType(string mimeType)
        {
            if (mimeType == null)
                return null;

            //also see System.Web.MimeMapping for more mime types

            string[] parts = mimeType.Split('/');
            string lastPart = parts[parts.Length - 1];
            switch (lastPart)
            {
                case "pjpeg":
                    lastPart = "jpg";
                    break;
                case "x-png":
                    lastPart = "png";
                    break;
                case "x-icon":
                    lastPart = "ico";
                    break;
            }
            return lastPart;
        }

        /// <summary>
        /// ��ͼƬת�����ֽ�
        /// </summary>
        /// <param name="pictureId">ͼƬ����Id</param>
        /// <param name="mimeType">��׺</param>
        /// <returns>ͼƬ�ֽ�</returns>
        protected virtual byte[] LoadPictureFromFile(int pictureId, string mimeType)
        {
            string lastPart = GetFileExtensionFromMimeType(mimeType);
            string fileName = string.Format("{0}_0.{1}", pictureId.ToString("0000000"), lastPart);
            var filePath = GetPictureLocalPath(fileName);
            if (!File.Exists(filePath))
                return new byte[0];
            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// ����ͼƬ
        /// </summary>
        /// <param name="pictureId">ͼƬ����Id</param>
        /// <param name="pictureBinary">ͼƬ�ֽ�</param>
        /// <param name="mimeType">��׺</param>
        protected virtual void SavePictureInFile(int pictureId, byte[] pictureBinary, string mimeType)
        {
            string lastPart = GetFileExtensionFromMimeType(mimeType);
            string fileName = string.Format("{0}_0.{1}", pictureId.ToString("0000000"), lastPart);
            File.WriteAllBytes(GetPictureLocalPath(fileName), pictureBinary);
        }

        /// <summary>
        /// ɾ��ͼƬ
        /// </summary>
        /// <param name="picture">ͼƬ����</param>
        protected virtual void DeletePictureOnFileSystem(Picture picture)
        {
            if (picture == null)
                throw new ArgumentNullException("picture");

            string lastPart = GetFileExtensionFromMimeType(picture.MimeType);
            string fileName = string.Format("{0}_0.{1}", picture.Id.ToString("0000000"), lastPart);
            string filePath = GetPictureLocalPath(fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// ɾ������ͼ
        /// </summary>
        /// <param name="picture">ͼƬ����</param>
        protected virtual void DeletePictureThumbs(Picture picture)
        {
            string filter = string.Format("{0}*.*", picture.Id.ToString("0000000"));
            var thumbDirectoryPath = _webHelper.MapPath("~/content/images/thumbs");
            string[] currentFiles = System.IO.Directory.GetFiles(thumbDirectoryPath, filter, SearchOption.AllDirectories);
            foreach (string currentFileName in currentFiles)
            {
                var thumbFilePath = GetThumbLocalPath(currentFileName);
                File.Delete(thumbFilePath);
            }
        }

        /// <summary>
        /// ��ȡͼƬ����ͼ·��
        /// </summary>
        /// <param name="thumbFileName">�ļ�����</param>
        /// <returns>����ͼ·��</returns>
        protected virtual string GetThumbLocalPath(string thumbFileName)
        {
            var thumbsDirectoryPath = _webHelper.MapPath("~/content/images/thumbs");
            //if (_mediaSettings.MultipleThumbDirectories)
            //{
            //    //get the first two letters of the file name
            //    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(thumbFileName);
            //    if (fileNameWithoutExtension != null && fileNameWithoutExtension.Length > MULTIPLE_THUMB_DIRECTORIES_LENGTH)
            //    {
            //        var subDirectoryName = fileNameWithoutExtension.Substring(0, MULTIPLE_THUMB_DIRECTORIES_LENGTH);
            //        thumbsDirectoryPath = Path.Combine(thumbsDirectoryPath, subDirectoryName);
            //        if (!System.IO.Directory.Exists(thumbsDirectoryPath))
            //        {
            //            System.IO.Directory.CreateDirectory(thumbsDirectoryPath);
            //        }
            //    }
            //}
            var thumbFilePath = Path.Combine(thumbsDirectoryPath, thumbFileName);
            return thumbFilePath;
        }

        /// <summary>
        /// Get picture (thumb) URL 
        /// </summary>
        /// <param name="thumbFileName">Filename</param>
        /// <returns>Local picture thumb path</returns>
        protected virtual string GetThumbUrl(string thumbFileName)
        {

            var url = "/content/images/thumbs/";

            //if (_mediaSettings.MultipleThumbDirectories)
            //{
            //    //get the first two letters of the file name
            //    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(thumbFileName);
            //    if (fileNameWithoutExtension != null && fileNameWithoutExtension.Length > MULTIPLE_THUMB_DIRECTORIES_LENGTH)
            //    {
            //        var subDirectoryName = fileNameWithoutExtension.Substring(0, MULTIPLE_THUMB_DIRECTORIES_LENGTH);
            //        url = url + subDirectoryName + "/";
            //    }
            //}

            url = url + thumbFileName;
            return url;
        }

        /// <summary>
        /// Get picture local path. Used when images stored on file system (not in the database)
        /// </summary>
        /// <param name="fileName">Filename</param>
        /// <returns>Local picture path</returns>
        protected virtual string GetPictureLocalPath(string fileName)
        {
            //TODO:��Ҫ�Զ�����Ŀ¼
            var imagesDirectoryPath = _webHelper.MapPath("~/content/images/");
            var filePath = Path.Combine(imagesDirectoryPath, fileName);
            return filePath;
        }

        #endregion

        #region ��ȡͼƬ����·����Url����

        /// <summary>
        /// ��ȡĬ��ͼƬ·��
        /// </summary>
        /// <param name="targetSize">ͼƬ��С</param>
        /// <param name="defaultPictureType">Ĭ��ͼƬ����</param>
        /// <returns>ͼƬ·��</returns>
        public virtual string GetDefaultPictureUrl(int targetSize = 0, 
            PictureType defaultPictureType = PictureType.Entity)
        {
            string defaultImageFileName;
            switch (defaultPictureType)
            {
                case PictureType.Entity:
                    defaultImageFileName = "default-image.gif";
                    //defaultImageFileName = _settingService.GetSettingByKey("Media.DefaultImageName", "default-image.gif");
                    break;
                case PictureType.Avatar:
                    defaultImageFileName = "default-avatar.jpg";
                    //defaultImageFileName = _settingService.GetSettingByKey("Media.Customer.DefaultAvatarImageName", "default-avatar.jpg");
                    break;
                default:
                    defaultImageFileName = "default-image.gif";
                    //defaultImageFileName = _settingService.GetSettingByKey("Media.DefaultImageName", "default-image.gif");
                    break;
            }

            string filePath = GetPictureLocalPath(defaultImageFileName);
            if (!File.Exists(filePath))
            {
                return "";
            }
            if (targetSize == 0)
            {
                string url = "/content/images/" + defaultImageFileName;
                return url;
            }
            else
            {
                string fileExtension = Path.GetExtension(filePath);
                string thumbFileName = string.Format("{0}_{1}{2}",
                    Path.GetFileNameWithoutExtension(filePath),
                    targetSize,
                    fileExtension);
                var thumbFilePath = GetThumbLocalPath(thumbFileName);
                if (!File.Exists(thumbFilePath))
                {
                    using (var b = new Bitmap(filePath))
                    {
                        var newSize = CalculateDimensions(b.Size, targetSize);

                        var destStream = new MemoryStream();
                        ImageBuilder.Current.Build(b, destStream, new ResizeSettings()
                        {
                            Width = newSize.Width,
                            Height = newSize.Height,
                            Scale = ScaleMode.Both,
                            //Quality = _mediaSettings.DefaultImageQuality
                        });
                        var destBinary = destStream.ToArray();
                        File.WriteAllBytes(thumbFilePath, destBinary);
                    }
                }
                var url = GetThumbUrl(thumbFileName);
                return url;
            }
        }

        /// <summary>
        /// ��ȡͼƬ·��
        /// </summary>
        /// <param name="pictureId">ͼƬ����Id</param>
        /// <param name="targetSize">ͼƬ��С</param>
        /// <param name="showDefaultPicture">�Ƿ���ʾĬ��ͼ��</param>
        /// <param name="defaultPictureType">Ĭ��ͼƬ����</param>
        /// <returns>ͼƬ·��</returns>
        public virtual string GetPictureUrl(int pictureId,
            int targetSize = 0,
            bool showDefaultPicture = true, 
            PictureType defaultPictureType = PictureType.Entity)
        {
            var picture = GetPictureById(pictureId);
            return GetPictureUrl(picture, targetSize, showDefaultPicture, defaultPictureType);
        }

        /// <summary>
        /// ��ȡͼƬ·��
        /// </summary>
        /// <param name="picture">ͼƬ����</param>
        /// <param name="targetSize">ͼƬ��С</param>
        /// <param name="showDefaultPicture">�Ƿ���ʾĬ��ͼ��</param>
        /// <param name="defaultPictureType">DĬ��ͼƬ����</param>
        /// <returns>ͼƬ·��</returns>
        public virtual string GetPictureUrl(Picture picture, 
            int targetSize = 0,
            bool showDefaultPicture = true, 
            PictureType defaultPictureType = PictureType.Entity)
        {
            string url = string.Empty;
            byte[] pictureBinary = null;
            if (picture != null)
            {
                pictureBinary = LoadPictureFromFile(picture.Id, picture.MimeType);
            }

            if (picture == null || pictureBinary == null || pictureBinary.Length == 0)
            {
                if(showDefaultPicture)
                {
                    url = GetDefaultPictureUrl(targetSize, defaultPictureType);
                }
                return url;
            }

            string lastPart = GetFileExtensionFromMimeType(picture.MimeType);
            string thumbFileName;
            if (picture.IsNew)
            {
                DeletePictureThumbs(picture);

                //we do not validate picture binary here to ensure that no exception ("Parameter is not valid") will be thrown
                picture = UpdatePicture(picture.Id, 
                    pictureBinary, 
                    picture.MimeType, 
                    picture.SeoFilename, 
                    false, 
                    false);
            }
            lock (s_lock)
            {
                string seoFileName = picture.SeoFilename; // = GetPictureSeName(picture.SeoFilename); //just for sure
                if (targetSize == 0)
                {
                    thumbFileName = !String.IsNullOrEmpty(seoFileName) ?
                        string.Format("{0}_{1}.{2}", picture.Id.ToString("0000000"), seoFileName, lastPart) :
                        string.Format("{0}.{1}", picture.Id.ToString("0000000"), lastPart);
                    var thumbFilePath = GetThumbLocalPath(thumbFileName);
                    if (!File.Exists(thumbFilePath))
                    {
                        File.WriteAllBytes(thumbFilePath, pictureBinary);
                    }
                }
                else
                {
                    thumbFileName = !String.IsNullOrEmpty(seoFileName) ?
                        string.Format("{0}_{1}_{2}.{3}", picture.Id.ToString("0000000"), seoFileName, targetSize, lastPart) :
                        string.Format("{0}_{1}.{2}", picture.Id.ToString("0000000"), targetSize, lastPart);
                    var thumbFilePath = GetThumbLocalPath(thumbFileName);
                    if (!File.Exists(thumbFilePath))
                    {
                        using (var stream = new MemoryStream(pictureBinary))
                        {
                            Bitmap b = null;
                            try
                            {
                                //try-catch to ensure that picture binary is really OK. Otherwise, we can get "Parameter is not valid" exception if binary is corrupted for some reasons
                                b = new Bitmap(stream);
                            }
                            catch (ArgumentException exc)
                            {
                                //_logger.Error(string.Format("Error generating picture thumb. ID={0}", picture.Id), exc);
                            }
                            if (b == null)
                            {
                                //bitmap could not be loaded for some reasons
                                return url;
                            }

                            var newSize = CalculateDimensions(b.Size, targetSize);

                            var destStream = new MemoryStream();
                            ImageBuilder.Current.Build(b, destStream, new ResizeSettings()
                            {
                                Width = newSize.Width,
                                Height = newSize.Height,
                                Scale = ScaleMode.Both,
                                //Quality = _mediaSettings.DefaultImageQuality
                            });
                            var destBinary = destStream.ToArray();
                            File.WriteAllBytes(thumbFilePath, destBinary);

                            b.Dispose();
                        }
                    }
                }
            }
            url = GetThumbUrl(thumbFileName);
            return url;
        }

        /// <summary>
        /// ��ȡ����ͼ·��
        /// </summary>
        /// <param name="picture">ͼƬ����</param>
        /// <param name="targetSize">ͼƬ��С</param>
        /// <param name="showDefaultPicture">�Ƿ���ʾĬ��ͼ��</param>
        /// <returns>����ͼ·��</returns>
        public virtual string GetThumbLocalPath(Picture picture, int targetSize = 0, bool showDefaultPicture = true)
        {
            string url = GetPictureUrl(picture, targetSize, showDefaultPicture);
            if(String.IsNullOrEmpty(url))
                return String.Empty;
            else
                return GetThumbLocalPath(Path.GetFileName(url));
        }

        #endregion

        #region ͼƬ��������

        /// <summary>
        /// ��ȡͼƬ
        /// </summary>
        /// <param name="pictureId">ͼƬ����Id</param>
        /// <returns>ͼƬ����</returns>
        public virtual Picture GetPictureById(int pictureId)
        {
            if (pictureId == 0)
                return null;

            return _pictureRepository.GetById(pictureId);
        }

        /// <summary>
        /// ɾ��ͼƬ
        /// </summary>
        /// <param name="picture">Picture</param>
        public virtual void DeletePicture(Picture picture)
        {
            if (picture == null)
                throw new ArgumentNullException("picture");

            //delete thumbs
            DeletePictureThumbs(picture);
            
            //delete from file system
            DeletePictureOnFileSystem(picture);

            //delete from database
            _pictureRepository.Delete(picture);

        }

        /// <summary>
        /// ��ȡͼƬ����
        /// </summary>
        /// <param name="pageIndex">��ǰҳ��</param>
        /// <param name="pageSize">ÿҳ��ʾ����</param>
        /// <returns>ͼƬ����</returns>
        public virtual IEnumerable<Picture> GetPictures(int pageIndex, int pageSize)
        {
            var query = from p in _pictureRepository.Table
                       orderby p.Id descending
                       select p;
            //var pics = new PagedList<Picture>(query, pageIndex, pageSize);
            return query.ToList();
        }


        /// <summary>
        /// ����ͼƬ
        /// </summary>
        /// <param name="pictureBinary">�ֽڴ�С</param>
        /// <param name="mimeType">ͼƬ��׺</param>
        /// <param name="seoFilename">Seo �ļ���</param>
        /// <param name="isNew">�Ƿ���ͼƬ</param>
        /// <param name="validateBinary">��ֵ��ʾ�Ƿ�Ҫ�ṩ��֤ͼƬ�Ķ�����</param>
        /// <returns>ͼƬ����</returns>
        public virtual Picture InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename,
            bool isNew, bool validateBinary = true)
        {
            mimeType = CommonHelper.EnsureNotNull(mimeType);
            mimeType = CommonHelper.EnsureMaximumLength(mimeType, 20);

            seoFilename = CommonHelper.EnsureMaximumLength(seoFilename, 100);

            if (validateBinary)
                pictureBinary = ValidatePicture(pictureBinary, mimeType);

            var picture = new Picture()
                              {
                                  MimeType = mimeType,
                                  SeoFilename = seoFilename,
                                  IsNew = isNew,
                              };

            _pictureRepository.Insert(picture);

            SavePictureInFile(picture.Id, pictureBinary, mimeType);
            
            return picture;
        }

        /// <summary>
        /// ����ͼƬ
        /// </summary>
        /// <param name="pictureId">ͼƬ����Id</param>
        /// <param name="pictureBinary">�ֽڴ�С</param>
        /// <param name="mimeType">ͼƬ��׺</param>
        /// <param name="seoFilename">Seo �ļ���</param>
        /// <param name="isNew">�Ƿ���ͼƬ</param>
        /// <param name="validateBinary">��ֵ��ʾ�Ƿ�Ҫ�ṩ��֤ͼƬ�Ķ�����</param>
        /// <returns>ͼƬ����</returns>
        public virtual Picture UpdatePicture(int pictureId, byte[] pictureBinary, string mimeType,
            string seoFilename, bool isNew, bool validateBinary = true)
        {
            mimeType = CommonHelper.EnsureNotNull(mimeType);
            mimeType = CommonHelper.EnsureMaximumLength(mimeType, 20);

            seoFilename = CommonHelper.EnsureMaximumLength(seoFilename, 100);

            if (validateBinary)
                pictureBinary = ValidatePicture(pictureBinary, mimeType);

            var picture = GetPictureById(pictureId);
            if (picture == null)
                return null;

            //delete old thumbs if a picture has been changed
            if (seoFilename != picture.SeoFilename)
                DeletePictureThumbs(picture);

            picture.MimeType = mimeType;
            picture.SeoFilename = seoFilename;
            picture.IsNew = isNew;

            _pictureRepository.Update(picture);

            SavePictureInFile(picture.Id, pictureBinary, mimeType);

            return picture;
        }

        /// <summary>
        /// ��֤ͼƬ������
        /// </summary>
        /// <param name="pictureBinary">ͼƬ�ֽڴ�С</param>
        /// <param name="mimeType">ͼƬ��׺����</param>
        /// <returns>ͼƬ�ֽڴ�С���׳��쳣</returns>
        public virtual byte[] ValidatePicture(byte[] pictureBinary, string mimeType)
        {
            var destStream = new MemoryStream();
            ImageBuilder.Current.Build(pictureBinary, destStream, new ResizeSettings()
            {
                //MaxWidth = _mediaSettings.MaximumImageSize,
                //MaxHeight = _mediaSettings.MaximumImageSize,
                //Quality = _mediaSettings.DefaultImageQuality
                MaxWidth = 1000,
                MaxHeight = 1000,
                Quality = 80
            });
            return destStream.ToArray();
        }
        
        #endregion
    }
}
