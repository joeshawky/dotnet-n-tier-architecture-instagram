namespace ModelViews.Concrete;

public class ProfileViewModel
{
    public int UserId { get; set; }
    public string Username { set; get; }
    public string UserProfilePicturePath { get; set; }
    public int PostCount { get; set; }
    public string Description { get; set; }
    public bool Following { get; set; } = false;
    public int FollowersCount { get; set; } = 0;
    public int FollowingCount { get; set; } = 0;

    public List<PostDisplayViewModel> Posts { get; set; }

    public ProfileViewModel()
    {
        Posts = new List<PostDisplayViewModel>();
    }
}