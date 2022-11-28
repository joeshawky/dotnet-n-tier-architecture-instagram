using AutoMapper;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using ModelViews.Concrete;

namespace UiLayerMvc.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly CommentManager _commentManager;
    private readonly RiskyCommentManager _riskyCommentManager;
    private readonly UserManager _userManager;

    private readonly IMapper _commentMapper;

    public CommentsController(
        IMapper mapper,
        ICommentDal commentDal,
        ICommentLikeDal commentLikeDal,
        IUserDal userDal,
        IImageDal imageDal,
        IRiskyCommentDal riskyCommentDal
    )
    {
        _commentMapper = mapper;
        _commentManager = new CommentManager(commentDal, commentLikeDal, riskyCommentDal, mapper);
        _userManager = new UserManager(userDal);
        new ImageManager(imageDal);
        _riskyCommentManager = new RiskyCommentManager(riskyCommentDal);
    }

    [HttpPost("PostComment")]
    public ActionResult<int> PostComment(CommentDto commentVM)
    {
        var comment = _commentMapper.Map<Comment>(commentVM);
                

        _commentManager.Add(comment);


        return Ok(_commentManager.GetIdByComment(comment));
    }

    [HttpDelete("RemoveComment")]
    public ActionResult RemoveComment(int commentId, int userId)
    {

        var comment = _commentManager.GetById(commentId);
        var user = _userManager.GetById(userId);

        if (comment is null)
            return BadRequest("Comment not found");

        if (user is null)
            return BadRequest("User not found");

        if (comment.UserId.Equals(user.UserId) is false)
        {
            return BadRequest("User is not creator of comment");
        }

        var riskyComment = _riskyCommentManager.GetByCommentId(commentId);
        if (riskyComment is not null)
        {
            _riskyCommentManager.Delete(riskyComment);
        }

        _commentManager.Delete(comment);
        return Ok("Removed comment successfully");

    }

    [HttpGet("GetCommentInfo")]
    public CommentViewModel GetCommentInfo(int userId, int postId)
    {
        var user = _userManager.GetById(userId);
        var commenterInfo = _commentMapper.Map<CommentViewModel>(user);
        commenterInfo.CommentId = _commentManager.GetByUserPostId(userId, postId).CommentId;

        return commenterInfo;
    }


    [HttpGet("Test")]
    public ActionResult Test()
    {
        Thread.Sleep(5000);
        return Ok("Returned :D");
    }
}