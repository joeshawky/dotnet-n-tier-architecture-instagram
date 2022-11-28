namespace ModelViews.Concrete
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public string CommentMakerUsername { get; set; }
        public string CommentMakerId { get; set; }
        public string CommentMakerProfilePicturePath { get; set; }
        public string CommentCreationTime { get; set; }
        public int CommentLikes { get; set; } = 0;
        public string HeartColor { get; set; }


    }

}
