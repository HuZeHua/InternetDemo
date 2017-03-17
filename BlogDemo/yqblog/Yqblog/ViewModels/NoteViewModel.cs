using Yqblog.Models;

namespace Yqblog.ViewModels
{
    public class NoteViewModel
    {
        public string WebTitle { get; set; }
        public string WebPath { get; set; }
        public string CurrentUrl { get; set; }
        public NoteModel Note { get; set; }
        public NoteListViewModel NoteList { get; set; }
        public string NoteOrderType { get; set; }
        public CategoryModel Category { get; set; }
    }
}
