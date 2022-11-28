using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using ModelViews.Concrete;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UiLayerMvc.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserManager _userManager;
    private readonly ImageManager _imageManager;

    public UsersController()
    {
        _imageManager = new ImageManager(new EfImageDal());
        _userManager = new UserManager(new EfUserDal());
    }


    [HttpGet(nameof(GetCurrentUserInfo))]
    public IActionResult GetCurrentUserInfo()
    {
        if (User.Identity.IsAuthenticated is false)
            return BadRequest("User not logged in.");

		var user = _userManager.GetByName(User.Identity.Name);

		if (user is null)
            return BadRequest("User not found");


        var userDto = new UserDto()
        {
            UserId = user.UserId,
            Username = user.Username,
            ProfilePicturePath = user.ProfileImage.ImagePath
        };

		return Ok(userDto);
    }


	[HttpGet("GetCurrentUserId")]
    public int? GetCurrentUserId()
    {
        if (User.Identity.IsAuthenticated)
        {
            return _userManager.GetByName(User.Identity.Name).UserId;
        }

        return 0;
    }

            




    // GET: api/<UsersController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<UsersController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}