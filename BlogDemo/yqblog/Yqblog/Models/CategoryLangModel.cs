using System.ComponentModel.DataAnnotations;
using Resource.Models.Category;
using Resource.Models.Web;

namespace Yqblog.Models
{
    public class CategoryLangModel
    {
        public int CateId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Web), ErrorMessageResourceName = "Common_Required_Msg")]
        [Display(ResourceType = typeof(Category), Name = "CateName")]
        public string CateName { get; set; }

        [Display(ResourceType = typeof(Category), Name = "CustomView")]
        [RegularExpression(@"^[a-zA-Z]{1}[a-zA-Z0-9_\-]{2,19}$", ErrorMessageResourceType = typeof(Category), ErrorMessageResourceName = "ReName_Reg_Msg")]
        public string CustomView { get; set; }
    }
}