using System.ComponentModel.DataAnnotations;
using Resource.Models.Article;
using Resource.Models.Web;

namespace Yqblog.Models
{
    public class ExtensionField
    {
        public string Lang { get; set; }
        public int Typeid { get; set; }
        public int Cateid { get; set; }

        [Display(ResourceType = typeof (Article), Name = "Field")]
        public string Field { get; set; }

        [Required(ErrorMessageResourceType = typeof (Web), ErrorMessageResourceName = "Common_Required_Msg")]
        [Display(ResourceType = typeof (Article), Name = "FieldRename")]
        public string Rename { get; set; }
    }
}