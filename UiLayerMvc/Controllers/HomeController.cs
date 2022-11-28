using System.Diagnostics;
using AutoMapper;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using ModelViews.Concrete;
using UiLayerMvc.Models;

namespace UiLayerMvc.Controllers;

public class HomeController : Controller
{
    private readonly PostManager _postManager;
    private readonly UserManager _userManager;

    private readonly IMapper _mapper;

        
    public HomeController(
        IMapper mapper,
        IPostDal postDal,
        IPostLikeDal postLikeDal,
        IPostSaveDal postSaveDal,
        IUserDal userDal)
    {
        _mapper = mapper;
        _postManager = new PostManager(postDal, postLikeDal, postSaveDal);
        _userManager = new UserManager(userDal);
    }

    public IActionResult Homepage()
    {
        var userId = GetUserId(); // returns 0 if user is not signed in.

        var posts = _postManager.GetList();

        var homePagePostsVm = new HomePageViewModel()
        {
            UserId = userId
        };

        var suggestedUsers = _userManager.GetFirstFiveExceptUser(userId);
        homePagePostsVm.SuggestedUsers = suggestedUsers.Select(user => _mapper.Map<SuggestedUserModalView>(user)).ToList();


        foreach (var post in posts)
        {
            var homepagePost = _mapper.Map<PostDisplayViewModel>(post);
            homepagePost.HeartColor = _postManager.IsPostLikedByCurrentUser(post.PostId, userId) ? Post.RedColor : Post.BlackColor;
            homepagePost.SaveIconColor = _postManager.IsPostSavedByCurrentUser(post.PostId, userId) ? Post.RedColor : Post.BlackColor;
            homePagePostsVm.Posts.Add(homepagePost);
                
        }


        return View(homePagePostsVm);
    }

    private int GetUserId()
    {
        return User.Identity.IsAuthenticated ? _userManager.GetByName(User.Identity.Name).UserId : 0;
    }

    public IActionResult Index()
    {
        return RedirectToAction(nameof(Homepage));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}