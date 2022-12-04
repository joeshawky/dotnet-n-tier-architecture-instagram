using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace UiLayerMvc.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class SavesController : ControllerBase
{
    private readonly PostManager _postManager;

    public SavesController(
        IPostDal postDal,
        IPostLikeDal postLikeDal,
        IPostSaveDal postSaveDal,
        IFollowInstanceDal followInstanceDal,
        IUserDal userDal)
    {
        _postManager = new PostManager(postDal, postLikeDal, postSaveDal, followInstanceDal, userDal);
    }

    [HttpPost("PostSave")]
    public IActionResult CommentLike(int postId, int userId)
    {
        var result = _postManager.PostSave(postId, userId);
        return Ok(result);

    }

}