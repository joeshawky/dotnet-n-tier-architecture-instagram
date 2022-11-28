using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete
{
    public class Post : Image
    {
        [Key] 
        public int PostId { get; set; }

        [StringLength(255)]
        [Display(Name = "Description")]
        public string PostDescription { get; set; } = string.Empty;

        public int Likes { get; set; } = 0;

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }


        [NotMapped] 
        public static string RedColor { get; set; } = "#f44336";

        [NotMapped] 
        public static string BlackColor { get; set; } = "#262626";
    }
}