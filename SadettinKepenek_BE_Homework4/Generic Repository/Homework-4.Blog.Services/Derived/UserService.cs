using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Homework_4.Blog.Data.Repositories.Interfaces;
using Homework_4.Blog.Domain.Entities;
using Homework_4.Blog.Domain.Models;
using Homework_4.Blog.Services.Interfaces;

namespace Homework_4.Blog.Services.Derived
{
    public class UserService:IUserService
    {

        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ServiceResponseModel> Create(UserDto user)
        {
            if (user.Id == 0)
            {
                return new ServiceResponseModel("User Id cannot be null",false);
            }
            var userEntity = _mapper.Map<User>(user);
            await _userRepository.Add(userEntity);
            return new ServiceResponseModel("User Created!",true);
        }

        public async  Task<ServiceResponseModel> Update(UserDto user)
        {
            var existingUser = await _userRepository.Get(u => u.Id == user.Id);
            if (existingUser == null)
            {
                return new ServiceResponseModel($"Cannot find user with id {user.Id}", false);
            }
            var userEntity = _mapper.Map<User>(user);
            await _userRepository.Update(userEntity);
            return new ServiceResponseModel("User Updated!",true);
        }
        public async  Task<ServiceResponseModel> Delete(int id)
        {
            var existingUser = await _userRepository.Get(p => p.Id == id);
            if (existingUser == null)
            {
                return new ServiceResponseModel($"Cannot find user with id {id}", false);
            }

            await _userRepository.Delete(existingUser);
            return new ServiceResponseModel("Post Deleted!",true);
        }
        public async Task<List<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _userRepository.Get(u => u.Id == id);
            return _mapper.Map<UserDto>(user);
        }
    }
}