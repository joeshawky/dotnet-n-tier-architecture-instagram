using EntityLayer.Concrete;

namespace BusinessLayer.Abstract;

public interface IFollowInstanceService : IService<FollowInstance>
{
    List<int> GetFollowingIdsForUser(int userId);
    List<int> GetFollowersIdsForUser(int userId);
}
