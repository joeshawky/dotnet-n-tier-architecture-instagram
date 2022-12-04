using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.EntityFramework;

public class EfFollowInstance : GenericRepository<FollowInstance>, IFollowInstanceDal
{
	private readonly Context _context;
	private readonly DbSet<FollowInstance> _object;

	public EfFollowInstance()
	{
		_context = new Context();
		_object = _context.Set<FollowInstance>();
	}

	public override FollowInstance? Get(Expression<Func<FollowInstance, bool>> filter)
	{
		return _object
			.Include(f => f.FollowerUser)
			.SingleOrDefault(filter);
	}

	public override List<FollowInstance> List()
	{
		return _object
			.Include(f => f.FollowerUser)
			.ToList();
	}

	public override List<FollowInstance> List(Expression<Func<FollowInstance, bool>> filter)
	{
		return _object
			.Include(f => f.FollowerUser)
			.Where(filter)
			.ToList();
	}

}
