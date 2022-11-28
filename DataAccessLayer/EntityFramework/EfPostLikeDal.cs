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
    public class EfPostLikeDal : GenericRepository<PostLike>, IPostLikeDal
    {
        private readonly Context _context;
        private readonly DbSet<PostLike> _object;
        public EfPostLikeDal()
        {
            _context = new Context();
            _object = _context.Set<PostLike>();
        }
        public override List<PostLike> List()
        {
            return _object
                .Include(c => c.User)
                .Include(c => c.Post)
                .ThenInclude(c => c.Comments)
                .ToList();
        }

        public override List<PostLike> List(Expression<Func<PostLike, bool>> filter)
        {
            return _object
                .Include(c => c.User)
                .Include(c => c.Post)
                .ThenInclude(c => c.Comments)
                .Where(filter)
                .ToList();
        }

        public override PostLike? Get(Expression<Func<PostLike, bool>> filter)
        {
            return _object
                .Include(c => c.User)
                .Include(c => c.Post)
                .ThenInclude(c => c.Comments)
                .SingleOrDefault(filter);
        }



    }
}
