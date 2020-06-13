using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interface;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SocialMediaContext _socialMediaContext;
        private readonly DbSet<T> _entites;

        public BaseRepository(SocialMediaContext socialMediaContext)
        {
            _socialMediaContext = socialMediaContext;
            _entites = socialMediaContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entites.ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            return await _entites.FindAsync(id);
        }
        public async Task Add(T entity)
        {
            _entites.Add(entity);
            await _socialMediaContext.SaveChangesAsync();
        }
        public async Task Update(T entity)
        {
            _entites.Update(entity);
            await _socialMediaContext.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            _entites.Remove(entity);
            _socialMediaContext.SaveChanges();
        }

    }
}
