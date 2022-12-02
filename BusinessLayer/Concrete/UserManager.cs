using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete;

public class UserManager : IUserService
{
    private readonly IUserDal _userDal;

    public UserManager(IUserDal userDal)
    {
        _userDal = userDal;
    }

    public List<User> GetListExceptId(int userId)
    {
        return _userDal.List(u => u.UserId != userId);
    }

    public List<User> GetList()
    {
        return _userDal.List();
    }
    public List<User> GetFirstFiveExceptUser(int userId)
    {
        return _userDal
            .List(u => u.UserId != userId)
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