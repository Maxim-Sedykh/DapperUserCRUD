using DapperUserCRUD.Objects.Dto;
using DapperUserCRUD.Objects.Result;

namespace DapperUserCRUD.Service.Interface
{
    public interface IUserService
    {
        Task<CollectionResult<UserDto>> GetUsersAsync();

        Task<BaseResult<UserDto>> DeleteUserAsync(Guid id);

        Task<BaseResult<UserDto>> GetUserAsync(Guid id);

        Task<BaseResult<UserDto>> UpdateUserDataAsync(UpdateUserDto dto);

        Task<BaseResult<UserDto>> AddUserAsync(CreateUserDto dto);
    }
}
