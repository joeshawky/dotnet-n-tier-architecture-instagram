using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfPostDal : GenericRepository<Post>, IPostDal
    {
        private Context _context;
        private DbSet<Post> _object;
        public EfPostDal()
        {
            _context = new Context();
            _object = _context.Set<Post>();
        }
        public override List<Post> List()
        {
            return _object
                .Include(o => o.Comments)
                .Include(o => o.User)
                .ThenInclude(o => o.ProfileImage)
                .ToList();
        }


        public override List<Post> List(Expression<Func<Post, bool>> filter)
        {
            return _object
                .Include(o => o.Comments)
                .Include(o => o.User)
                .ThenInclude(o => o.ProfileImage)
                .Where(filter)
                .ToList();
        }

        public override Post? Get(Expression<Func<Post, bool>> filter)
        {
            return _object
                .Include(o => o.Comments)
                .Include(o => o.User)
                .ThenInclude(o => o.ProfileImage)
                .SingleOrDefault(filter);
        }

    }
}
