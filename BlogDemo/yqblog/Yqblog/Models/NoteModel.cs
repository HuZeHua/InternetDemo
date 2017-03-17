namespace Yqblog.Models
{
    public class NoteModel
    {
        public long NoteId { get; set; }

        public int ParentId { get; set; }

        public int CategoryId { get; set; }

        public long UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Url { get; set; }

        public string Content { get; set; }

        public string ValidationCode { get; set; }

        public int DataType { get; set; }
    }
}