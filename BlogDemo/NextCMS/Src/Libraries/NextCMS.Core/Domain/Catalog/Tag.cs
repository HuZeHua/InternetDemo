using System.Collections.Generic;

namespace XCode.Core.Domain.Catalog
{
    public partial class Tag : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// 文章
        /// </summary>

        private ICollection<Article> _articles;
        public virtual ICollection<Article> Articles
        {
            get { return _articles ?? (_articles = new List<Article>()); }
            protected set { _articles = value; }
        }
    }
}
