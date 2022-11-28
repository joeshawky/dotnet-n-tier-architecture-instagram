using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework
{
    public class EfRiskyCommentDal : GenericRepository<RiskyComment>, IRiskyCommentDal
    {
        private readonly Context _context;
        private readonly DbSet<RiskyComment> _object;
        public EfRiskyCommentDal()
        {
            _context = new Context();
            _object = _context.Set<RiskyComment>();
        }
        public override RiskyComment? Get(Expression<Func<RiskyComment, bool>> filter)
        {
            return _object
                .Include(c => c.Comment)
                .ThenInclude(c => c.User)
                .SingleOrDefault(filter);
        }

        public override List<RiskyComment> List()
        {
            return _object
                .Include(c => c.Comment)
                .ThenInclude(c => c.User)
                .ToList();
        }

        public override List<RiskyComment> List(Expression<Func<RiskyComment, bool>> filter)
        {
            return _object
                .Include(c => c.Comment)
                .ThenInclude(c => c.User)
                .Where(filter)
                .ToList();
        }
    }
}
