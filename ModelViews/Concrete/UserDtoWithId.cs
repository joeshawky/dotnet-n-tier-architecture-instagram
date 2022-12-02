using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViews.Concrete
{
    public class UserDtoWithId
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string ProfilePicturePath { get; set; } = string.Empty;
    }
}
