using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Homework_4.Blog.Data.Repositories.Interfaces;
using Homework_4.Blog.Domain.Entities;
using Homework_4.Blog.Domain.Models;
using Homework_4.Blog.Services.Interfaces;

namespace Homework_4.Blog.Services.Derived
{
    public class PostService:IPostService
    {

        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostService(IMapper mapper, IPostRepository postRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }
        public async Task<ServiceResponseModel> Create(PostDto post)
        {
            var existingUser = await _userRepository.Get(u => u.Id == post.UserId);
            if (existingUser == null)
            {
                return new ServiceResponseModel($"Cannot find user with id {post.UserId}", false);
            }
            var postEntity = _mapper.Map<Post>(post);
            await _postRepository.Add(postEntity);
            return new ServiceResponseModel("Post Created!",true);
        }

        public async  Task<ServiceResponseModel> Update(PostDto post)
        {
            var existingPost = await _postRepository.Get(p => p.Id == post.Id);
            if (existingPost == null)
            {
                return new ServiceResponseModel($"Cannot find user with id {post.Id}", false);
            }
            var postEntity = _mapper.Map<Post>(post);
            await _postRepository.Update(postEntity);
            return new ServiceResponseModel("Post Updated!",true);
        }

        public async  Task<ServiceResponseModel> Delete(int id)
        {
            var existingPost = await _postRepository.Get(p => p.Id == id);
            if (existingPost == null)
            {
                return new ServiceResponseModel($"Cannot find user with id {id}", false);
            }

            await _postRepository.Delete(existingPost);
            return new ServiceResponseModel("Post Deleted!",true);
        }

        public async Task<List<PostDto>> GetAll()
        {
            var users = await _postRepository.GetAll();
            return _mapper.Map<List<PostDto>>(users);
        }

        public async Task<PostDto> GetById(int id)
        {
            var user = await _postRepository.Get(u => u.Id == id);
            return _mapper.Map<PostDto>(user);
        }
    }
}