using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Yqblog.Data;
using Yqblog.Models;

namespace Yqblog.Services.ServiceImpl
{
    public partial class ServiceImpl
    {
        public List<UserInfoModel> GetUserList()
        {
            return _entity.blog_vuser.Select(ConvertToUserInfoModel).ToList();
        }

        public List<UserInfoModel> GetAdminList()
        {
            return _entity.blog_vuser.Where(m=>m.roleid==2).Select(ConvertToUserInfoModel).ToList();
        }

        public blog_vuser GetUserInfoById(long userid)
        {
            return _entity.blog_vuser.SingleOrDefault(m => m.userid == userid);
        }

        public UserInfoModel GetUserInfo(string username)
        {
            var userinfo = _entity.blog_vuser.SingleOrDefault(m => m.username == username && m.userid>0);

            if (userinfo != null)
            {
                var user = ConvertToUserInfoModel(userinfo);
                user.PostCount = GetArticleCountByUser(username, 5);
                return user;
            }

            return null;
        }

        public blog_users UserExist(string name, string password)
        {
            var query = _entity.blog_users.Where(m => m.username.ToLower() == name.ToLower() && m.userpwd == password);
            return !query.Any() ? null : query.First();
        }

        public bool UserExist(string name)
        {
            return _entity.blog_users.Count(m => m.username.ToLower() == name.ToLower())>0;
        }

        public void UpdateLastLogInDate(long userid)
        {
            var query = _entity.blog_users.FirstOrDefault(m => m.userid == userid);
            if (query == null) return;
            query.lastlogindate = DateTime.Now;
            query.lastactivitydate = DateTime.Now;
            _entity.SaveChanges();
        }

        public void UpdateLastActivityDate(long userid)
        {
            var query = _entity.blog_users.FirstOrDefault(m => m.userid == userid);
            if (query == null) return;
            query.lastactivitydate = DateTime.Now;
            _entity.SaveChanges();
        }

        public long AddUser(UserInfoModel user)
        {
            var role = _entity.blog_roles.FirstOrDefault(x => x.roleid == user.RoleId) ?? AddRole(user.RoleId);
            var dbuser = new blog_users
            {
                username = user.UserName,
                userpwd = user.UserPwd,
                usercreatedate = DateTime.Now,
                lastlogindate = DateTime.Now,
                lastactivitydate = DateTime.Now,
                blog_roles = role,
                userstate = 1
            };

            _entity.blog_users.AddObject(dbuser);
            _entity.SaveChanges();

            return dbuser.userid;
        }

        private blog_roles AddRole(int roleId)
        {
            var role = new blog_roles
                           {
                               roleid = roleId,
                               rolename = roleId == 1 ? "user" : "admin"
                           };
            _entity.blog_roles.AddObject(role);
            _entity.SaveChanges();
            return role;
        }

        public void UpdatePassword(long userid, string newpwd)
        {
            var query = _entity.blog_users.FirstOrDefault(m => m.userid == userid);
            if (query == null) return;
            query.userpwd = newpwd;
            _entity.SaveChanges();
        }

        public void UpdateUserRole(long userid, int roleid)
        {
            var query = _entity.blog_users.FirstOrDefault(m => m.userid == userid);
            if (query == null) return;
            query.blog_roles = _entity.blog_roles.FirstOrDefault(x => x.roleid == roleid);
            _entity.SaveChanges();
        }

        public void UpdateUserState(long userid, int state)
        {
            var query = _entity.blog_users.FirstOrDefault(m => m.userid == userid);
            if (query == null) return;
            query.userstate = state;
            _entity.SaveChanges();
        }

        public void UpdateUserProfile(UserInfoModel user)
        {
            var dbuser = _entity.blog_users.FirstOrDefault(m => m.username == user.UserName);
            if(dbuser==null)
            {  return;}
            var query = _entity.blog_userprofile.FirstOrDefault(m => m.userid == dbuser.userid);
            if (query == null)
            {
                user.UserId = dbuser.userid;
                AddUserProfile(user);
                return;
            }
            query.gender = user.Gender;
            query.nickname = user.NickName;
            query.signature = user.Signature;
            query.intro = user.Intro;
            query.birth = user.Birth;
            query.location = user.Location;
            query.website = user.Website;
            query.qq = user.Qq;
            query.sina = user.Sina;
            query.facebook = user.Facebook;
            query.twitter = user.Twitter;
            query.medals = user.Medals;
            query.phone = user.Phone;
            query.email = user.Email;
            query.isSendEmail = user.IsSendEmail;

            _entity.SaveChanges();
        }

        public void DeleteUser(long userid)
        {
            var query = _entity.blog_users.FirstOrDefault(m => m.userid == userid);

            if (query == null) return;

            using (var scope = new TransactionScope())
            {
                var userprofile = _entity.blog_userprofile.FirstOrDefault(m => m.userid == userid);

                if (userprofile != null)
                {
                    _entity.blog_userprofile.DeleteObject(userprofile);
                    _entity.SaveChanges();
                }

                _entity.blog_users.DeleteObject(query);
                _entity.SaveChanges();

                scope.Complete();
            }
        }

        private void AddUserProfile(UserInfoModel user)
        {
            var dbuserProfile = new blog_userprofile
            {
                userid = user.UserId,
                gender = user.Gender,
                nickname = user.NickName,
                signature = user.Signature,
                intro = user.Intro,
                birth = user.Birth,
                location = user.Location,
                website = user.Website,
                qq = user.Qq,
                sina = user.Sina,
                facebook = user.Facebook,
                twitter = user.Twitter,
                medals = user.Medals,
                phone = user.Phone,
                email = user.Email,
                isSendEmail = user.IsSendEmail
            };

            _entity.blog_userprofile.AddObject(dbuserProfile);
            _entity.SaveChanges();
        }

        private static UserInfoModel ConvertToUserInfoModel(blog_vuser userinfo)
        {
            return new UserInfoModel
            {
                RoleId = userinfo.roleid,
                UserId = userinfo.userid,
                UserName = userinfo.username,
                CreateDate = userinfo.usercreatedate,
                LastLoginDate = userinfo.lastlogindate,
                NickName = userinfo.nickname,
                Signature = userinfo.signature,
                Intro = userinfo.intro,
                Gender = userinfo.gender ?? 0,
                Birth = userinfo.birth,
                Location = userinfo.location,
                Website = userinfo.website,
                Qq = userinfo.qq,
                Sina = userinfo.sina,
                Facebook = userinfo.facebook,
                Twitter = userinfo.twitter,
                Medals = userinfo.medals,
                Phone = userinfo.phone,
                Email = userinfo.email,
                IsSendEmail = userinfo.isSendEmail ?? false
            };
        }
    }
}