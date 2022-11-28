using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IPostSaveService : IService<PostSave>
    {
        PostSave? GetById(int postId, int userId);
        List<PostSave> GetListByUser(User user);

    }
}
