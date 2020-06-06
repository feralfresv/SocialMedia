using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interface;
using SocialMedia.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepositoy : IPostRepository
    {
        private readonly SocialMediaContext _context;

        public PostRepositoy(SocialMediaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            return posts;
        }

        public async Task<Post> GetPost(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            return post;
        }

        public async Task InsertPost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            var currentpost = await GetPost(post.PostId);
            currentpost.Date = post.Date;
            currentpost.Description = post.Description;
            currentpost.Comments = post.Comments;
            currentpost.Image = post.Image;

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeletePost(int id)
        {
            var currentpost = await GetPost(id);
            _context.Posts.Remove(currentpost);

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }



        #region Example de DB
        //public async Task<IEnumerable<Posts>> GetPosts()
        //{
        //    var posts = Enumerable.Range(1, 10).Select(x => new Posts 
        //    { 
        //    PostId = x,
        //    Description = $"Description {x}",
        //    Date = DateTime.Now,
        //    Image = $"https://misapis.com/{x}",
        //    UserId = x*2
        //    });
        //    await Task.Delay(10);
        //    return posts;
        //}
        #endregion
    }
}
