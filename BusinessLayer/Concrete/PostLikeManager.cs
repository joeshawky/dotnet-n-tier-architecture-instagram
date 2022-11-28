using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete;

public class PostLikeManager : IPostLikeService
{
    private readonly IPostLikeDal _postLikeDal;

    public PostLikeManager(IPostLikeDal postLikeDal)
    {
        _postLikeDal = postLikeDal;
    }

    public List<PostLike> GetList()
    {
        return _postLikeDal.List();
    }
    public List<PostLike> GetListByUser(User user)
    {
        return _postLikeDal.List(p => p.UserId == user.UserId).OrderByDescending(p => p.DateTime).ToList();
    }
    public void Add(PostLike postLike)
    {
        _postLikeDal.Insert(postLike);
    }

    public void Update(PostLike postLike)
    {
        _postLikeDal.Update(postLike);
    }

    public void Delete(PostLike postLike)
    {
        _postLikeDal.Delete(postLike);
    }

    public PostLike? GetById(int postId)
    {
        return _postLikeDal.Get(p => p.PostId == postId);
    }

    public PostLike? GetById(int postId, int UserId)
    {
        return _postLikeDal.Get(p => p.PostId == postId && p.UserId == UserId);
    }

}