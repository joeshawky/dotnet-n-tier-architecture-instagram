using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete;

public class Image : InsertionDate
{

    [StringLength(25)]
    public string ImageTitle { get; set; } = "Post";

    [StringLength(255)]
    public string ImagePath { get; set; }

    [StringLength(25)]
    public string ImageExtension { get; set; }

        


}