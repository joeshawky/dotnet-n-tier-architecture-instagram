namespace ModelViews.Concrete
{
    public class PostDisplayViewModel
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public List<CommentViewModel> PostComments { get; set; }
        public string PostMakerUsername { get; set; }
        public string PostMakerProfilePicturePath { get; set; }
        public string PostImagePath { get; set; }
        public string PostDescription { get; set; }
        public string PostCreationTime { get; set; }
        public int PostLikesCount { get; set; } = 0;
        public int PostCommentsCount { get; set; } = 0;
        public string HeartColor { get; set; } = "#262626";
        public string SaveIconColor { get; set; } = "#262626";



        public PostDisplayViewModel()
        {
            PostComments = new List<CommentViewModel>();
        }
    }
}
