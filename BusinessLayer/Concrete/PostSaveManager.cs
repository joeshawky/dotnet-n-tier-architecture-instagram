using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete;

public class PostSaveManager : IPostSaveService
{
    private readonly IPostSaveDal _postSaveDal;

    public PostSaveManager(IPostSaveDal postSaveDal)
    {
        _postSaveDal = postSaveDal;
    }

    public List<PostSave> GetList()
    {
        return _postSaveDal.List();
    }
    public List<PostSave> GetList(User user)
    {
        return _postSaveDal.List(p => p.UserId == user.UserId);
    }
    public void Add(PostSave postLike)
    {
        _postSaveDal.Insert(postLike);
    }

    public void Update(PostSave postLike)
    {
        _postSaveDal.Update(postLike);
    }

    public PostSave? GetById(int postSaveId)
    {
        return _postSaveDal.Get(p => p.PostId == postSaveId);
    }

    public PostSave? GetById(int postId, int userId)
    {
        return _postSaveDal.Get(p => p.PostId == postId && p.UserId == userId);
    }

    public List<PostSave> GetListByUser(User user)
    {
        return _postSaveDal.List(u => u.UserId == user.UserId).OrderByDescending(p => p.DateTime).ToList();
    }


    public void Delete(PostSave postLike)
    {
        _postSaveDal.Delete(postLike);
    }


}