
using aspnetserver.Data;
using aspnetserver.Service.PostInterface;
using Microsoft.EntityFrameworkCore;

namespace aspnetserver.Service
{
    public class PostService : IPost
    {

        public async Task<List<PostData>> GetPostAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Posts.ToListAsync();
            }
        }

        public async Task<PostData> GetPostByIdAsync(int postId)
        {
            using (var db = new AppDBContext())
            {
                return await db.Posts
                    .FirstOrDefaultAsync(post => post.PostId == postId);
            }


        }


        public async Task<bool> CreatePostAsync(PostData postToCreate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    await db.Posts.AddAsync(postToCreate);
                    return await db.SaveChangesAsync() >= 1;


                }
                catch (Exception)
                {

                    return false;
                }
            }
        }


        public async Task<bool> UpdatePostAsync(PostData postToCreate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    db.Posts.Update(postToCreate);
                    return await db.SaveChangesAsync() >= 1;


                }
                catch (Exception)
                {

                    return false;
                }
            }
        }


        public async Task<bool> DeletePostAsync(int postId)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    PostData postToDelete = await GetPostByIdAsync(postId);

                    db.Remove(postToDelete);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception)
                {

                    return false;
                }
            }
        }
        
    }
}
