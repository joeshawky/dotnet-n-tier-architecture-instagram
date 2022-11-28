using System.ComponentModel.DataAnnotations;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;

namespace ModelViews.Concrete
{
    public class PostDto
    {

        public string PostDescription { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }


    }
}