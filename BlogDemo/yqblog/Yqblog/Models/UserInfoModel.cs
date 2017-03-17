using System;

namespace Yqblog.Models
{
    public class UserInfoModel : UserProfileModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public int RoleId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastActivityDate { get; set; }
        public int PostCount { get; set; }
        public bool IsOnline { get; set; }
    }
}