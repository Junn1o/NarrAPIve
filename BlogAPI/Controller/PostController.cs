using BlogAPI.Data;
using BlogAPI.Model.DTO;
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
        [HttpGet("post")]
        [Authorize(Roles = "User")]
        public IActionResult GetFullPost([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var postList = _ipostRepository.GetFullPost(pageNumber, pageSize);
            if (postList != null)
            {
                return Ok(postList);
            }
            else
                return Ok("Data Empty");
        }
        [HttpGet("post/id/{postId}")]
        [Authorize(Roles = "User")]
        public IActionResult PostWithId([FromForm] Guid postId)
        {
            var postListWithId = _ipostRepository.PostWithId(postId);
            if (postListWithId != null)
            {
                return Ok(postListWithId);
            }
            else
                return Ok("Data Empty");
        }
        [HttpGet("post/user/{userId}")]
        [Authorize(Roles = "User")]
        public IActionResult PostWithUserId([FromForm] Guid userId)
        {
            var postListWithUserId = _ipostRepository.PostWithUserId(userId);
            if (postListWithUserId != null)
            {
                return Ok(postListWithUserId);
            }
            else
                return Ok("Data Empty");
        }
        [HttpGet("post/volume/{volumeId}")]
        [Authorize(Roles = "User")]
        public IActionResult VolumeDetailWithId([FromForm] Guid volumeId)
        {
            var volumeDetail = _ipostRepository.PostWithUserId(volumeId);
            if (volumeDetail != null)
            {
                return Ok(volumeDetail);
            }
            else
                return Ok("Data Empty");
        }
        [HttpPost("post")]
        [Authorize(Roles = "User")]
        public IActionResult AddPost([FromForm] PostRequestFormDTO addpostDTO)
        {
            var newPost = _ipostRepository.AddPost(addpostDTO);
            if (newPost != null)
            {
                return Ok(newPost);
            }
            else
                return Ok("Data Empty");
        }
        [HttpPut("post")]
        [Authorize(Roles = "User")]
        public IActionResult UpdatePost([FromForm] Guid postId, [FromForm] PostRequestFormDTO addpostDTO)
        {
            var updatePost = _ipostRepository.UpdatePost(postId, addpostDTO);
            if (updatePost != null)
            {
                return Ok(updatePost);
            }
            else
                return Ok("Data Empty");
        }
        [HttpDelete("post")]
        [Authorize(Roles = "User")]
        public IActionResult DeletePost([FromForm] Guid postId)
        {
            var deletePost = _ipostRepository.DeletePost(postId);
            if (deletePost != null)
            {
                return Ok(deletePost);
            }
            else
                return Ok("Data Empty");
        }

        [HttpPost("post/volume")]
        [Authorize(Roles = "User")]
        public IActionResult AddVolume([FromForm] VolumeRequestFormDTO addvolumeDTO)
        {
            var newVolume = _ipostRepository.AddVolume(addvolumeDTO);
            if (newVolume != null)
            {
                return Ok(newVolume);
            }
            else
                return Ok("Data Empty");
        }
        [HttpPut("post/volume")]
        [Authorize(Roles = "User")]
        public IActionResult UpdateVolume([FromForm] Guid volumeId, [FromForm] VolumeRequestFormDTO updatevolumeDTO)
        {
            var updateVolume = _ipostRepository.UpdateVolume(volumeId, updatevolumeDTO);
            if (updateVolume != null)
            {
                return Ok(updateVolume);
            }
            else
                return Ok("Data Empty");
        }
        [HttpDelete("post/volume")]
        [Authorize(Roles = "User")]
        public IActionResult DeleteVolume([FromForm] Guid volumeId)
        {
            var deleteVolume = _ipostRepository.DeleteVolume(volumeId);
            if (deleteVolume != null)
            {
                return Ok(deleteVolume);
            }
            else
                return Ok("Data Empty");
        }

        [HttpPost("post/volume/chapter")]
        [Authorize(Roles = "User")]
        public IActionResult AddChapter([FromForm] ChapterRequestFormDTO addchapterDTO)
        {
            var newChapter = _ipostRepository.AddChapter(addchapterDTO);
            if (newChapter != null)
            {
                return Ok(newChapter);
            }
            else
                return Ok("Data Empty");
        }
        [HttpPut("post/volume/chapter")]
        [Authorize(Roles = "User")]
        public IActionResult UpdateChapter([FromForm] Guid chapterId, [FromForm] ChapterRequestFormDTO updatechapterDTO)
        {
            var updateChapter = _ipostRepository.UpdateChapter(chapterId, updatechapterDTO);
            if (updateChapter != null)
            {
                return Ok(updateChapter);
            }
            else
                return Ok("Data Empty");
        }
        [HttpDelete("post/volume/chapter")]
        [Authorize(Roles = "User")]
        public IActionResult DeleteChapter([FromForm] Guid chapterId)
        {
            var deleteChapter = _ipostRepository.DeleteChapter(chapterId);
            if (deleteChapter != null)
            {
                return Ok(deleteChapter);
            }
            else
                return Ok("Data Empty");
        }

        [HttpPost("post/volume/chapter/image")]
        [Authorize(Roles = "User")]
        public IActionResult AddChapterImage([FromForm] ChapterImageRequestFormDTO addchapterimageDTO)
        {
            var newChapterImage = _ipostRepository.AddChapterImage(addchapterimageDTO);
            if (newChapterImage != null)
            {
                return Ok(newChapterImage);
            }
            else
                return Ok("Data Empty");
        }
        [HttpPut("post/volume/chapter/image")]
        [Authorize(Roles = "User")]
        public IActionResult UpdateChapterImage([FromForm] ChapterImageRequestFormDTO updatechapterimageDTO)
        {
            var updateChapterImage = _ipostRepository.UpdateChapterImage(updatechapterimageDTO);
            if (updateChapterImage != null)
            {
                return Ok(updateChapterImage);
            }
            else
                return Ok("Data Empty");
        }
        [HttpDelete("post/volume/chapter")]
        [Authorize(Roles = "User")]
        public IActionResult DeleteChapterImage([FromForm] string chapterImagePath)
        {
            var deleteChapterImage = _ipostRepository.DeleteChapterImage(chapterImagePath);
            if (deleteChapterImage != null)
            {
                return Ok(deleteChapterImage);
            }
            else
                return Ok("Data Empty");
        }
    }
}
