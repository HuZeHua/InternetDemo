using System.Web.Mvc;
using Yqblog.Models;

namespace Yqblog.ViewModels
{
    public class UCenterViewModel
    {
        public UserInfoModel UserInfo { get; set; }
        public string Localhost { get; set; }
        public string AvatarFlashParam { get; set; }
        public string GenderInfo { get; set; }
        public bool IsSendEmail { get; set; }
        public SelectList GenderList { get; set; }
    }
}
