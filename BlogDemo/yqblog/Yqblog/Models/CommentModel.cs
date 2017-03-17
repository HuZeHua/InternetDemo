namespace Yqblog.Models
{
    public class CommentModel
    {
        public long CommentId { get; set; }

        public long ParentId { get; set; }

        public long CommentArticleId { get; set; }

        public byte ReplyPermit { get; set; }

        public long UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Url { get; set; }

        public string Content { get; set; }

        public string ValidationCode { get; set; }

        public int DataType { get; set; }
    }
}