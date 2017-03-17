using System.ComponentModel.DataAnnotations;
using res = Resource.Models.Category.Category;
using resWeb = Resource.Models.Web.Web;

namespace Yqblog.Models
{
    public class CategoryModel
    {
        public int CateId { get; set; }

        [Required(ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "Common_Required_Msg")]
        [Display(ResourceType = typeof(res), Name = "CateName")]
        public string CateName { get; set; }

        [Required(ErrorMessageResourceType = typeof(resWeb), ErrorMessageResourceName = "Common_Required_Msg")]
        [Display(ResourceType = typeof(res), Name = "Type")]
        public int Type { get; set; }

        [Display(ResourceType = typeof(res), Name = "ListNum")]
        [RegularExpression(@"^\d+", ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "ListNum_Reg_Msg")]
        public int ListNum { get; set; }

        public int OrderId { get; set; }

        [Display(ResourceType = typeof(res), Name = "ReplyPermit")]
        public int ReplyPermit { get; set; }

        [Display(ResourceType = typeof(res), Name = "ParentId")]
        public int ParentId { get; set; }

        [Display(ResourceType = typeof(res), Name = "IsNav")]
        public bool IsNav { get; set; }

        [Display(ResourceType = typeof(res), Name = "IsIndex")]
        public bool IsIndex { get; set; }

        public int SubCount { get; set; }

        [Display(ResourceType = typeof(res), Name = "Status")]
        public int Status { get; set; }

        public string Path { get; set; }
        [Display(ResourceType = typeof(res), Name = "HomePageKey")]
        [RegularExpression(@"^[a-zA-Z]{1}[a-zA-Z0-9_\-]{2,19}$", ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "ReName_Reg_Msg")]
        public string HomePageKey { get; set; }

        [Display(ResourceType = typeof(res), Name = "ReName")]
        [RegularExpression(@"^[a-zA-Z]{1}[a-zA-Z0-9_\-]{2,19}$", ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "ReName_Reg_Msg")]
        public string ReName { get; set; }

        [Display(ResourceType = typeof(res), Name = "CustomView")]
        [RegularExpression(@"^[a-zA-Z]{1}[a-zA-Z0-9_\-]{2,19}$", ErrorMessageResourceType = typeof(res), ErrorMessageResourceName = "ReName_Reg_Msg")]
        public string CustomView { get; set; }

        [Display(ResourceType = typeof(res), Name = "Description")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(res), Name = "Admins")]
        public string Admins { get; set; }

        public string ViewName { get; set; }
    }
}