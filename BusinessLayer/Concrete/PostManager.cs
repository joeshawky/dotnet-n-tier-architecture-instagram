using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete;

public class PostManager : IPostService
{
    private readonly IPostDal _postDal;
    private readonly IPostLikeService _postLikeManager;
    private readonly IPostSaveService _postSaveManager;

    public PostManager(IPostDal postDal, IPostLikeDal postLikeDal, IPostSaveDal postSaveDal)
    {
        _postDal = postDal;
        _postSaveManager = new PostSaveManager(postSaveDal);
        _postLikeManager = new PostLikeManager(postLikeDal);
    }

    public List<Post> GetList()
    {
        return _postDal.List().OrderByDescending(p => p.DateTime).ToList();
    }
    public List<Post> GetList(User user)
    {
        return _postDal.List(p => p.UserId == user.UserId).OrderByDescending(p => p.DateTime).ToList();
    }
    public List<PostLike> GetLikedPostsList(User user)
    {
        return _postLikeManager.GetListByUser(user);
    }
    public List<PostSave> GetSavedPostsList(User user)
    {
        return _postSaveManager.GetListByUser(user);
    }
    public void Add(Post post)
    {
        _postDal.Insert(post);
    }

    public void Update(Post post)
    {
        _postDal.Update(post);
    }

    public void Delete(Post post)
    {
        _postDal.Delete(post);
    }

    public Post? GetById(int postId)
    {
        return _postDal.Get(p => p.PostId == postId);
    }

    public string PostLike(int postId, int userId)
    {
        var postLikedByUser = _postLikeManager.GetById(postId, userId);
        var post = GetById(postId);

        if (postLikedByUser is null)
        {
            _postLikeManager.Add(new PostLike()
            {
                PostId = postId,
                UserId = userId
            });
            post.Likes++;
            Update(post);
            return "Post Liked";
        }
        _postLikeManager.Delete(postLikedByUser);

        post.Likes--;
        Update(post);
        return "Post Disliked";
    }

    public bool IsPostLikedByCurrentUser(int postId, int userId)
    {
        if (_postLikeManager.GetById(postId, userId) == null)
            return false;

        return true;
    }

    public string PostSave(int postId, int userId)
    {
        var postSavedByUser = _postSaveManager.GetById(postId, userId);

        if (postSavedByUser is null)
        {
            _postSaveManager.Add(new PostSave()
            {
                PostId = postId,
                UserId = userId
            });
            return "Post Saved";
        }


        _postSaveManager.Delete(postSavedByUser);

        return "Post UnSaved";
    }

    public bool IsPostSavedByCurrentUser(int postId, int userId)
    {
        if (_postSaveManager.GetById(postId, userId) == null)
            return false;

        return true;
    }

}