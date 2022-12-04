using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete;

public class FollowInstance : InsertionDate
{
    [Key]
    public int Id { get; set; }


    [ForeignKey(nameof(UserId))]
    public int UserId { get; set; }

    public virtual User FollowerUser { get; set; }

    public int FollowedUserId { get; set; }

    

}
