using AutoMapper;
using BusinessLayer.Concrete;
using BusinessLayer.Utilities;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using ModelViews.Concrete;

namespace BusinessLayer.AutoMapperProfiles;

public class ImageMapper : Profile
{
    private readonly ImageManager _imageManager = new ImageManager(new EfImageDal());

    public ImageMapper()
    {
        CreateMap<IFormFile, ProfileImage>()
            .ForMember(p => p.ImageExtension, operation => operation.MapFrom(source => source.ContentType))
            .ForMember(p => p.ImagePath, operation => operation.MapFrom(source => _imageManager.SaveFileToFolder(source)));
    }
}