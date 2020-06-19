using SocialMedia.Core.QueryFilters;
using SocialMedia.Infrastructure.Interfaces;
using System;

namespace SocialMedia.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUrl;

        public UriService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public Uri GetPostPaginationUry(PostQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUrl}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
