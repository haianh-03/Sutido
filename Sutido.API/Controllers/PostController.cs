using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sutido.Model.Entites;
using Sutido.Service.Interfaces;
using System.Data;
using ViewModels.Requests;
using ViewModels.Responses;

namespace Sutido.API.Controllers
{
    [Route("/api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _iService;
        private readonly IMapper _iMapper;

        public PostController(IPostService iService, IMapper iMapper)
        {
            _iService = iService;
            _iMapper = iMapper;
        }

        //// get all post
        //[HttpGet]
        //public async Task<IActionResult> GetAllAsync(
        //    [FromQuery] string? keyword,
        //    [FromQuery] string? sortBy,
        //    [FromQuery] string sortOrder = "asc",
        //    [FromQuery] string? subject = null,
        //    [FromQuery] string? grade = null,
        //    [FromQuery] string? location = null,
        //    [FromQuery] int page = 1,
        //    [FromQuery] int pageSize = 5)
        //{
        //    var filters = new Dictionary<string, string>();
        //    filters.Add("Subject", subject);
        //    filters.Add("StudentGrade", grade);
        //    filters.Add("Location", location);

        //    var result = await _iService.GetAllAsync(keyword, sortBy, sortOrder, page, pageSize, filters);

        //    if (result == null || result.Count() == 0) return NoContent();

        //    var response = _iMapper.Map<IEnumerable<PostResponse>>(result);

        //    return Ok(response);
        //}

        // get all of tutor
        [HttpGet("tutor-posts")]
        public async Task<IActionResult> GetAllTutorPostsAsync(
            [FromQuery] string? keyword,
            [FromQuery] string? sortBy,
            [FromQuery] string sortOrder = "asc",
            [FromQuery] string? subject = null,
            [FromQuery] string? grade = null,
            [FromQuery] string? location = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5)
        {
            var filters = new Dictionary<string, string>();
            filters.Add("Subject", subject);
            filters.Add("StudentGrade", grade);
            filters.Add("Location", location);

            var result = await _iService.GetAllAsync(keyword, sortBy, sortOrder, page, pageSize, filters, Model.Enums.PostType.FindStudent);

            if (result == null || result.Count() == 0) return NoContent();

            var response = _iMapper.Map<IEnumerable<PostResponse>>(result);

            return Ok(response);
        }

        // get all of tutor
        [HttpGet("cus-posts")]
        public async Task<IActionResult> GetAllCustomerPostsAsync(
            [FromQuery] string? keyword,
            [FromQuery] string? sortBy,
            [FromQuery] string sortOrder = "asc",
            [FromQuery] string? subject = "",
            [FromQuery] string? grade = "",
            [FromQuery] string? location = "",
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5)
        {
            var filters = new Dictionary<string, string>();
            filters.Add("Subject", subject);
            filters.Add("StudentGrade", grade);
            filters.Add("Location", location);

            var result = await _iService.GetAllAsync(keyword, sortBy, sortOrder, page, pageSize, filters, Model.Enums.PostType.FindTutor);

            if (result == null || result.Count() == 0) return NoContent();

            var response = _iMapper.Map<IEnumerable<PostResponse>>(result);

            return Ok(response);
        }

        // get post by id
        [HttpGet("{id}", Name = "GetPostById")]
        public async Task<IActionResult> GetDetailsByIdAsync(int id)
        {
            var result = await _iService.GetDetailsByIdAsync(id);

            if (result == null) return NotFound();

            var response = _iMapper.Map<PostDetailsResponse>(result);

            return Ok(response);
        }

        // add post
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] PostRequest p)
        {

            var request = _iMapper.Map<Post>(p);

            var result = await _iService.AddAsync(request);

            if (result == null)
                return StatusCode(500, "Failed to create post.");
            var response = _iMapper.Map<PostResponse>(result);

            return CreatedAtRoute("GetPostById", new { id = result.PostId }, response);
        }

        // update product
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdatePostRequest p)
        {

            var request = _iMapper.Map<Post>(p);

            var result = await _iService.UpdateAsync(id, request);
            if (result == -1)
                return NotFound($"Product with ID {id} not found.");

            if (result <= 0)
                return StatusCode(500, "Failed to update post.");

            return NoContent();
        }

        // delete product
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _iService.DeleteAsync(id);

            if (result == -1)
                return NotFound($"Product with ID {id} not found.");

            if (result <= 0)
                return StatusCode(500, "Failed to delete post.");

            return NoContent();
        }
    }
}
