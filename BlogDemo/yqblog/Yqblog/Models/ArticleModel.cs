using System;
using System.ComponentModel.DataAnnotations;
using res = Resource.Models.Article.Article;
using resWeb = Resource.Models.Web.Web;

namespace Yqblog.Models
{
    public class ArticleModel
    {
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "Common_Required_Msg")]
        [StringLength(100, ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "Title_Length_Msg")]
        [Display(ResourceType = typeof(res), Name = "Title")]
        public string Title { get; set; }

        [StringLength(400, ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "Summary_Length_Msg")]
        [Display(ResourceType = typeof(res), Name = "Summary")]
        public string Summary { get; set; }

        [Required(ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "Common_Required_Msg")]
        [Display(ResourceType = typeof(res), Name = "Content")]
        public string Content { get; set; }

        [Required(ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "Common_Required_Msg")]
        [Display(ResourceType = typeof(res), Name = "CateId")]
        public int CateId { get; set; }

        public int TypeId { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "Tags_Length_Msg")]
        [Display(ResourceType = typeof(res), Name = "Tags")]
        public string Tags { get; set; }

        [Display(ResourceType = typeof(res), Name = "ReplyPermit")]
        public byte ReplyPermit { get; set; }

        [Display(ResourceType = typeof(res), Name = "Status")]
        public byte Status { get; set; }

        [Display(ResourceType = typeof(res), Name = "IsCommend")]
        public byte IsCommend { get; set; }

        [Display(ResourceType = typeof(res), Name = "IsTop")]
        public byte IsTop { get; set; }

        [Display(ResourceType = typeof(res), Name = "IsIndexTop")]
        public byte IsIndexTop { get; set; }

        [StringLength(500, ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "SeoTitle_Length_Msg")]
        [Display(ResourceType = typeof(res), Name = "SeoTitle")]
        public string SeoTitle { get; set; }

        [StringLength(1000, ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "SeoDescription_Length_Msg")]
        [Display(ResourceType = typeof(res), Name = "SeoDescription")]
        public string SeoDescription { get; set; }

        [StringLength(500, ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "Seokeywords_Length_Msg")]
        [Display(ResourceType = typeof(res), Name = "Seokeywords")]
        public string Seokeywords { get; set; }

        [StringLength(1000, ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "SeoMetas_Length_Msg")]
        [Display(ResourceType = typeof(res), Name = "SeoMetas")]
        public string SeoMetas { get; set; }

        [StringLength(60, ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "ReName_Length_Msg")]
        [Display(ResourceType = typeof(res), Name = "ReName")]
        [RegularExpression(@"^[a-zA-Z0-9\-]+$", ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "Common_Reg_Msg")]
        public string ReName { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "SeoTitle_Length_Msg")]
        public string E1 { get; set; }
        [StringLength(50, ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "SeoTitle_Length_Msg")]
        public string E2 { get; set; }
        [StringLength(50, ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "SeoTitle_Length_Msg")]
        public string E3 { get; set; }
        [StringLength(50, ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "SeoTitle_Length_Msg")]
        public string E4 { get; set; }
        [StringLength(50, ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "SeoTitle_Length_Msg")]
        public string E5 { get; set; }
        [StringLength(500, ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "SeoTitle_Length_Msg")]
        public string E6 { get; set; }
        [StringLength(500, ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "SeoTitle_Length_Msg")]
        public string E7 { get; set; }
        [StringLength(500, ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "SeoTitle_Length_Msg")]
        public string E8 { get; set; }
        public string E9 { get; set; }
        public string E10 { get; set; }

        public int DataType { get; set; }
        public int ArticleTypeId { get; set; }
    }
}