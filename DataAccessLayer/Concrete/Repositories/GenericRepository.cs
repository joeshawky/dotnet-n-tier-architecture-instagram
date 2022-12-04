using DataAccessLayer.Abstract;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concrete.Repositories
{
    public class GenericRepository<T> : IRepositoryDal<T> where T : class
    {
        private readonly Context _context;
        private readonly DbSet<T> _object;

        public GenericRepository()
        {
            _context = new Context();
            _object = _context.Set<T>();
        }

        public virtual List<T> List()
        {
            return _object.ToList();
        }

        public void Insert(T p)
        {

            var updatedEntity = _context.Entry(p);
            updatedEntity.State = EntityState.Added;
            _context.SaveChanges();

        }

        public void Update(T p)
        {
            var updatedEntity = _context.Entry(p);
            updatedEntity.State = EntityState.Modified;
            _context.SaveChanges();

        }

        public void Delete(T p)
        {
            var updatedEntity = _context.Entry(p);
            updatedEntity.State = EntityState.Deleted;
            _context.SaveChanges();


        }

        public virtual List<T> List(Expression<Func<T, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public virtual T? Get(Expression<Func<T, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }
    }
}
