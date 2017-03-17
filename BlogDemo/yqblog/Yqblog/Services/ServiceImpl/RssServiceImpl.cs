using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using Common;
using Yqblog.Data;
using Yqblog.General;
using Yqblog.Models;

namespace Yqblog.Services.ServiceImpl
{
    public partial class ServiceImpl
    {
        public List<SyndicationItem> GetRss(List<blog_varticle> list)
        {
            var ret = new List<SyndicationItem>();
            foreach (var r in list)
            {
                var synObj = new SyndicationItem
                                 {
                                     Title = SyndicationContent.CreatePlaintextContent(r.title),
                                     Summary =
                                         SyndicationContent.CreatePlaintextContent(
                                             Utils.CleanInvalidXmlChars(r.summary)),
                                     Content =
                                         SyndicationContent.CreateHtmlContent(
                                             Utils.CleanInvalidXmlChars(r.content))
                                 };
                synObj.Links.Add(new SyndicationLink(new Uri(WebUtils.GetYqUrl(r.url,r.typeid))));
                synObj.PublishDate = new DateTimeOffset(r.createdate);
                synObj.LastUpdatedTime = new DateTimeOffset(r.createdate);
                ret.Add(synObj);
            }
            return ret;
        }

        public List<SyndicationItem> GetCommentRss(List<blog_varticle> list)
        {
            var ret = new List<SyndicationItem>();

            foreach (var r in list)
            {
                var synObj = new SyndicationItem
                                 {
                                     Title = SyndicationContent.CreatePlaintextContent("Re:" + r.title),
                                     Summary = SyndicationContent.CreatePlaintextContent(r.summary),
                                     Content =
                                         SyndicationContent.CreateHtmlContent(
                                             Utils.CleanInvalidXmlChars(r.content) +
                                             "<p style=\"text-align:right;\">" + r.username + " @ " +
                                             r.lastreplydate + "</p>")
                                 };
                synObj.Links.Add(new SyndicationLink(new Uri(WebUtils.GetYqUrl(r.url,r.typeid))));
                synObj.PublishDate = new DateTimeOffset(r.createdate);
                synObj.LastUpdatedTime = new DateTimeOffset(r.createdate);
                ret.Add(synObj);
            }
            return ret;
        }

        public List<TagInfo> GetTagList(int tid, int count)
        {
            var articles = GetVArticles(tid, 0, 0);
            var tagstr = Enumerable.Aggregate(articles, "", (current, article) => current + (article.tags.Trim() + ","));

            var arrTag = tagstr.Split(',');
            arrTag = Utils.DistinctStringArray(arrTag);

            var tags = (from a in arrTag where a.Trim() != "" select new TagInfo {Tag = a.Trim(), Count = Utils.RegexCount(a, tagstr)}).ToList();
            tags.Sort((x, y) => y.Count - x.Count);

            if (count > 0)
            {  tags = tags.Take(count).ToList();}

            return tags;
        }
    }
}