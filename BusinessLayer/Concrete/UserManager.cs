using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete;

public class UserManager : IUserService
{
    private readonly IUserDal _userDal;
    private readonly FollowInstanceManager _followInstanceManager;

    public UserManager(IUserDal userDal, IFollowInstanceDal followInstanceDal)
    {
        _userDal = userDal;
        _followInstanceManager = new FollowInstanceManager(followInstanceDal, userDal);
    }

    public List<User> GetListExceptId(int userId)
    {
        return _userDal.List(u => u.UserId != userId);
    }

    public List<User> GetList()
    {
        return _userDal.List();
    }

    public bool DoesUserExist(string username)
    {
        return _userDal.List(u => u.Username == username).Any();
    }
    public List<string> GetUsernames(List<int> userids)
    {
        var usernames = new List<string>();
        userids.ForEach(u =>
        {
            usernames.Add(_userDal.Get(ud => ud.UserId == u).Username);
        });

        return usernames;
    }
    public int GetUserId(string username)
    {
        var user = _userDal.Get(u => u.Username == username);
        if (user is null)
            throw new Exception("User not found");

        return user.UserId;
    }

    public List<User> GetUsersForUsernameList(List<string> usernames)
    {
        var users = new List<User>();
        usernames.ForEach(u =>
        {
            var user = _userDal.Get(user => user.Username == u);

            if (user is null)
                throw new Exception("User not found");

            users.Add(user);
        });


        return users;
    }
    public List<User> GetFirstFiveExceptUser(int userId)
    {
        var followingIds = _followInstanceManager.GetFollowingIdsForUser(userId);
        return _userDal
            .List(u => u.UserId != userId && followingIds.Contains(u.UserId) == false)
            .Take(5)
            .ToList();
    }
    public void Add(User user)
    {
        _userDal.Insert(user);
    }

    public void Update(User user)
    {
        _userDal.Update(user);
    }

    public void Delete(User user)
    {
        _userDal.Delete(user);
    }

    public User? GetById(int userId)
    {
        return _userDal.Get(u => u.UserId == userId);
    }

    public User? GetByName(string name)
    {
        return _userDal.Get(u => u.Username == name);
    }

    public User? AuthenticateUser(User user)
    {
        return _userDal
            .Get(u => u.Username == user.Username && u.Password == user.Password);
    }
}