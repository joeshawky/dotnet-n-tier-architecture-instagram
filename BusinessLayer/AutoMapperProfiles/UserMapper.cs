using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using ModelViews.Concrete;

namespace BusinessLayer.AutoMapperProfiles;

public class UserMapper : Profile
{
    private readonly PostManager _postManager = new PostManager(new EfPostDal(), new EfPostLikeDal(), new EfPostSaveDal(), new EfFollowInstance(), new EfUserDal());
    private readonly UserManager _userManager = new UserManager(new EfUserDal(), new EfFollowInstance());
    private readonly FollowInstanceManager _followInstanceManager = new FollowInstanceManager(new EfFollowInstance(), new EfUserDal());
    public UserMapper()
    {

        CreateMap<User, EditProfileModelView>()
            .ForMember(destination => destination.ImagePath, operation => operation.MapFrom(source => source.ProfileImage.ImagePath));

        CreateMap<User, SuggestedUserModalView>()
            .ForMember(destination => destination.ProfilePicturePath,
                operation => operation.MapFrom(source => source.ProfileImage.ImagePath));



        CreateMap<User, ProfileViewModel>()
            .ForMember(destination => destination.UserProfilePicturePath, operation => operation.MapFrom(source => source.ProfileImage.ImagePath))
            .ForMember(destination => destination.PostCount, operation => operation.MapFrom(source => _postManager.GetList(source).Count))
            .ForMember(destination => destination.FollowingCount, operation => operation.MapFrom(source => _followInstanceManager.GetFollowingUsernamesForUser(source.Username).Count))
            .ForMember(destination => destination.FollowersCount, operation => operation.MapFrom(source => _followInstanceManager.GetFollowersUsernamesForUser(source.Username).Count));

        CreateMap<User, UserDto>()
            .ForMember(destination => destination.ProfilePicturePath, operation => operation.MapFrom(source => source.ProfileImage.ImagePath))
            .ForMember(destination => destination.CreationDate, operation => operation.MapFrom(source => source.DateTime))
            .ForMember(destination => destination.Role, operation => operation.MapFrom(source => source.UserRole));



    }
}