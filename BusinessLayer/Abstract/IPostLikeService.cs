using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IPostLikeService : IService<PostLike>
    {
        PostLike? GetById(int postId, int userId);
        List<PostLike> GetListByUser(User user);

    }
}
