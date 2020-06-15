using SocialMedia.Core.Entities;
using System;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepositroy { get; }
        IRepository<User> UserRepositroy { get; }
        IRepository<Comment> CommentRepositroy { get; }
        void SaveChange();
        Task SaveChangeAsync();
    }
}
