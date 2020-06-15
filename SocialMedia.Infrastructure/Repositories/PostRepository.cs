﻿using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interface;
using SocialMedia.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(SocialMediaContext socialMediaContext) : base(socialMediaContext) { }

        public async Task<IEnumerable<Post>> GetPostsByUser(int userId)
        {
            return await _entites.Where(o => o.UserId == userId).ToListAsync();
        }
    }
}
