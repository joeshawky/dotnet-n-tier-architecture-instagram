using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete;

public class PostSave : InsertionDate
{
    [Key] 
    public int PostSaveId { get; set; }

    public int? UserId { get; set; }
    public virtual User User { get; set; }

    public int PostId { get; set; }
    public virtual Post Post { get; set; }
}