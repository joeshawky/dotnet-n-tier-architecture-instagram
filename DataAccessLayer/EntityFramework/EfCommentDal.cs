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
    public class EfCommentDal : GenericRepository<Comment>, ICommentDal
    {
        private readonly Context _context;
        private readonly DbSet<Comment> _object;
        public EfCommentDal()
        {
            _context = new Context();
            _object = _context.Set<Comment>();
        }

        public override List<Comment> List()
        {
            return _object
                .Include(c => c.User)
                .ThenInclude(u => u.ProfileImage)
                .Include(c => c.Post).ToList();
        }

        public override List<Comment> List(Expression<Func<Comment, bool>> filter)
        {
            return _object
                .Include(c => c.User)
                .ThenInclude(u => u.ProfileImage)
                .Include(c => c.Post)
                .Where(filter)
                .ToList();
        }

        public override Comment? Get(Expression<Func<Comment, bool>> filter)
        {
            return _object
                .Include(c => c.User)
                .ThenInclude(u => u.ProfileImage)
                .Include(c => c.Post)
                .SingleOrDefault(filter);
        }

    }
}
