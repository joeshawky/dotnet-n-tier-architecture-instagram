using AutoMapper;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using ModelViews.Concrete;

namespace UiLayerMvc.Controllers;

public class AdminController : Controller
{
    private readonly RiskyCommentManager _riskyCommentManager;

    private readonly IMapper _commentMapper;

    private readonly CommentManager _commentManager;

    public AdminController(
        IMapper mapper,
        IRiskyCommentDal riskyCommentDal,
        ICommentDal commentDal,
        ICommentLikeDal commentLikeDal)
    {
        _commentMapper = mapper;
        _riskyCommentManager = new RiskyCommentManager(riskyCommentDal);
        _commentManager = new CommentManager(commentDal, commentLikeDal, riskyCommentDal, mapper);
    }

    public IActionResult Index()
    {
        return RedirectToAction(nameof(MainPage));
    }

    public IActionResult MainPage()
    {
        return View();
    }

    public IActionResult Comments()
    {

        var riskyComments = _riskyCommentManager.GetList();

        var riskyCommentsVm = AddIndexesToRiskyComments(riskyComments);

        return View(riskyCommentsVm);
    }


    public IActionResult Roles()
    {
        return View();
    }



    public IActionResult ListOrderedByDate()
    {
        var riskyComments = _riskyCommentManager.GetListOrderedByDate();

        var riskyCommentsVm = AddIndexesToRiskyComments(riskyComments);


        return View(nameof(Comments), riskyCommentsVm);
    }



    public IActionResult ListOrderedByDateDecreasingly()
    {
        var riskyComments = _riskyCommentManager.GetListOrderedByDateDescending();

        var riskyCommentsVm = AddIndexesToRiskyComments(riskyComments);


        return View(nameof(Comments), riskyCommentsVm);
    }

    private List<RiskyCommentViewModel> AddIndexesToRiskyComments(List<RiskyComment> riskyComments)
    {
        var riskyCommentsVm = new List<RiskyCommentViewModel>();

        for (int i = 0; i < riskyComments.Count; i++)
        {
            var tempRiskyCom = _commentMapper.Map<RiskyCommentViewModel>(riskyComments[i]);
            tempRiskyCom.Index = i + 1;
            riskyCommentsVm.Add(tempRiskyCom);
        }

        return riskyCommentsVm;
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