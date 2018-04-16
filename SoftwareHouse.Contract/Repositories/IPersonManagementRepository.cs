using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SoftwareHouse.Contract.DataContracts;

namespace SoftwareHouse.Contract.Repositories
{
    public interface IPersonManagementRepository
    {
        Task<IEnumerable<ApplicationUserDto>> GetAllUsers();
        Task<ApplicationUserDto> GetUser(string id);
        Task<ApplicationUserDto> AddUser(ApplicationUserDto user);
        Task<ApplicationUserDto> UpdateUser(ApplicationUserDto user);
        Task Delete(string id);
    }
}
