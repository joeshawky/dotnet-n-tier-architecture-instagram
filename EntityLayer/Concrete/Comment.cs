using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete;

public class Comment : InsertionDate
{
    [Key]
    public int CommentId { get; set; }

    [StringLength(255)]
    public string CommentText { get; set; }
    public int Likes { get; set; } = 0;

    public int PostId { get; set; }
    public virtual Post Post { get; set; }

    public int? UserId { get; set; }
    public virtual User User { get; set; }
    public ICollection<CommentLike> CommentLikes { get; set; }


}