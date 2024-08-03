using BlogAPI.Data;
using BlogAPI.Model.DTO;
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
        public UserController(AppDbContext appDbContext, IUserRepository userRepository)
        {
            _appDbContext = appDbContext;
            _userRepository = userRepository;
        }
        [HttpPost("add-user")]
        public IActionResult adduser([FromForm] userrequestformDTO adduserDTO)
        {
            var userAdd = _userRepository.adduserDTO(adduserDTO);
            return Ok(userAdd);
        }
    }
}
