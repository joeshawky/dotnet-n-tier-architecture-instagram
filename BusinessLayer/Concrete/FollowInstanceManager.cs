using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Migrations;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete;

public class FollowInstanceManager : IFollowInstanceService
{
    private readonly IFollowInstanceDal _followInstanceDal;
    private readonly IUserDal _userDal;

    public FollowInstanceManager(IFollowInstanceDal followInstanceDal, IUserDal userDal)
    {
        _followInstanceDal = followInstanceDal;
        _userDal = userDal;
    }
    public void Add(FollowInstance followInstance)
    {
        _followInstanceDal.Insert(followInstance);
    }

    public void Delete(FollowInstance followInstance)
    {
        _followInstanceDal.Delete(followInstance);
    }

    public string RemoveFollowInstance(int followerUserId, int followedUserId)
    {
        var followInstance = _followInstanceDal.Get(f => f.UserId == followerUserId && f.FollowedUserId == followedUserId);
        if (followInstance is null)
            return "No follow instance record was found";

        _followInstanceDal.Delete(followInstance);
        return "Successfully deleted record";
    }

    public bool IsUserOneFollowingUsertwo(int UserOneId, int UserTwoId)
    {
        var instance = _followInstanceDal.Get(f => f.UserId == UserOneId&& f.FollowedUserId == UserTwoId);
        return instance != null;
    }


    public List<string> GetFollowingUsernamesForUser(string username)
    {
        var userId = _userDal.Get(u => u.Username == username).UserId;

        var usersIds = _followInstanceDal
            .List(f => f.UserId == userId)
            .Select(f => f.FollowedUserId).ToList();

        var usernames = _userDal
            .List(u => usersIds.Contains(u.UserId))
            .Select(u => u.Username)
            .ToList();


        return usernames;
    }

    public List<string> GetFollowersUsernamesForUser(string username)
    {

        var userId = _userDal.Get(u => u.Username == username).UserId;

        var usersIds = _followInstanceDal
            .List(f => f.FollowedUserId == userId)
            .Select(f => f.UserId).ToList();

        var usernames = _userDal
            .List(u => usersIds.Contains(u.UserId))
            .Select(u => u.Username)
            .ToList();


        return usernames;

    }


    public List<int> GetFollowingIdsForUser(int userId)
    {
        return _followInstanceDal
            .List(f => f.UserId == userId)
            .Select(f => f.FollowedUserId).ToList();
    }

    public List<int> GetFollowersIdsForUser(int userId)
    {
        return _followInstanceDal
            .List(f => f.FollowedUserId == userId)
            .Select(f => f.UserId).ToList();
    }

    public FollowInstance? GetById(int id)
    {
        return _followInstanceDal.Get(f => f.Id == id);
    }

    public List<FollowInstance> GetList()
    {
        return _followInstanceDal.List();
    }

    public void Update(FollowInstance followInstance)
    {
        _followInstanceDal.Update(followInstance);
    }

}
