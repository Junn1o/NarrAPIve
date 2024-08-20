using BlogAPI.Data;
using BlogAPI.Model.DTO;
using BlogAPI.Repository;
using BlogAPI.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserRepository _userRepository;
        public UserController(AppDbContext appDbContext, IUserRepository userRepository)
        {
            _appDbContext = appDbContext;
            _userRepository = userRepository;
        }
        [HttpPost("register")]
        public IActionResult RegisterUser([FromForm] UserRequestFormDTO adduserDTO)
        {
            var userAdd = _userRepository.RegisterUser(adduserDTO);
            return Ok(userAdd);
        }
        [HttpPost("login")]
        public IActionResult Login([FromForm] LoginDTO credentialDTO)
        {
            var credential = _userRepository.Login(credentialDTO.userName);
            if (credential == null || !_userRepository.ValidatePassword(credentialDTO.userName, credentialDTO.password))
                return Unauthorized();
            var loginResponse = _userRepository.ResponseData(credentialDTO.userName);
            var token = _userRepository.GenerateJwtToken(loginResponse);
            return Ok(new { token });
        }
        [Authorize]
        [HttpGet("user/{userId}")]
        public IActionResult UserWithId([FromForm] Guid userId)
        {
            var currentUserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _userRepository.UserWithId(userId, currentUserId);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
    }
}
