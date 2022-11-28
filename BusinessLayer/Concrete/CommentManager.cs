using BusinessLayer.Abstract;
using AutoMapper;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete;

public class CommentManager : ICommentService
{
    private readonly ICommentDal _commentDal;
    private readonly CommentLikeManager _commentLikeManager;
    private readonly IRiskyCommentDal _riskyCommentDal;
    private readonly IMapper _mapper;
    public CommentManager(ICommentDal commentDal, ICommentLikeDal commentLikeDal, IRiskyCommentDal riskyCommentDal, IMapper mapper)
    {
        _commentDal = commentDal;
        _commentLikeManager = new CommentLikeManager(commentLikeDal);
        _riskyCommentDal = riskyCommentDal;
        _mapper = mapper;

    }

    public List<Comment> GetList()
    {
        return _commentDal.List();
    }
    public void Add(Comment comment)
    {

        _commentDal.Insert(comment);
        var riskyComment = _mapper.Map<RiskyComment>(comment);
        _riskyCommentDal.Insert(riskyComment);


    }

    public int GetIdByComment(Comment comment)
    {
        var resultComment = _commentDal.List(c =>
            c.UserId == comment.UserId &&
            c.PostId == comment.PostId &&
            c.CommentText == comment.CommentText).Last();


        return resultComment.CommentId; //! suppressed null reference in compiler :)
    }
    public void Update(Comment comment)
    {
        _commentDal.Update(comment);
        //_riskyCommentDal.Update(_mapper.Map<RiskyComment>(comment));

    }

    public void Delete(Comment comment)
    {
        //_riskyCommentDal.Delete(_mapper.Map<RiskyComment>(comment));

        _commentDal.Delete(comment);

    }

    public Comment? GetByUserPostId(int userId, int postId)
    {
        var comments = _commentDal.List(c => c.UserId == userId && c.PostId == postId);
        return comments.MaxBy(c => c.DateTime);
    }
    public Comment? GetById(int id)
    {
        return _commentDal.Get(c => c.CommentId == id);
    }
    public Comment? GetByPostId(int postId)
    {
        return _commentDal.Get(c => c.PostId == postId);
    }
    public List<Comment> GetAllByPostId(int postId)
    {
        return _commentDal.List(c => c.PostId == postId);
    }


    public string CommentLike(int commentId, int userId)
    {
        var commentLikedByUser = _commentLikeManager.GetById(commentId, userId);
        var comment = GetById(commentId);

        if (commentLikedByUser is null)
        {
            _commentLikeManager.Add(new CommentLike()
            {
                CommentId = commentId,
                UserId = userId
            });

            comment.Likes++;
            Update(comment);
            return "Comment Liked";
        }


        comment.Likes--;
        Update(comment);
        _commentLikeManager.Delete(commentLikedByUser);


        return "Comment Disliked";

    }

    public bool IsPostLikedByCurrentUser(int postId, int userId)
    {
        if (_commentLikeManager.GetById(postId, userId) == null)
            return false;

        return true;
    }

}