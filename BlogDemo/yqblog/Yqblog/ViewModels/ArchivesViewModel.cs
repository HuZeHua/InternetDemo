using Yqblog.General;

namespace Yqblog.ViewModels
{
    public class ArchivesViewModel
    {
        public string WebTitle { get; set; }
        public string WebPath { get; set; }
        public string CurrentUrl { get; set; }
        public Pager ArticlePagerInfo { get; set; }
    }
}
