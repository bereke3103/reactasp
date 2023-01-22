using aspnetserver.Data;
using aspnetserver.Service;
using Microsoft.AspNetCore.Mvc;

namespace aspnetserver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        PostService _postService;
        PostContext _context;

        public PostController(PostService postService, PostContext context)
        {
            _postService = postService;
            _context = context;
        }


       [HttpGet("/get-all-post")]

       public async Task<ActionResult<List<PostData>>> GetPostsAll()
        {
           return await _postService.GetPostAsync();
        }


        [HttpGet("/get-post-by-id/{postId}")]
        public async Task<ActionResult<PostData>> GetPostByIdAsync(int postId)
        {

            PostData postToReturn = await _postService.GetPostByIdAsync(postId);
            if (postToReturn != null)
            {
                return Ok(postToReturn);
            }
            else
            {
                return BadRequest();
            }

        }


        [HttpPost("/create-post")]
        public async Task<ActionResult<bool>> CreatePost(PostData postCreate)
        {
            bool createSuccessfull = await _postService.CreatePostAsync(postCreate);

            if (createSuccessfull)
            {
                return Ok("Create succesfull");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("/update-post")]
        public async Task<ActionResult<bool>> UpdatePostAsync(PostData pstToUpdate)
        {
            bool updateSuccessfull = await _postService.UpdatePostAsync(pstToUpdate);

            if (updateSuccessfull)
            {
                return Ok("Update Successfull");
            }
            else
            {
                return BadRequest();
            }

        }


        [HttpDelete("/delete-post-id/{postId}")]
        public async Task<ActionResult<bool>> DeletePostAsync(int postId)
        {
            bool deletePostId = await _postService.DeletePostAsync(postId);

            if (deletePostId)
            {
                return Ok("Delete successfull");
            }
            else
            {
                return BadRequest();
            }
            
          
        }

    }
}
