using DapperUserCRUD.DataAccess.Repository.Interface;
using DapperUserCRUD.Objects.Dto;
using DapperUserCRUD.Objects.Entity;
using DapperUserCRUD.Objects.Enum;
using DapperUserCRUD.Objects.Helpers;
using DapperUserCRUD.Objects.Result;
using DapperUserCRUD.Service.Interface;
using Mapster;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Reflection;

namespace DapperUserCRUD.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResult<UserDto>> AddUserAsync(CreateUserDto dto)
        {
            var user = (await _userRepository.GetUsersAsync()).FirstOrDefault(x => x.Login == dto.Login);

            if (user != null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorCode = (int)StatusCode.UserAlreadyExist,
                    ErrorMessage = "User already exists"
                };
            }
            user = new User()
            {
                Login = dto.Login,
                Password = HashPasswordHelper.HashPassword(dto.Password),
                Email = dto.Email,
                Age = dto.Age,
                CreatedAt = DateTime.Now,
            };

            await _userRepository.CreateAsync(user);

            return new BaseResult<UserDto>()
            {
                Data = user.Adapt<UserDto>()
            };
        }

        public async Task<BaseResult<UserDto>> DeleteUserAsync(Guid id)
        {
            var user = (await _userRepository.GetUsersAsync()).FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorCode = (int)StatusCode.UserNotFound,
                    ErrorMessage = "User not found"
                };
            }

            await _userRepository.DeleteAsync(user.Id);

            return new BaseResult<UserDto>()
            {
                Data = user.Adapt<UserDto>()
            };
        }

        public async Task<BaseResult<UserDto>> GetUserAsync(Guid id)
        {
            var user = (await _userRepository.GetUsersAsync()).FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorCode = (int)StatusCode.UserNotFound,
                    ErrorMessage = "User not found"
                };
            }

            return new BaseResult<UserDto>
            {
                Data = user.Adapt<UserDto>()
            };
        }

        public async Task<CollectionResult<UserDto>> GetUsersAsync()
        {
            var users = (await _userRepository.GetUsersAsync())
                .Select(x => new UserDto() { Id = x.Id, Age = x.Age, Login = x.Login, Email = x.Email})
                .ToArray();

            if (users == null)
            {
                return new CollectionResult<UserDto>()
                {
                    ErrorCode = (int)StatusCode.UsersNotFound,
                    ErrorMessage = "Users not found"
                };
            }

            return new CollectionResult<UserDto>
            {
                Data = users,
                Count = users.Length
            };
        }

        public async Task<BaseResult<UserDto>> UpdateUserDataAsync(UpdateUserDto dto)
        {
            var user = (await _userRepository.GetUsersAsync()).FirstOrDefault(x => x.Login == dto.Login);

            if (user != null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = "User already exist with this data",
                    ErrorCode = (int)StatusCode.UserAlreadyExist
                };
            }

            user = (await _userRepository.GetUsersAsync()).FirstOrDefault(x => x.Id == dto.Id);

            if (user == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = "User is not found",
                    ErrorCode = (int)StatusCode.UserNotFound
                };
            }

            user.Login = dto.Login;
            user.Email = dto.Email;
            user.Age = dto.Age;

            await _userRepository.UpdateAsync(user);

            return new BaseResult<UserDto>()
            {
                Data = user.Adapt<UserDto>(),
            };
        }
    }
}
