using AutoMapper;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace UiLayerMvc.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class LikesController : ControllerBase
{
    private readonly PostManager _postManager;
    private readonly CommentManager _commentManager;


    public LikesController
    (
        IPostDal postdal,
        IPostLikeDal postLikeDal,
        IPostSaveDal postSaveDal,
        ICommentDal commentDal,
        ICommentLikeDal commentLikeDal,
        IRiskyCommentDal riskyCommentDal,
        IMapper mapper
    )
    {
        _postManager = new PostManager(postdal, postLikeDal, postSaveDal);
        _commentManager = new CommentManager(commentDal, commentLikeDal, riskyCommentDal, mapper);
    }



    [HttpPost("CommentLike")]
    public IActionResult CommentLike(int commentId, int userId)
    {
        var result = _commentManager.CommentLike(commentId, userId);
        return Ok(result);

    }


    [HttpPost("PostLike")]
    public IActionResult PostLike(int postId, int userId)
    {
        var result = _postManager.PostLike(postId, userId);
        return Ok(result);

    }

}