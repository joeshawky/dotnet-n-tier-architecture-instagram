namespace ModelViews.Concrete
{
    public class RiskyCommentViewModel
    {
        public int Index { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int CommentId { get; set; }

        public string Username { get; set; } = string.Empty;
        public string CommentText { get; set; } = string.Empty;
        public DateTime CommentDatetime { get; set; }

    }
}
