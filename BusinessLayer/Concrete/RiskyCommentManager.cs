using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete;

public class RiskyCommentManager : IRiskyCommentService
{
    private readonly IRiskyCommentDal _riskyCommentDal;

    public RiskyCommentManager(IRiskyCommentDal riskyCommentDal)
    {
        _riskyCommentDal = riskyCommentDal;

    }

    public List<RiskyComment> GetListByPostId(int postId)
    {
        return _riskyCommentDal.List(c => c.Comment.PostId == postId);
    }
    public List<RiskyComment> GetList()
    {
        return _riskyCommentDal.List();

    }
    public List<RiskyComment> GetListOrderedByDate()
    {
        return _riskyCommentDal.List().OrderBy(c => c.DateTime).ToList();
    }

    public List<RiskyComment> GetListOrderedByDateDescending()
    {
        return _riskyCommentDal.List().OrderByDescending(c => c.DateTime).ToList();
    }

    public void Add(RiskyComment t)
    {
        _riskyCommentDal.Insert(t);
    }
    public bool RiskyCommentExists(int riskyCommentId)
    {
        var result = _riskyCommentDal.Get(c => c.RiskyCommentId == riskyCommentId);
        return result != null;
    }
    public void DeleteList(List<RiskyComment> riskyComments)
    {
        foreach (var riskyComment in riskyComments)
        {
            if (RiskyCommentExists(riskyComment.RiskyCommentId))
                _riskyCommentDal.Delete(riskyComment);
        }
    }
    public void Delete(RiskyComment t)
    {
        _riskyCommentDal.Delete(t);
    }

    public void Update(RiskyComment t)
    {
        _riskyCommentDal.Update(t);
    }

    public RiskyComment? GetById(int id)
    {
        return _riskyCommentDal.Get(c => c.RiskyCommentId == id);
    }

    public RiskyComment? GetByCommentId(int commentId)
    {
        return _riskyCommentDal.Get(c => c.CommentId == commentId);
    }
}