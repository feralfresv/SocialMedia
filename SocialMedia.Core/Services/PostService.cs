﻿using Microsoft.Extensions.Options;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interface;
using SocialMedia.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public PostService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<Post> GetPost(int id)
        {
            return await _unitOfWork.PostRepositroy.GetById(id);
        }

        public PagedList<Post> GetPosts(PostQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var posts = _unitOfWork.PostRepositroy.GetAll();
            if (filters.UserId != null)
            {
                posts = posts.Where(o => o.UserId == filters.UserId);
            }
            if (filters.Date != null)
            {
                posts = posts.Where(o => o.Date.ToShortDateString() == filters.Date?.ToShortDateString());
            }
            if (filters.Description != null)
            {
                posts = posts.Where(o => o.Description.ToLower().Contains(filters.Description.ToLower()));
            }

            var pagePosts = PagedList<Post>.Create(posts, filters.PageNumber, filters.PageSize);

            return pagePosts;
        }

        public async Task InsertPost(Post post)
        {
            var user = await _unitOfWork.PostRepositroy.GetById(post.UserId);
            if (user == null)
            {
                throw new BusinessException("User doesn't exist");
            }

            var userPosts = await _unitOfWork.PostRepositroy.GetPostsByUser(post.UserId);
            if (userPosts.Count() < 10)
            {
                var lasPost = userPosts.OrderByDescending(o => o.Date).FirstOrDefault();
                if ((DateTime.Now - lasPost.Date).TotalDays < 7)
                {
                    throw new BusinessException("Ypu are not able to publish the post");
                }
            }

            if (post.Description.Contains("Sexo") || post.Description.Contains("sexo"))
            {
                throw new BusinessException("Content not allow");
            }

            await _unitOfWork.PostRepositroy.Add(post);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            _unitOfWork.PostRepositroy.Update(post);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepositroy.Delete(id);
            return true;
        }

    }
}
