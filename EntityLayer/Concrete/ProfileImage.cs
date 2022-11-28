using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete;

public class ProfileImage : InsertionDate
{
    [Key] 
    public int ProfileImageId { get; set; }


    [StringLength(25)] 
    public string ImageTitle { get; set; } = "Profile Picture";


    [StringLength(255)] 
    public string ImagePath { get; set; }


    [StringLength(25)] 
    public string ImageExtension { get; set; }


    [NotMapped] 
    public static int DefaultProfilePicture { get; set; } = 10;
}