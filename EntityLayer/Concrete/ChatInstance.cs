using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete;

public class ChatInstance : InsertionDate
{
    [Key]
    public int ChatInstanceId { get; set; }

    [Required]
    [MaxLength(100)]
    public int Channel { get; set; }

    [Required]
    [MaxLength(25)]
    public string Sender { get; set; } = string.Empty;

    [Required]
    [MaxLength(25)]

    public string Receiver { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Message { get; set; } = string.Empty;
}
