using System.Security.Claims;
using AutoMapper;
using BusinessLayer.Concrete;
using BusinessLayer.FluentValidation;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelViews.Concrete;

namespace UiLayerMvc.Controllers;

public class AccountController : Controller
{

    private readonly UserManager _userManager;
    private readonly PostManager _postManager;
    private readonly ProfileImageManager _profileImageManager;
    private readonly ImageManager _imageManager;

    private readonly UserValidator _userValidator;
    private readonly EditProfileValidator _editProfileValidator;


    private readonly IMapper _mapper;

    public AccountController(
        IMapper mapper,
        IUserDal userDal,
        IPostDal postDal,
        IPostLikeDal postLikeDal,
        IPostSaveDal postSaveDal,
        IProfileImageDal profileImageDal,
        IImageDal imageDal
    )
    {
        _userManager = new UserManager(userDal);
        _postManager = new PostManager(postDal, postLikeDal, postSaveDal);
        _profileImageManager = new ProfileImageManager(profileImageDal);
        _userValidator = new UserValidator();
        _editProfileValidator = new EditProfileValidator();
        _imageManager = new ImageManager(imageDal);
        _mapper = mapper; ;
    }
    public IActionResult Register()
    {

        return View();
    }

    [HttpPost]
    public IActionResult Register(User user)
    {
        var results = _userValidator.Validate(user);
        if (results.IsValid)
        {
            var profileImage = _profileImageManager.GetFirstImage();

            user.ProfileImage = profileImage;

            _userManager.Add(user);
            return RedirectToAction(nameof(Login));
        }

        foreach (var error in results.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return View();
    }


    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(User user)
    {
        var authenticatedUser = _userManager.AuthenticateUser(user);

        if (authenticatedUser is null)
        {
            ModelState.AddModelError("", "Email or password may be wrong");
            return View(user);
        }

        UserSignIn(authenticatedUser);

        return RedirectToAction("Index", "home");

    }

    [Authorize]
    public IActionResult Logout()
    {
        UserSignOut();
        return RedirectToAction("index", "home");
    }

    [HttpGet]
    [Authorize]
    public IActionResult ProfileEdit(int userId)
    {
        var user = _userManager.GetById(userId);
        var vm = _mapper.Map<EditProfileModelView>(user);

        return View(vm);
    }

    [HttpPost]
    public IActionResult ProfileEdit(EditProfileModelView vm)
    {

        var result = _editProfileValidator.Validate(vm);
        if (result.IsValid == false)
        {
            result.Errors.ForEach(e => ModelState.AddModelError(e.PropertyName, e.ErrorMessage));
            return View(vm);
        }


        var user = _userManager.GetById(vm.UserId);
        user.Description = vm.Description;

        if (user.Username != vm.Username)
        {
            user.Username = vm.Username;
            UserSignOut();
            UserSignIn(user);
        }

        if (vm.ProfilePicture != null)
        {
            var newProfileImage = _mapper.Map<ProfileImage>(vm.ProfilePicture);
            _profileImageManager.Add(newProfileImage);
            user.ProfileImageId = newProfileImage.ProfileImageId;
        }
        _userManager.Update(user);


        return RedirectToAction(nameof(Profile), new { userId = vm.UserId });
    }
    public IActionResult Profile(int userId)
    {
        var user = _userManager.GetById(userId);

        var isOwner = User.Identity.IsAuthenticated && User.Identity.Name.ToLower() == user.Username.ToLower() ? true : false;

        var posts = _postManager.GetList(user);

        var profileVm = _mapper.Map<ProfileViewModel>(user);

        profileVm.Posts = posts.Select(p => _mapper.Map<PostDisplayViewModel>(p)).ToList();


        return isOwner ? View(profileVm) : View(nameof(OtherProfile), profileVm);
    }

    public IActionResult OtherProfile()
    {
        return View();
    }
    private void UserSignOut()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public IActionResult MainProfile()
    {
        var user = _userManager.GetByName(User.Identity.Name);
        return (RedirectToAction(nameof(Profile), new { userId = user.UserId }));

    }



    public IActionResult LikedPosts(int userId)
    {
        var user = _userManager.GetById(userId);

        var isOwner = User.Identity.IsAuthenticated && User.Identity.Name.ToLower() == user.Username.ToLower() ? true : false;

        var posts = _postManager.GetLikedPostsList(user);

        var profileVm = _mapper.Map<ProfileViewModel>(user);

        profileVm.Posts = posts.Select(p => _mapper.Map<PostDisplayViewModel>(p)).ToList();


        if (isOwner)
            return View(profileVm);

        throw new Exception("User is not the owner");
    }

    public IActionResult SavedPosts(int userId)
    {
        var user = _userManager.GetById(userId);

        var isOwner = User.Identity.IsAuthenticated && User.Identity.Name.ToLower() == user.Username.ToLower() ? true : false;

        var posts = _postManager.GetSavedPostsList(user);
        var profileVm = _mapper.Map<ProfileViewModel>(user);

        profileVm.Posts = posts.Select(p => _mapper.Map<PostDisplayViewModel>(p)).ToList();


        if (isOwner)
            return View(profileVm);

        throw new Exception("User is not the owner");
    }




    private void UserSignIn(User user)
    {

        var principal = GenerateClaimsPrincipal(user, new List<string>() { user.UserRole });

        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }

    private static ClaimsPrincipal GenerateClaimsPrincipal(User user, List<string> roles)
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, user.Username));

        roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r)));

        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        ClaimsPrincipal principal = new ClaimsPrincipal(identity);
        return principal;
    }
}