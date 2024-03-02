using AutoMapper;
using DreamNDine.Api.Models;
using DreamNDine.API.Models;
using DreamNDine.BLL.Models;
using DreamNDine.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace DreamNDine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public ActionResult<User> RegisterUser(UserRequest userDetails)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _mapper.Map<User>(userDetails);

            var createdUser = _userService.RegisterUser(user);
            return CreatedAtAction("GetUserById", new { id = createdUser.UserID }, _mapper.Map<UserViewModel>(createdUser));
        }

        [HttpPost("authenticate")]
        public ActionResult<User> AuthenticateUser(string username, string password)
        {
            var user = _userService.AuthenticateUser(username, password);
            if (user == null) return Unauthorized();
            return Ok(_mapper.Map<UserViewModel>(user));
        }

        [HttpPut("{userId}")]
        public ActionResult UpdateUserProfile(int userId, UserUpdateRequest userUpdateRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userToUpdate = _userService.GetUserProfile(userId);
            if (userToUpdate == null) return NotFound();

            _mapper.Map(userUpdateRequest, userToUpdate);

            _userService.UpdateUserProfile(userId, userToUpdate);
            return Ok();
        }

        [HttpPut("change-password")]
        public ActionResult ChangePassword(int userId, ChangePasswordRequest changePasswordRequest)
        {

            var user = _userService.GetUserProfile(userId);
            if (user == null) return NotFound();

            // Verify current password
            if (!BCrypt.Net.BCrypt.Verify(changePasswordRequest.CurrentPassword, user.Password))
            {
                return BadRequest("Incorrect current password");
            }

            // Hash the new password
            user.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordRequest.NewPassword);

            _userService.UpdateUserProfile(userId, user);
            return NoContent();
        }



        [HttpGet("{userId}")]
        public ActionResult<User> GetUserById(int userId)
        {
            var user = _userService.GetUserProfile(userId);
            if (user == null) return NotFound();
            return Ok(user);
        }   
    }
}