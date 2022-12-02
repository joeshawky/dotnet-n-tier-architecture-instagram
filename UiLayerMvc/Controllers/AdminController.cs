using AutoMapper;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModelViews.Concrete;
using System.Security.Claims;

namespace UiLayerMvc.Controllers;



[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    private readonly RiskyCommentManager _riskyCommentManager;

    private readonly IMapper _mapper;

    private readonly CommentManager _commentManager;
    private readonly UserManager _userManager;

    public AdminController(
        IMapper mapper,
        IRiskyCommentDal riskyCommentDal,
        ICommentDal commentDal,
        ICommentLikeDal commentLikeDal,
        IUserDal userDal)
    {
        _mapper = mapper;
        _riskyCommentManager = new RiskyCommentManager(riskyCommentDal);
        _commentManager = new CommentManager(commentDal, commentLikeDal, riskyCommentDal, mapper);
        _userManager = new UserManager(userDal);

    }

    public void SetUserInfo()
    {

        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var user = _userManager.GetById(userId);

        var userVm = _mapper.Map<UserDto>(user);

        ViewData["username"] = userVm.Username;

        ViewData["profilePicturePath"] = userVm.ProfilePicturePath;

    }

    public IActionResult Index()
    {
        SetUserInfo();

        return RedirectToAction(nameof(MainPage));
    }

    public IActionResult MainPage()
    {
        SetUserInfo();

        return View();
    }

    public IActionResult Comments()
    {
        SetUserInfo();

        var riskyComments = _riskyCommentManager.GetList();

        var riskyCommentsVm = new List<RiskyCommentViewModel>();

        riskyComments.ForEach(rc =>
        {
            riskyCommentsVm.Add(_mapper.Map<RiskyCommentViewModel>(rc));

        });

        return View(riskyCommentsVm);
    }


    public IActionResult Roles()
    {
        SetUserInfo();

        var rolesDropdownList = new List<SelectListItem>()
        {
            
            new SelectListItem(){Text = "", Value = ""},
            new SelectListItem(){Text = "Admin", Value = "admin"},
            new SelectListItem(){Text = "user", Value = "user"}
        };
        ViewData["RolesSelectItems"] = rolesDropdownList;

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
            return BadRequest("user id is null");


        var users = _userManager.GetListExceptId(Convert.ToInt32(userId));

        var usersDto = new List<UserDto>();
        users.ForEach(u =>
        {
            usersDto.Add(_mapper.Map<UserDto>(u));
        });


        return View(usersDto);
    }



    public IActionResult ListOrderedByDate()
    {
        SetUserInfo();


        var riskyComments = _riskyCommentManager.GetListOrderedByDate();

        var riskyCommentsVm = new List<RiskyCommentViewModel>();

        riskyComments.ForEach(rc =>
        {
            riskyCommentsVm.Add(_mapper.Map<RiskyCommentViewModel>(rc));

        });


        return View(nameof(Comments), riskyCommentsVm);
    }



    public IActionResult ListOrderedByDateDecreasingly()
    {
        SetUserInfo();

        var riskyComments = _riskyCommentManager.GetListOrderedByDateDescending();

        var riskyCommentsVm = new List<RiskyCommentViewModel>();

        riskyComments.ForEach(rc =>
        {
            riskyCommentsVm.Add(_mapper.Map<RiskyCommentViewModel>(rc));

        });


        return View(nameof(Comments), riskyCommentsVm);
    }


    public IActionResult RemoveComment(int commentId)
    {
        try
        {
            _riskyCommentManager.Delete(_riskyCommentManager.GetByCommentId(commentId));
            _commentManager.Delete(_commentManager.GetById(commentId));
        }
        catch (Exception ex)
        {

            return BadRequest("couldn't find comment to delete." + ex);
        }

        return RedirectToAction(nameof(Comments));
    }

    public IActionResult ApproveComment(int commentId)
    {

        try
        {
            _riskyCommentManager.Delete(_riskyCommentManager.GetByCommentId(commentId));

        }
        catch (Exception ex)
        {
            return BadRequest("couldn't find comment to delete." + ex);

        }

        return RedirectToAction(nameof(Comments));

    }
}