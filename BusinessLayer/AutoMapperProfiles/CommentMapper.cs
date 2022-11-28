using AutoMapper;
using BusinessLayer.Utilities;
using EntityLayer.Concrete;
using ModelViews.Concrete;

namespace BusinessLayer.AutoMapperProfiles;

public class CommentMapper : Profile
{
    private readonly TimeFormatter _timeFormatter = new TimeFormatter();
    public CommentMapper()
    {
        CreateMap<Comment, RiskyComment>();

        CreateMap<RiskyComment, RiskyCommentViewModel>()
            .ForMember(destination => destination.Username,
                operation => operation.MapFrom(source => source.Comment.User.Username))
            .ForMember(destination => destination.CommentText,
                operation => operation.MapFrom(source => source.Comment.CommentText))
            .ForMember(destination => destination.CommentDatetime,
                operation => operation.MapFrom(source => source.Comment.DateTime))
            .ForMember(destination => destination.PostId,
                operation => operation.MapFrom(source => source.Comment.PostId))
            ;


        CreateMap<CommentDto, Comment>()
            .ForMember(destination => destination.UserId,
                operation => operation.MapFrom(source => source.CommenterUserId));



        CreateMap<Comment, CommentViewModel>()
            .ForMember(destination => destination.CommentCreationTime,
                operation => operation.MapFrom(source => _timeFormatter.FormatTime(source.DateTime)))
            .ForMember(destination => destination.CommentLikes,
                operation => operation.MapFrom(source => source.Likes))
            .ForMember(destination => destination.CommentMakerId,
                operation => operation.MapFrom(source => source.User.UserId))
            .ForMember(destination => destination.CommentMakerUsername,
                operation => operation.MapFrom(source => source.User.Username))
            .ForMember(destination => destination.CommentMakerProfilePicturePath,
                operation => operation.MapFrom(source => source.User.ProfileImage.ImagePath));

    }
}