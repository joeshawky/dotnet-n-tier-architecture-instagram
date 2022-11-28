using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete;

public class ProfileImageManager : IProfileImageService
{
    private readonly IProfileImageDal _profileImageDal;

    public ProfileImageManager(IProfileImageDal profileImageDal)
    {
        _profileImageDal = profileImageDal;
    }


    public List<ProfileImage> GetList()
    {
        return _profileImageDal.List();
    }

    public void Add(ProfileImage profileImage)
    {
        _profileImageDal.Insert(profileImage);
    }

    public void Delete(ProfileImage profileImage)
    {
        _profileImageDal.Delete(profileImage);
    }

    public void Update(ProfileImage profileImage)
    {
        _profileImageDal.Update(profileImage);
    }

    public ProfileImage? GetById(int id)
    {
        return _profileImageDal.Get(p => p.ProfileImageId == id);
    }

    public ProfileImage? GetFirstImage()
    {
        return _profileImageDal.List(p => p.ProfileImageId != 0).FirstOrDefault();
    }
}