using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Post> PostRepositroy { get; }
        IRepository<User> UserRepositroy { get; }

        IRepository<Comment> CommentRepositroy { get; }

        void SaveChange();
        Task SaveChangeAsync();
    }
}
