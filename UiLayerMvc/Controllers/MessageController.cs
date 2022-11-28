using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelViews.Concrete;
using System.Security.Claims;

namespace UiLayerMvc.Controllers;

public class MessageController : Controller
{
    private readonly UserManager _userManager;

	public MessageController(IUserDal userDal)
	{
		_userManager = new UserManager(userDal);
	}

    [Authorize]
    public IActionResult Index()
    {
        var loggedInUserId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value;
        if (loggedInUserId is null)
            throw new InvalidOperationException("User is not logged in correctly.");

        ViewData["userId"] = loggedInUserId;

        var users = _userManager.GetListExceptId(Convert.ToInt32(loggedInUserId));
        var usersChatsBlocks = new List<ChatIconModelView>();

        users.ForEach(u =>
        {
            usersChatsBlocks.Add(new ChatIconModelView()
            {
                ProfilePicturePath = u.ProfileImage.ImagePath,
                UserId = u.UserId,
                Username = u.Username
            });
        });

        return View(usersChatsBlocks);
    }

    public IActionResult Chat()
    {
        return View();
    }
}