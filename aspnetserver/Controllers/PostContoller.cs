using aspnetserver.Data;
using Microsoft.EntityFrameworkCore;

namespace aspnetserver.Controllers
{
    internal static class PostContoller
    {
        internal async static Task<List<Post>> GetPostAsync()
        {
           using(var db = new AppDBContext())
	         {
                return await db.Posts.ToListAsync();
	         }
        }

        internal async static Task<Post> GetPostByIdAsync(int postId)
        {
            using (var db = new AppDBContext())
            {
                return await db.Posts
                    .FirstOrDefaultAsync(post => post.PostId == postId);
            }
        }

        internal async static Task<bool> CreatePostAsync(Post postToCreate)
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


        internal async static Task<bool> UpdatePostAsync(Post postToCreate)
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


        internal async static Task<bool> DeletePostAsync(int postId)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    Post postToDelete = await GetPostByIdAsync(postId);

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
