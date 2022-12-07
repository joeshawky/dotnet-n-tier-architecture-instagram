using AutoMapper;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ModelViews.Concrete;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UiLayerMvc.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly UserManager _userManager;
    private readonly ImageManager _imageManager;
    private readonly FollowInstanceManager _followInstanceManager;

    public UsersController(
        IImageDal imageDal,
        IUserDal userDal,
        IFollowInstanceDal followInstanceDal,
        IMapper mapper
        )
    {
        _mapper = mapper;
        _imageManager = new ImageManager(imageDal);
        _userManager = new UserManager(userDal, followInstanceDal);
        _followInstanceManager = new FollowInstanceManager(followInstanceDal, userDal);
    }


    [HttpGet(nameof(GetFollowers))]
    public IActionResult GetFollowers(string profileUsername, string? loggedInUsername)
    {
        if (_userManager.DoesUserExist(profileUsername) is false)
            return NotFound("user was not found");

        var followInstances = _followInstanceManager.GetFollowersUsernamesForUser(profileUsername);
        var users = _userManager.GetUsersForUsernameList(followInstances);
        var usersVm = users
            .Select(u => _mapper.Map<SuggestedUserModalView>(u))
            .ToList();

        if (loggedInUsername is null)
            return Ok(usersVm);


        usersVm.ForEach(user =>
        {
            user.YouFollowUser = _followInstanceManager.IsUserOneFollowingUsertwo(loggedInUsername, user.Username);
        });
        return Ok(usersVm);

    }
    [HttpGet(nameof(GetFollowings))]
    public IActionResult GetFollowings(string profileUsername, string? loggedInUsername)
    {
        if (_userManager.DoesUserExist(profileUsername) is false)
            return NotFound("user was not found");

        var followInstances = _followInstanceManager.GetFollowingUsernamesForUser(profileUsername);
        var users = _userManager.GetUsersForUsernameList(followInstances);
        var usersVm = users
            .Select(u => _mapper.Map<SuggestedUserModalView>(u))
            .ToList();

        if (loggedInUsername is null)
            return Ok(usersVm);

        usersVm.ForEach(user =>
        {
            user.YouFollowUser = _followInstanceManager.IsUserOneFollowingUsertwo(loggedInUsername, user.Username);
        });

        return Ok(usersVm);
    }

    [HttpGet(nameof(GetLoggedInUsername))]
    public IActionResult GetLoggedInUsername()
    {
        if (User.Identity.IsAuthenticated is false)
            return BadRequest("User not logged in.");

        var user = _userManager.GetByName(User.Identity.Name);

        if (user is null)
            return BadRequest("User not found");

        return Ok(user.Username);
    }


    [HttpPost(nameof(FollowUser))]
    public IActionResult FollowUser(string followerUsername, string otherUsername)
    {
        var otherUserFollowers = _followInstanceManager.GetFollowersUsernamesForUser(otherUsername);

        if (otherUserFollowers.Contains(followerUsername))
            return BadRequest("User already follows user.");

        if (followerUsername == otherUsername)
            return BadRequest("User can not follow himself!");

        var followInstance = new FollowInstance()
        {
            UserId = _userManager.GetUserId(followerUsername),
            FollowedUserId = _userManager.GetUserId(otherUsername)
        };

        _followInstanceManager.Add(followInstance);
        return Ok("Successfully followed user");
    }



    [HttpDelete(nameof(UnFollowUser))]
    public IActionResult UnFollowUser(string followerUsername, string otherUsername)
    {
        var otherUserFollowers = _followInstanceManager.GetFollowersUsernamesForUser(otherUsername);

        if (otherUserFollowers.Contains(followerUsername) == false)
            return BadRequest("User doesnot follow user");

        if (followerUsername == otherUsername)
            return BadRequest("User can not unfollow himself!");


        var followerUserId = _userManager.GetUserId(followerUsername);
        var otherUserId = _userManager.GetUserId(otherUsername);

        var result = _followInstanceManager.RemoveFollowInstance(followerUserId, otherUserId);

        return Ok(result);
    }



    [HttpGet(nameof(GetAllUsers))]
    public IActionResult GetAllUsers()
    {
        var users = _userManager.GetList();
        return Ok(users);
    }


    [HttpGet(nameof(GetCurrentUserInfo))]
    public IActionResult GetCurrentUserInfo()
    {
        if (User.Identity.IsAuthenticated is false)
            return BadRequest("User not logged in.");

        var user = _userManager.GetByName(User.Identity.Name);

        if (user is null)
            return BadRequest("User not found");


        var userDto = new UserDtoWithId()
        {
            UserId = user.UserId,
            Username = user.Username,
            ProfilePicturePath = user.ProfileImage.ImagePath
        };

        return Ok(userDto);
    }


    [HttpGet("GetCurrentUserId")]
    public int? GetCurrentUserId()
    {
        if (User.Identity.IsAuthenticated)
        {
            return _userManager.GetByName(User.Identity.Name).UserId;
        }

        return 0;
    }

}