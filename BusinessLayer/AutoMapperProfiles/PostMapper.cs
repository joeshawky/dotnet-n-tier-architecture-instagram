using AutoMapper;
using BusinessLayer.Utilities;
using EntityLayer.Concrete;
using ModelViews.Concrete;

namespace BusinessLayer.AutoMapperProfiles;

public class PostMapper : Profile
{
    private readonly TimeFormatter _timeFormatter = new TimeFormatter();
    public PostMapper()
    {
        CreateMap<Post, PostDisplayViewModel>()
            .ForMember(destination => destination.PostLikesCount,
                operation => operation.MapFrom(source => source.Likes))
            .ForMember(destination => destination.PostCommentsCount,
                operation => operation.MapFrom(source => source.Comments.Count()))
            .ForMember(destination => destination.PostMakerUsername,
                operation => operation.MapFrom(source => source.User.Username))
            .ForMember(destination => destination.PostCreationTime,
                operation => operation.MapFrom(source => _timeFormatter.FormatTime(source.DateTime)))
            .ForMember(destination => destination.PostImagePath,
                operation => operation.MapFrom(source => source.ImagePath))
            .ForMember(destination => destination.PostMakerProfilePicturePath,
                operation => operation.MapFrom(source => source.User.ProfileImage.ImagePath));


        CreateMap<PostLike, PostDisplayViewModel>()
            .ForMember(destination => destination.PostImagePath,
                operation => operation.MapFrom(source => source.Post.ImagePath))
            .ForMember(destination => destination.PostLikesCount,
                operation => operation.MapFrom(source => source.Post.Likes))
            .ForMember(destination => destination.PostCommentsCount,
                operation => operation.MapFrom(source => source.Post.Comments.Count));


        CreateMap<PostSave, PostDisplayViewModel>()
            .ForMember(destination => destination.PostImagePath,
                operation => operation.MapFrom(source => source.Post.ImagePath))
            .ForMember(destination => destination.PostLikesCount,
                operation => operation.MapFrom(source => source.Post.Likes))
            .ForMember(destination => destination.PostCommentsCount,
                operation => operation.MapFrom(source => source.Post.Comments.Count));

        CreateMap<PostDto, Post>()
            .ForMember(destination => destination.ImageExtension,
                operation => operation.MapFrom(source => source.ImageFile.ContentType));




    }
}