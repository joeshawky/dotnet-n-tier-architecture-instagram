using BusinessLayer.Abstract;
using FluentValidation;
using ModelViews.Concrete;

namespace BusinessLayer.FluentValidation;

public class PostValidator : AbstractValidator<PostDto>, IPostValidator
{
    public List<string> ImageFormats = new List<string>()
    {
        "image/png",
        "image/jpeg",
        "image/jpg"
    };

    public PostValidator()
    {
        RuleFor(x => x.ImageFile)
            .NotEmpty().WithMessage("You must choose an image");

        RuleFor(x => x.ImageFile)
            .Must(x => x is not null && ContainsRightFormat(x.ContentType))
            .WithMessage("Image file must be jpg, jpeg or png");

        RuleFor(x => x.PostDescription)
            .NotEmpty()
            .WithMessage("Description can't be empty");

    }

    public bool ContainsRightFormat(string imageExtension)
    {
        return ImageFormats.Contains(imageExtension);
    }


}