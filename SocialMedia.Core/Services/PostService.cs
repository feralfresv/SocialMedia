using SocialMedia.Core.Entities;
using SocialMedia.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async  Task<Post> GetPost(int id)
        {
            return await _unitOfWork.PostRepositroy.GetById(id);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _unitOfWork.PostRepositroy.GetAll();
        }

        public async Task InsertPost(Post post)
        {
            var user = await _unitOfWork.PostRepositroy.GetById(post.UserId);
            if (user == null)
            {
                throw new Exception("User doesn't exist");
            }
            if (post.Description.Contains("Sexo") || post.Description.Contains("sexo"))
            {
                throw new Exception("Content not allow");
            }

            await _unitOfWork.PostRepositroy.Add(post);
        }

        public async Task<bool> UpdatePost(Post post)
        {
                await _unitOfWork.PostRepositroy.Update(post);
                return true;
        }
        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepositroy.Delete(id);
            return true;
        }

    }
}
