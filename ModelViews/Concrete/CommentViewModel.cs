namespace ModelViews.Concrete
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; } = string.Empty;
        public string CommentMakerUsername { get; set; } = string.Empty;
        public string CommentMakerId { get; set; } = string.Empty;
        public string CommentMakerProfilePicturePath { get; set; } = string.Empty;
        public string CommentCreationTime { get; set; } = string.Empty;
        public int CommentLikes { get; set; } = 0;
        public string HeartColor { get; set; } = string.Empty;


    }

}
