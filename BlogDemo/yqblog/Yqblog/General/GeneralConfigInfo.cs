using System;
using System.ComponentModel.DataAnnotations;
using resWeb = Resource.Models.Web.Web;

namespace Yqblog.General
{
    [Serializable]
    public class GeneralConfigInfo : IConfigInfo
    {
        #region
        private string _webtitle = "";
        private string _webDescription = "";
        private string _icp = "";
        private string _webpath = "/";
        private int _indexPagerCount = 10;
        private int _catePagerCount = 10;
        private int _commentPagerCount = 10;
        private int _notePagerCount = 10;
        private int _forumPagerCount = 10;
        private int _threadPagerCount = 10;
        private int _decorateImgCount;
        private string _thumbnailInfo = "";
        private string _theme = "";
        private string _defaultlang = "";
        private string _versionNo =  DateTime.Now.ToString("yyyyMMddhhmmss");
        private int _maxSummaryCharCount;
        private string _adminEmail= "";
        private string _smtpServer= "";
        private string _smtpUser= "";
        private string _smtpPass= "";
        private int _smtpPort = 25;
        private int _ifSendReplyEmail = 1;
        private int _ifWebStatic = 2;
        private int _ifHtmlCreated = 2;
        private string _webStaticFolderPart1 = "static";
        private string _webStaticFolderPart2 = "archive";
        private string _webStaticFolder = "/static/{lang}/archive/";
        private string[] _webLangList={};
        private bool _userPermission;
        private bool _ifValidateCode;
        private int _ifIndependentContentViaLang = 1;
        private string _webContentLang="all";
        private string _langTemplateStr = "";
        private int _maxCategoryId;
        private string _logo = "";
        private bool _ifPagingAjax;

        #endregion

        [Required(ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "Common_Required_Msg")]
        [Display(ResourceType = typeof(resWeb), Name = "Webtitle")]
        public string Webtitle
        {
            get { return _webtitle; }
            set { _webtitle = value; }
        }

        [Display(ResourceType = typeof(resWeb), Name = "WebDescription")]
        public string WebDescription
        {
            get { return _webDescription; }
            set { _webDescription = value; }
        }

        [Display(ResourceType = typeof(resWeb), Name = "Logo")]
        public string Logo
        {
            get { return _logo; }
            set { _logo = value; }
        }

        [Display(ResourceType = typeof(resWeb), Name = "Icp")]
        public string Icp
        {
            get { return _icp; }
            set { _icp = value; }
        }

         [Display(ResourceType = typeof(resWeb), Name = "WebPath")]
         public string WebPath
         {
             get { return _webpath; }
             set { _webpath = value; }
         }

         [Display(ResourceType = typeof(resWeb), Name = "IndexPagerCount")]
         [RegularExpression(@"^\d+", ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "MustNum")]
         public int IndexPagerCount
         {
             get { return _indexPagerCount; }
             set { _indexPagerCount = value; }
         }

         [Display(ResourceType = typeof(resWeb), Name = "CatePagerCount")]
         [RegularExpression(@"^\d+", ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "MustNum")]
         public int CatePagerCount
         {
             get { return _catePagerCount; }
             set { _catePagerCount = value; }
         }

         [Display(ResourceType = typeof(resWeb), Name = "CommentPagerCount")]
         [RegularExpression(@"^\d+", ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "MustNum")]
         public int CommentPagerCount
         {
             get { return _commentPagerCount; }
             set { _commentPagerCount = value; }
         }

         [Display(ResourceType = typeof(resWeb), Name = "NotePagerCount")]
         [RegularExpression(@"^\d+", ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "MustNum")]
         public int NotePagerCount
         {
             get { return _notePagerCount; }
             set { _notePagerCount = value; }
         }

         [Display(ResourceType = typeof(resWeb), Name = "ForumPagerCount")]
         [RegularExpression(@"^\d+", ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "MustNum")]
         public int ForumPagerCount
         {
             get { return _forumPagerCount; }
             set { _forumPagerCount = value; }
         }

         [Display(ResourceType = typeof(resWeb), Name = "ThreadPagerCount")]
         [RegularExpression(@"^\d+", ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "MustNum")]
         public int ThreadPagerCount
         {
             get { return _threadPagerCount; }
             set { _threadPagerCount = value; }
         }

         public int DecorateImgCount
         {
             get { return _decorateImgCount; }
             set { _decorateImgCount = value; }
         }

         [Display(ResourceType = typeof(resWeb), Name = "ThumbnailInfo")]
         [RegularExpression(@"^[0-9x,]*$", ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "ThumbnailFormat")]
         public string ThumbnailInfo
         {
             get { return _thumbnailInfo; }
             set { _thumbnailInfo = value; }
         }

         public string CopyRight
         {
             get { return resWeb.CopyRight; }
         }

         [Display(ResourceType = typeof(resWeb), Name = "Theme")]
         [RegularExpression(@"^[a-zA-Z0-9\-]+$", ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "Common_Reg_Msg")]
         public string Theme
         {
             get { return _theme; }
             set { _theme = value; }
         }

         [Display(ResourceType = typeof(resWeb), Name = "DefaultLang")]
         [RegularExpression(@"^[a-zA-Z\-]+$", ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "Common_Reg_Msg")]
         public string DefaultLang
         {
             get { return _defaultlang; }
             set { _defaultlang = value; }
         }

         public string VersionNo
         {
             get { return _versionNo; }
             set { _versionNo = value; }
         }

         [Display(ResourceType = typeof(resWeb), Name = "MaxSummaryCount")]
         [RegularExpression(@"^\d+", ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "MustNum")]
         public int MaxSummaryCharCount
         {
             get { return _maxSummaryCharCount; }
             set { _maxSummaryCharCount = value; }
         }

        [Display(ResourceType = typeof(resWeb), Name = "AdminEmail")]
         public string AdminEmail
         {
             get { return _adminEmail; }
             set { _adminEmail = value; }
         }

        [Display(ResourceType = typeof(resWeb), Name = "SmtpServer")]
         public string SmtpServer
         {
             get { return _smtpServer; }
             set { _smtpServer = value; }
         }

        [Display(ResourceType = typeof(resWeb), Name = "SmtpUser")]
         public string SmtpUser
         {
             get { return _smtpUser; }
             set { _smtpUser = value; }
         }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(resWeb), Name = "SmtpPass")]
         public string SmtpPass
         {
             get { return _smtpPass; }
             set { _smtpPass = value; }
         }

        [Display(ResourceType = typeof(resWeb), Name = "SmtpPort")]
        [RegularExpression(@"^\d+", ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "MustNum")]
         public int SmtpPort
         {
             get { return _smtpPort; }
             set { _smtpPort = value; }
         }

        [Display(ResourceType = typeof(resWeb), Name = "IfSendReplyEmail")]
         public int IfSendReplyEmail
         {
             get { return _ifSendReplyEmail; }
             set { _ifSendReplyEmail = value; }
         }

        [Display(ResourceType = typeof(resWeb), Name = "IfWebStatic")]
        public int IfWebStatic
        {
            get { return _ifWebStatic; }
            set { _ifWebStatic = value; }
        }

        [Display(ResourceType = typeof(resWeb), Name = "IfHtmlCreated")]
        public int IfHtmlCreated
        {
            get { return _ifHtmlCreated; }
            set { _ifHtmlCreated = value; }
        }

        [Required(ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "Common_Required_Msg")]
        [RegularExpression(@"^[a-zA-Z0-9\-]+$", ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "Common_Reg_Msg")]
        public string WebStaticFolderPart1
        {
            get { return _webStaticFolderPart1; }
            set { _webStaticFolderPart1 = value; }
        }

        [RegularExpression(@"^[a-zA-Z0-9\-\{\}\/]+$", ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "Common_Reg_Msg")]
        public string WebStaticFolderPart2
        {
            get { return _webStaticFolderPart2; }
            set { _webStaticFolderPart2 = value; }
        }

        [Display(ResourceType = typeof(resWeb), Name = "WebStaticFolder")]
        public string WebStaticFolder
        {
            get { return _webStaticFolder; }
            set { _webStaticFolder = value; }
        }

        [Display(ResourceType = typeof(resWeb), Name = "WebLangs")]
        [Required(ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "Common_Required_Msg")]
        public string[] WebLangList
        {
            get { return _webLangList; }
            set { _webLangList = value; }
        }

        [Display(ResourceType = typeof(resWeb), Name = "UserPermission")]
        public bool UserPermission
        {
            get { return _userPermission; }
            set { _userPermission = value; }
        }

        [Display(ResourceType = typeof(resWeb), Name = "IfValidateCode")]
        public bool IfValidateCode
        {
            get { return _ifValidateCode; }
            set { _ifValidateCode = value; }
        }

        [Display(ResourceType = typeof(resWeb), Name = "IfPagingAjax")]
        public bool IfPagingAjax
        {
            get { return _ifPagingAjax; }
            set { _ifPagingAjax = value; }
        }

        [Display(ResourceType = typeof(resWeb), Name = "IfIndependentContentViaLang")]
        public int IfIndependentContentViaLang
        {
            get { return _ifIndependentContentViaLang; }
            set { _ifIndependentContentViaLang = value; }
        }

        [Display(ResourceType = typeof(resWeb), Name = "WebContentLang")]
        public string WebContentLang
        {
            get { return _webContentLang; }
            set { _webContentLang = value; }
        }

        public string LangTemplateStr
        {
            get { return _langTemplateStr; }
            set { _langTemplateStr = value; }
        }

        public int MaxCategoryId
        {
            get { return _maxCategoryId; }
            set { _maxCategoryId = value; }
        }
    }
}
