namespace ModelViews.Concrete
{
    public class HomePageViewModel
    {
        public int? UserId { get; set; } = null;
        public List<PostDisplayViewModel> Posts { get; set; }
        public List<SuggestedUserModalView> SuggestedUsers { get; set; }
        public HomePageViewModel()
        {
            SuggestedUsers = new List<SuggestedUserModalView>();
            Posts = new List<PostDisplayViewModel>();
        }
    }
}
