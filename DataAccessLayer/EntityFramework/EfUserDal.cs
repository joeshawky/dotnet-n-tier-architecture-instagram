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
using System.Xml.Linq;

namespace DataAccessLayer.EntityFramework
{
    public class EfUserDal : GenericRepository<User>, IUserDal
    {
        private readonly Context _context;
        private readonly DbSet<User> _object;
        public EfUserDal()
        {
            _context = new Context();
            _object = _context.Set<User>();
        }
        public override List<User> List()
        {
            return _object
                .Include(u => u.ProfileImage)
                .Include(u => u.FollowInstances)
                .ToList();
        }
        public override List<User> List(Expression<Func<User, bool>> filter)
        {
            return _object
                .Include(u => u.ProfileImage)
                .Include(u => u.FollowInstances)
                .Where(filter)
                .ToList();
        }

        public override User? Get(Expression<Func<User, bool>> filter)
        {
            return _object
                .Include(u => u.ProfileImage)
                .Include(u => u.FollowInstances)
                .SingleOrDefault(filter);
        }
    }
}
