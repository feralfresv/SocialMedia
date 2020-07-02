using Microsoft.Extensions.Options;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SecurityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<Security> GetLoginByCredentials(UserLogin login)
        {
            return await _unitOfWork.SecurityRepositroy.GetLoginByCredential(login);
        }

        public async Task RegisterUser(Security security)
        {
            await _unitOfWork.SecurityRepositroy.Add(security);
            await _unitOfWork.SaveChangeAsync();
        }


    }
}
