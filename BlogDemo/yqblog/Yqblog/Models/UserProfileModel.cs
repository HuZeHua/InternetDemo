using System;

namespace Yqblog.Models
{
    public class UserProfileModel
    {
        public string NickName{ get; set; }

        public string Signature{ get; set; }

        public string Intro{ get; set; }

        public int Gender{ get; set; }

        public DateTime? Birth{ get; set; }

        public string Location{ get; set; }

        public string Website{ get; set; }

        public string Qq{ get; set; }

        public string Sina{ get; set; }

        public string Facebook{ get; set; }

        public string Twitter{ get; set; }

        public string Medals{ get; set; }

        public string Phone{ get; set; }

        public string Email{ get; set; }

        public bool IsSendEmail { get; set; }
    }
}