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
    public class EfPostSaveDal : GenericRepository<PostSave>, IPostSaveDal
    {
        private readonly Context _context;
        private readonly DbSet<PostSave> _object;
        public EfPostSaveDal()
        {
            _context = new Context();
            _object = _context.Set<PostSave>();
        }
        public override List<PostSave> List()
        {
            return _object
                .Include(c => c.User)
                .Include(c => c.Post)
                .ThenInclude(c => c.Comments)
                .ToList();
        }

       

        public override List<PostSave> List(Expression<Func<PostSave, bool>> filter)
        {
            return _object
                .Include(c => c.User)
                .Include(c => c.Post)
                .ThenInclude(c => c.Comments)
                .Where(filter)
                .ToList();
        }

        public override PostSave? Get(Expression<Func<PostSave, bool>> filter)
        {
            return _object
                .Include(c => c.User)
                .Include(c => c.Post)
                .ThenInclude(c => c.Comments)
                .SingleOrDefault(filter);
        }

    }
}
