using FluentValidation;
using Microsoft.AspNetCore.Http;
using ModelViews.Concrete;

namespace BusinessLayer.FluentValidation;

public class EditProfileValidator : AbstractValidator<EditProfileModelView>
{

    public EditProfileValidator()
    {
        RuleFor(x => x.Username)
            .Must(x => x.Length <= 20 && x.Length >= 3)
            .WithMessage("Username must be between 3 and 20 characters.");

        RuleFor(x => x.Description)
             .Must(x => x.Length <= 255)
             .WithMessage("Description is too long.");
       
       RuleFor(x => x.ProfilePicture)
            .Must(x => FileValidationRules(x))
            .WithMessage("Image file must be jpg, jpeg or png.");
    }
    public List<string> ImageFormats = new List<string>()
    {
        "image/png",
        "image/jpeg",
        "image/jpg"
    };

    public bool FileValidationRules(IFormFile file)
    {
        if (IsEmptyOrSame(file))
            return true;

        return ContainsRightFormat(file.ContentType);
    }
    public bool IsEmptyOrSame(IFormFile file)
    {
        if (file == null)
            return true;

        return false;
    }
    public bool ContainsRightFormat(string imageExtension)
    {
        return ImageFormats.Contains(imageExtension);
    }


}
