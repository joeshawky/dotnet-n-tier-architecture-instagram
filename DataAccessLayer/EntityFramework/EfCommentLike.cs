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
    public class EfCommentLike : GenericRepository<CommentLike>, ICommentLikeDal
    {
        private readonly Context _context;
        private readonly DbSet<CommentLike> _object;
        public EfCommentLike()
        {
            _context = new Context();
            _object = _context.Set<CommentLike>();
        }


        public override List<CommentLike> List()
        {
            return _object
                .Include(c => c.User)
                .Include(c => c.Comment).ToList();
        }

        public override List<CommentLike> List(Expression<Func<CommentLike, bool>> filter)
        {
            return _object
                .Include(c => c.User)
                .Include(c => c.Comment)
                .Where(filter)
                .ToList();
        }

        public override CommentLike? Get(Expression<Func<CommentLike, bool>> filter)
        {
            return _object
                .Include(c => c.User)
                .Include(c => c.Comment)
                .SingleOrDefault(filter);
        }
    }
}
