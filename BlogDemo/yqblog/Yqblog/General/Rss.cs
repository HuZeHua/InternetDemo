using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Yqblog.General
{
    public class Rss : ActionResult
    {
        private readonly string _title;
        private readonly string _desc;
        private readonly Uri _altLink;
        private readonly List<SyndicationItem> _items;

        public Rss(string title, string desc, string link, List<SyndicationItem> items)
        {
            _title = title;
            _desc = desc;
            _altLink = new Uri(link);
            _items = items;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var rss = new SyndicationFeed(
                _title,
                _desc,
                _altLink,
                _items
            );
            var settings = new XmlWriterSettings {Indent = true, NewLineHandling = NewLineHandling.Entitize};
            using (var writer = XmlWriter.Create(context.HttpContext.Response.OutputStream, settings))
            {
                rss.SaveAsAtom10(writer);
            }
        }
    }
}