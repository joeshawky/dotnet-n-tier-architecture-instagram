using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete;

public class PostLike : InsertionDate
{
    [Key] 
    public int PostLikeId { get; set; }

    public int? UserId { get; set; }
    public virtual User User { get; set; }

    public int PostId { get; set; }
    public virtual Post Post { get; set; }
}