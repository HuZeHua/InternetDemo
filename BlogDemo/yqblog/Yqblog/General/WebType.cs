using System.Collections.Generic;
using System.Linq;
using Yqblog.Models;
using res = Resource.Admin.Admin;

namespace Yqblog.General
{
    public class WebType
    {
        public static int GetTypeId(string key)
        {
            return GetTypeList().First(x => x.Key.Equals(key)).TypeId;
        }

        public static PageType GetType(string key)
        {
            return GetTypeList().First(x=>x.Key.Equals(key));
        }

        public static List<PageType> GetTypeList()
        {
            var items = new List<PageType>
                            {
                                new PageType
                                    {
                                        Key = "article",
                                        TypeName = res.Article,
                                        TypeId = 1,
                                        IsCustomView=true
                                    },
                                new PageType
                                    {
                                        Key = "single",
                                        TypeName = res.SinglePage,
                                        TypeId = 2,
                                        IsCustomView=true
                                    },
                                new PageType
                                    {
                                        Key = "album",
                                        TypeName = res.Album,
                                        TypeId = 4,
                                        IsCustomView=true
                                    },
                                new PageType
                                    {
                                        Key = "note",
                                        TypeName = res.Note,
                                        TypeId = 6,
                                        IsCustomView=true
                                    },
                                new PageType
                                    {
                                        Key = "customHomepageArea",
                                        TypeName = res.CustomArea,
                                        TypeId = 7,
                                        IsCustomView=false
                                    },
                                new PageType
                                    {
                                        Key = "customGlobalArea",
                                        TypeName = res.CustomGlobalArea,
                                        TypeId = 8,
                                        IsCustomView=false
                                    }
                            };
            return items;
        }
    }
}