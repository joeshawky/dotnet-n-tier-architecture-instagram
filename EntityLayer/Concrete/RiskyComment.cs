using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class RiskyComment : InsertionDate
    {
        [Key]
        public int RiskyCommentId { get; set; }


        public int? CommentId { get; set; }
        public virtual Comment Comment { get; set; }

        

    }
}
