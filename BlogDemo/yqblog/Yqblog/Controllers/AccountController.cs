using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Common;
using Yqblog.Data;
using Yqblog.General;
using Yqblog.Models;
using Yqblog.ViewModels;
using res = Resource.Web;

namespace Yqblog.Controllers
{
    [WebFilter]
    public class AccountController : BaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CheckLoginCookie();
            if (Session["user"] as blog_users == null
                && filterContext.ActionDescriptor.ActionName.ToLower() != "logon"
                && filterContext.ActionDescriptor.ActionName.ToLower() != "register"
                && filterContext.ActionDescriptor.ActionName.ToLower() != "ajaxlogoninfo"
                && filterContext.ActionDescriptor.ActionName.ToLower() != "uview")
            {
                filterContext.Result = new RedirectResult("~/account/logon", true);
            }
            base.OnActionExecuting(filterContext);
        }

        public ActionResult Manage()
        {
            DataCache.RemoveCache("CategoriesInfoCache");
            var rn = DateTime.Now.ToString("hhmmss");
            return RedirectToRoute("Admin_default", new { controller = "Admin", action = "AdminArticle", rn});
        }

        public ActionResult AjaxLogonInfo()
        {
            return PartialView("_UserAndLangInfo");
        }

        public ActionResult LogOn()
        {
            ViewBag.ReturnUrl = WebUtils.MyString(Request["ReturnUrl"])!=""?Request["ReturnUrl"]:(Request.UrlReferrer==null?"":Request.UrlReferrer.ToString());
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            var re=string.Empty;
            if (ModelState.IsValid)
            {
                var md5Pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password, "md5");
                var user = MyService.UserExist(model.UserName.Trim(), md5Pwd);
                if (user == null)
                {
                    re = res.LogOn_UsernameOrPwdError;
                }
                if (model.RememberMe)
                {
                    var cUser = new HttpCookie("cUser", model.UserName.Trim());
                    var cPwd = new HttpCookie("cPwd", md5Pwd);
                    cUser.Expires = DateTime.Now.AddYears(10);
                    cPwd.Expires = DateTime.Now.AddYears(10);
                    Response.Cookies.Add(cUser);
                    Response.Cookies.Add(cPwd);
                }
                Session.Add("user", user);
            }
            else
            {
                re = res.SysError;
            }
            return Content(re, "text/html;charset=UTF-8");
        }

        public ActionResult Register()
        {
            ViewBag.ReturnUrl = WebUtils.MyString(Request["ReturnUrl"]) != "" ? Request["ReturnUrl"] : (Request.UrlReferrer == null ? "" : Request.UrlReferrer.ToString());
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            var re = string.Empty;
            if (ModelState.IsValid)
            {
                if (!MyService.UserExist(model.UserName.Trim()))
                {
                    var user=new UserInfoModel
                                           {
                                               UserName = model.UserName.Trim(),
                                               UserPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password, "md5"),
                                               RoleId = GetRoleId(model.UserName)
                                           };
                    MyService.AddUser(user);
                    Session.Add("user", MyService.UserExist(user.UserName, user.UserPwd));
                }
                else
                {
                    re = res.Register_UsernameExists;
                }
            }
            else
            {
                re = res.SysError;
            }
            return Content(re, "text/html;charset=UTF-8");
        }

        public ActionResult LogOff()
        {
            var langPath = Resource.Web.Lang == Configinfo.DefaultLang ? "" : Resource.Web.Lang+"_";
            if (Session["user"] != null)
            {
                Session.Remove("user");
                ClearLoginCookie();
            }
            return RedirectToRoute(langPath+"Index");
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            var re = string.Empty;
            if (ModelState.IsValid)
            {
                var user = MyService.UserExist(UserInfo.username, FormsAuthentication.HashPasswordForStoringInConfigFile(model.OldPassword, "md5"));
                if (user != null)
                {
                    MyService.UpdatePassword(user.userid, FormsAuthentication.HashPasswordForStoringInConfigFile(model.NewPassword, "md5"));
                }
                else
                {
                    re = res.OldPasswordWrong;
                }
            }
            else
            {
                re = "error";
            }
            return Content(re, "text/html;charset=UTF-8");
        }

        public ActionResult UView(string user)
        {
            var port = Request.Url.Port;
            var applicationPath = Request.ApplicationPath != "/" ? Request.ApplicationPath : string.Empty;
            var uid = user;
            var localhost = string.Format("{0}://{1}{2}{3}",
                                             Request.Url.Scheme,
                                             Request.Url.Host,
                                             (port == 80 || port == 0) ? "" : ":" + port,
                                             applicationPath);
            ViewBag.Localhost = localhost;
            var userinfo = MyService.GetUserInfo(uid);
            return userinfo!=null ? View(userinfo) : View("Error");
        }

        public ActionResult UCenter()
        {
            var port = Request.Url.Port;
            var applicationPath = Request.ApplicationPath != "/" ? Request.ApplicationPath : string.Empty;
            var uname = UserInfo.username;
            var localhost = string.Format("{0}://{1}{2}{3}",
                                             Request.Url.Scheme,
                                             Request.Url.Host,
                                             (port == 80 || port == 0) ? "" : ":" + port,
                                             applicationPath);
            var encodeLocalhost = HttpUtility.UrlEncode(localhost);
            var avatarFlashParam = string.Format("{0}/upload/avatar/common/camera.swf?nt=1&inajax=1&appid=1&input={1}&ucapi={2}/ajaxavatar.ashx", localhost, uname, encodeLocalhost);

            var userinfo = MyService.GetUserInfo(uname);
            var uCenterViewModel = new UCenterViewModel
                                       {
                                           Localhost = localhost,
                                           AvatarFlashParam = avatarFlashParam,
                                           GenderInfo = WebUtils.GetGenderList().Find(item => item.Value == userinfo.Gender.ToString()).Key,
                                           GenderList = new SelectList(WebUtils.GetGenderList(), "Value", "Key", userinfo.Gender.ToString()),
                                           UserInfo = MyService.GetUserInfo(uname),
                                           IsSendEmail = Configinfo.IfSendReplyEmail==1
                                       };
            return View(uCenterViewModel);
        }

        [HttpPost]
        public ActionResult UserProfile(UserProfileModel model)
        {
            var userinfo = new UserInfoModel
                {
                    UserName = UserInfo.username,
                    NickName = model.NickName,
                    Signature = model.Signature,
                    Intro = model.Intro,
                    Gender = model.Gender,
                    Birth = model.Birth,
                    Location = model.Location,
                    Website = model.Website,
                    Qq = model.Qq,
                    Sina = model.Sina,
                    Facebook = model.Facebook,
                    Twitter = model.Twitter,
                    Medals = model.Medals,
                    Phone = model.Phone,
                    Email = model.Email,
                    IsSendEmail = model.IsSendEmail
                };
            MyService.UpdateUserProfile(userinfo);
            return Content("", "text/html;charset=UTF-8");
        }

        public ActionResult UserCommentList(int p,int size=20)
        {
            var pager = new Pager { PageNo = p, PageSize = size };
            pager = MyService.GetReplyPaging(pager, 1, 0, user: UserInfo.userid);
            var commentListViewModel = new CommentListViewModel
            {
                CommentPagerInfo = pager
            };
            return PartialView("_UserComment", commentListViewModel);
        }

        public ActionResult UserNoteList(int p, int size = 20)
        {
            var pager = new Pager {PageNo = p, PageSize = size};
            pager = MyService.GetArticlePaging(pager, 6, 0, user: UserInfo.userid);
            var noteListViewModel = new NoteListViewModel
            {
                NotePagerInfo = pager
            };
            if (pager.Amount>0)
            {
                var varticle = (pager.Entity as IQueryable<blog_varticle>).First();
                var category = MyService.GetCategoryById(varticle.cateid);
                ViewBag.NoteUrl = WebUtils.GetCateUrl(category);
            }
            return PartialView("_UserNote", noteListViewModel);
        }

        [NonAction]
        private static int GetRoleId(string username)
        {
            return username.Trim().ToLower() != "admin" ? 1 : 2;
        }
    }
}
