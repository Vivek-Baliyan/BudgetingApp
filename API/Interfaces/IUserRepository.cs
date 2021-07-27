using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> SaveAllAsync();
        Task<IEnumerable<MemberDto>> GetUsers();
        Task<AppUser> GetUserById(int id);
    }
}