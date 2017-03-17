using System.Collections.Generic;
using XCode.Core.Domain.Media;

namespace XCode.Services.Media
{
    /// <summary>
    /// ͼƬ
    /// </summary>
    public partial interface IPictureService
    {
        #region ��ȡͼƬ����·����Url����

        /// <summary>
        /// ��ȡĬ��ͼƬ·��
        /// </summary>
        /// <param name="targetSize">ͼƬ��С</param>
        /// <param name="defaultPictureType">Ĭ��ͼƬ����</param>
        /// <returns>ͼƬ·��</returns>
        string GetDefaultPictureUrl(int targetSize = 0,
            PictureType defaultPictureType = PictureType.Entity);

        /// <summary>
        /// ��ȡͼƬ·��
        /// </summary>
        /// <param name="pictureId">ͼƬ����Id</param>
        /// <param name="targetSize">ͼƬ��С</param>
        /// <param name="showDefaultPicture">�Ƿ���ʾĬ��ͼ��</param>
        /// <param name="defaultPictureType">Ĭ��ͼƬ����</param>
        /// <returns>ͼƬ·��</returns>
        string GetPictureUrl(int pictureId,
            int targetSize = 0,
            bool showDefaultPicture = true,
            PictureType defaultPictureType = PictureType.Entity);

        /// <summary>
        /// ��ȡͼƬ·��
        /// </summary>
        /// <param name="picture">ͼƬ����</param>
        /// <param name="targetSize">ͼƬ��С</param>
        /// <param name="showDefaultPicture">�Ƿ���ʾĬ��ͼ��</param>
        /// <param name="defaultPictureType">Ĭ��ͼƬ����</param>
        /// <returns>ͼƬ·��</returns>
        string GetPictureUrl(Picture picture,
            int targetSize = 0,
            bool showDefaultPicture = true,
            PictureType defaultPictureType = PictureType.Entity);

        /// <summary>
        /// ��ȡ����ͼ·��
        /// </summary>
        /// <param name="picture">ͼƬ����</param>
        /// <param name="targetSize">ͼƬ��С</param>
        /// <param name="showDefaultPicture">�Ƿ���ʾĬ��ͼ��</param>
        /// <returns>����ͼ·��</returns>
        string GetThumbLocalPath(Picture picture, int targetSize = 0, bool showDefaultPicture = true);

        #endregion

        #region ͼƬ��������

        /// <summary>
        /// ��ȡͼƬ
        /// </summary>
        /// <param name="pictureId">ͼƬ����Id</param>
        /// <returns>ͼƬ����</returns>
        Picture GetPictureById(int pictureId);

        /// <summary>
        /// ɾ��ͼƬ
        /// </summary>
        /// <param name="picture">Picture</param>
        void DeletePicture(Picture picture);

        /// <summary>
        /// ��ȡͼƬ����
        /// </summary>
        /// <param name="pageIndex">��ǰҳ��</param>
        /// <param name="pageSize">ÿҳ��ʾ����</param>
        /// <returns>ͼƬ����</returns>
        IEnumerable<Picture> GetPictures(int pageIndex, int pageSize);

        /// <summary>
        /// ����ͼƬ
        /// </summary>
        /// <param name="pictureBinary">�ֽڴ�С</param>
        /// <param name="mimeType">ͼƬ��׺</param>
        /// <param name="seoFilename">Seo �ļ���</param>
        /// <param name="isNew">�Ƿ���ͼƬ</param>
        /// <param name="validateBinary">��ֵ��ʾ�Ƿ�Ҫ�ṩ��֤ͼƬ�Ķ�����</param>
        /// <returns>ͼƬ����</returns>
        Picture InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename, bool isNew, bool validateBinary = true);

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
        Picture UpdatePicture(int pictureId, byte[] pictureBinary, string mimeType, string seoFilename, bool isNew, bool validateBinary = true);

        /// <summary>
        /// ��֤ͼƬ������
        /// </summary>
        /// <param name="pictureBinary">ͼƬ�ֽڴ�С</param>
        /// <param name="mimeType">ͼƬ��׺����</param>
        /// <returns>ͼƬ�ֽڴ�С���׳��쳣</returns>
        byte[] ValidatePicture(byte[] pictureBinary, string mimeType);

        #endregion
    }
}
