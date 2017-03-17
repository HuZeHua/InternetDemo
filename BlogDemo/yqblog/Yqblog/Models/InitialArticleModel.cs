using System;
using Common;
using Yqblog.Data;

namespace Yqblog.Models
{
    public class InitialArticleModel
    {
        public static blog_varticle VArticle()
        {
            var varticle = new blog_varticle
                               {
                                   articleid = 0,
                                   typeid = 0,
                                   cateid = 0,
                                   catepath = string.Empty,
                                   title = string.Empty,
                                   summary = string.Empty,
                                   content = string.Empty,
                                   tags = string.Empty,
                                   layer = 0,
                                   orderid = 1,
                                   parentid = 0,
                                   replypermit = 1,
                                   seodescription = string.Empty,
                                   seokeywords = string.Empty,
                                   seometas = string.Empty,
                                   seotitle = string.Empty,
                                   rename = string.Empty,
                                   status = 1,
                                   userid = 0,
                                   username = string.Empty,
                                   iscommend = 2,
                                   istop = 2,
                                   isindextop = 2,
                                   articletypeid = 0,
                                   viewcount = 0,
                                   createdate = DateTime.Now,
                                   lastreplydate = DateTime.Now,
                                   ip = Utils.GetIp()
                               };

            return varticle;
        }

        public static blog_article Article()
        {
            var article = new blog_article();
            return article;
        }
    }
}