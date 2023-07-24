using Microsoft.AspNetCore.Mvc;

namespace MyApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            var users = _userService.GetUsers();
            var userDtos = users.Select(u => new UserDto { Id = u.Id, Name = u.Name });
            return Ok(userDtos);
        }

        [HttpPost]
        public ActionResult CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var user = new User { Name = createUserDto.Name };
            _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUser(int id)
        {
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = new UserDto { Id = user.Id, Name = user.Name };
            return Ok(userDto);
        }
    }
}