using AutoMapper;
using BusinessLayer.Concrete;
using BusinessLayer.FluentValidation;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelViews.Concrete;

namespace UiLayerMvc.Controllers
{
    public class PostController : Controller
    {
        private readonly PostManager _postManager;
        private readonly ImageManager _imageManager;
        private readonly UserManager _userManager;
        private readonly CommentManager _commentManager;
        private readonly RiskyCommentManager _riskyCommentManager;

        private readonly PostValidator _postValidator;

        private readonly IMapper _mapper;


        public PostController
            (
                IPostDal postDal,
                IPostLikeDal postLikeDal,
                IPostSaveDal postSaveDal,
                IImageDal imageDal,
                IUserDal userDal,
                ICommentDal commentDal,
                ICommentLikeDal commentLikeDal,
                IRiskyCommentDal riskyCommentDal,
                IMapper mapper,
                IFollowInstanceDal followInstanceDal

            )
        {
            _mapper = mapper;

            _postManager = new PostManager(postDal, postLikeDal, postSaveDal, followInstanceDal, userDal);
            _imageManager = new ImageManager(imageDal);
            _userManager = new UserManager(userDal, followInstanceDal);
            _commentManager = new CommentManager(commentDal, commentLikeDal, riskyCommentDal, mapper);
            _riskyCommentManager = new RiskyCommentManager(riskyCommentDal);

            _postValidator = new PostValidator();
        }



        [Authorize]
        public IActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddPost(PostDto postVM)
        {

            var results = _postValidator.Validate(postVM);

            if (results.IsValid is false)
            {
                results.Errors.ForEach(error => ModelState.AddModelError(error.PropertyName, error.ErrorMessage));

                return View();
            }


            var imagePath = _imageManager.SaveFileToFolder(postVM.ImageFile);

            var post = _mapper.Map<Post>(postVM);
            post.User = _userManager.GetByName(User.Identity.Name);
            post.ImagePath = imagePath;


            _postManager.Add(post);
            return RedirectToAction("Profile", "Account", new { userId = post.UserId });


        }

        public IActionResult Delete(int id)
        {
            var post = _postManager.GetById(id);

            var riskyComment = _riskyCommentManager.GetListByPostId(post.PostId);
            _riskyCommentManager.DeleteList(riskyComment);
            _postManager.Delete(post);
            _imageManager.RemoveFile(post.ImagePath);
            return RedirectToAction("Profile", "Account", new { userId = post.UserId });
        }

        public IActionResult View(int id)
        {
            var post = _postManager.GetById(id);

            if (post is null)
                return BadRequest("Post was not found");


            var comments = _commentManager.GetAllByPostId(id);

            var user = User.Identity.IsAuthenticated ? _userManager.GetByName(User.Identity.Name) : null;

            var postVm = _mapper.Map<PostDisplayViewModel>(post);


            AssignLikeAndSaveIconColors(ref postVm, user, post);


            foreach (var comment in comments)
            {
                var commentVM = _mapper.Map<CommentViewModel>(comment);


                commentVM.HeartColor =
                    user is not null && _commentManager.IsPostLikedByCurrentUser(comment.CommentId, user.UserId)
                        ? Post.RedColor
                        : Post.BlackColor;

                postVm.PostComments.Add(commentVM);
            }

            return View(postVm);
        }

        private void AssignLikeAndSaveIconColors(ref PostDisplayViewModel postVm, User? user, Post post)
        {
            postVm.HeartColor = user is not null && _postManager.IsPostLikedByCurrentUser(post.PostId, user.UserId)
                ? Post.RedColor
                : Post.BlackColor;


            postVm.SaveIconColor = user is not null && _postManager.IsPostSavedByCurrentUser(post.PostId, user.UserId)
                ? Post.RedColor
                : Post.BlackColor;
        }
    }

}
