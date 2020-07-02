using SocialMedia.Core.Entities;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interface
{
    public interface ISecurityService
    {
        Task<Security> GetLoginByCredentials(UserLogin login);
        Task RegisterUser(Security security);
    }
}