using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete;

public class CommentLikeManager : ICommentLikeService
{
    private readonly ICommentLikeDal _commentLikeDal;

    public CommentLikeManager(ICommentLikeDal commentLikeDal)
    {
        _commentLikeDal = commentLikeDal;
    }

    public List<CommentLike> GetList()
    {
        return _commentLikeDal.List();
    }
    public List<CommentLike> GetList(User user)
    {
        return _commentLikeDal.List(p => p.UserId == user.UserId);
    }
    public void Add(CommentLike commentLike)
    {
        _commentLikeDal.Insert(commentLike);
    }

    public void Update(CommentLike commentLike)
    {
        _commentLikeDal.Update(commentLike);
    }

    public void Delete(CommentLike commentLike)
    {
        _commentLikeDal.Delete(commentLike);
    }

    public CommentLike? GetById(int commentId)
    {
        return _commentLikeDal.Get(p => p.CommentId == commentId);
    }
    public CommentLike? GetById(int commentId, int userId)
    {
        return _commentLikeDal.Get(p => p.CommentId == commentId && p.UserId == userId);
    }
}