using DapperUserCRUD.DataAccess.Repository.Interface;
using DapperUserCRUD.Objects.Constants;
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
            var user = await _userRepository.GetByLoginAsync(dto.Login);

            if (user != null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorCode = (int)StatusCode.UserAlreadyExist,
                    ErrorMessage = ErrorMessages.UserAlreadyExists
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
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorCode = (int)StatusCode.UserNotFound,
                    ErrorMessage = ErrorMessages.UserNotFound
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
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorCode = (int)StatusCode.UserNotFound,
                    ErrorMessage = ErrorMessages.UserNotFound
                };
            }

            return new BaseResult<UserDto>
            {
                Data = user.Adapt<UserDto>()
            };
        }

        public async Task<CollectionResult<UserDto>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();

            if (!users.Any())
            {
                return new CollectionResult<UserDto>()
                {
                    ErrorCode = (int)StatusCode.UsersNotFound,
                    ErrorMessage = ErrorMessages.UsersNotFound
                };
            }

            var usersDtos = users.Select(x => x.Adapt<UserDto>()).ToArray();

            return new CollectionResult<UserDto>
            {
                Data = usersDtos,
                Count = usersDtos.Length
            };
        }

        public async Task<BaseResult<UserDto>> UpdateUserDataAsync(UpdateUserDto dto)
        {
            var user = await _userRepository.GetByLoginAsync(dto.Login);

            if (user != null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessages.UserAlreadyExistWithThisData,
                    ErrorCode = (int)StatusCode.UserAlreadyExist
                };
            }

            user = await _userRepository.GetByIdAsync(dto.Id);

            if (user == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessages.UserNotFound,
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
