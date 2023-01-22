using aspnetserver.Data;
using Microsoft.Extensions.Hosting;

namespace aspnetserver.Service.PostInterface
{
    public interface IPost
    {

        public Task<List<PostData>> GetPostAsync();

        public Task<PostData> GetPostByIdAsync(int postId);

        public Task<bool> CreatePostAsync(PostData postToCreate);

        public Task<bool> UpdatePostAsync(PostData postToCreate);

        public Task<bool> DeletePostAsync(int postId);
    }
}
