using ABankApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABankApi.Data
{
    public interface IUserRepository
    {
        Task<User?> GetUserByPhoneAndPassword(string telefono, string password);
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto?> GetUserById(int id);
        Task<UserDto> CreateUser(User user);
        Task<bool> updateUser(int id, User user);
        Task<bool> DeleteUser(int id);
    }
}
