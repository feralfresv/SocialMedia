using SocialMedia.Core.Entities;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interface
{
    public interface ISecurityRepository : IRepository<Security>
    {
        Task<Security> GetLoginByCredential(UserLogin login);
    }
}