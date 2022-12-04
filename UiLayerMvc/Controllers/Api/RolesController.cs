using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UiLayerMvc.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private UserManager _userManager;


        public RolesController(IUserDal userDal, IFollowInstanceDal followInstanceDal)
        {
            _userManager = new UserManager(userDal, followInstanceDal);
        }

        [HttpPut("ModifyRole")]
        public IActionResult ModifyRole(string username, string roleName)
        {
            var user = _userManager.GetByName(username);
            if (user is null)
                return NotFound();

            if (string.IsNullOrEmpty(roleName))
                return BadRequest();

            user.UserRole= roleName;
            _userManager.Update(user);
            return Ok();
        }
    }
}
