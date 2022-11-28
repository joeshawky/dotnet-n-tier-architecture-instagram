using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ModelViews.Concrete;

public class EditProfileModelView
{
    public int UserId { get; set; }
    [Display(Name = "Username")]
    public string Username { get; set; } = string.Empty;

    [Display(Name = "Description")]
    public string Description { get; set; } = string.Empty;

    [Display(Name ="Profile picture")]
    public IFormFile ProfilePicture { get; set; }
    public string ImagePath { get; set; } = string.Empty;

}
