namespace ModelViews.Concrete
{
    public class SuggestedUserModalView

    {
        public string Username { get; set; }
        public string ProfilePicturePath { get; set; }
        public int UserId { get; set; }
        public bool FollowsYou { get; set; } = false;
    }
}
