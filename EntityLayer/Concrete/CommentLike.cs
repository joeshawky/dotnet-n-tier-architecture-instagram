using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete;

public class CommentLike
{
    [Key]
    public int PostLikeId { get; set; }


    public int? UserId { get; set; }
    public virtual User User { get; set; }

    public int CommentId { get; set; }
    public virtual Comment Comment { get; set; }
}