using BlogAPI.Data;
using BlogAPI.Model.DTO;
using BlogAPI.Repository;
using BlogAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserRepository _userRepository;
        private readonly ICredentialRepository _credentialRepository;
        public UserController(AppDbContext appDbContext, IUserRepository userRepository, ICredentialRepository credentialRepository)
        {
            _appDbContext = appDbContext;
            _userRepository = userRepository;
            _credentialRepository = credentialRepository;
        }
        [HttpPost("register-user")]
        public IActionResult registeruser([FromForm] UserRequestFormDTO adduserDTO)
        {
            var userAdd = _userRepository.registeruser(adduserDTO);
            return Ok(userAdd);
        }
        [HttpPost("login")]
        public IActionResult Login([FromForm] CredentialDTO credentialDTO)
        {

            var credential = _credentialRepository.Login(credentialDTO.userName, credentialDTO.password);
            if (credential == null)
                return Unauthorized();
            var loginResponse = _credentialRepository.LoginData(credentialDTO.userName);
            var token = _credentialRepository.GenerateJwtToken(loginResponse);
            return Ok(new { token });
        }
    }
}
