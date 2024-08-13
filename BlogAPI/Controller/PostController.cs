using BlogAPI.Data;
using BlogAPI.Repository;
using BlogAPI.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPostRepository _ipostRepository;
        public PostController(AppDbContext appDbContext, IPostRepository ipostRepository)
        {
            _appDbContext = appDbContext;
            _ipostRepository = ipostRepository;
        }
        [HttpGet("Get-all-post")]
        [Authorize(Roles = "User")]
        public IActionResult getfullpost([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var postList = _ipostRepository.getfullpost(pageNumber, pageSize);
            if (postList != null)
            {
                return Ok(postList);
            }
            else
                return Ok("Data Empty");
        }
    }
}
