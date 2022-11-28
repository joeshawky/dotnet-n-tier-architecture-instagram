using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete;

public class User : InsertionDate
{
    [Key] 
    public int UserId { get; set; }


    [StringLength(20)]
    public string Username { get; set; } = string.Empty;


    [StringLength(20)]
    public string Password { get; set; } = string.Empty;


    [StringLength(15)] 
    public string UserRole { get; set; } = "user";


    [StringLength(255)]
    public string Description { get; set; } = string.Empty;
    public int? ProfileImageId { get; set; }
    public virtual ProfileImage ProfileImage { get; set; }

    public ICollection<Post> Posts { get; set; }
}