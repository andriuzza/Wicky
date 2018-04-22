using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SoftwareHouse.Contract.DataContracts;
using SoftwareHouse.Contract.DataContracts.QueryClass;
using SoftwareHouse.Contract.Helpers;

namespace SoftwareHouse.Contract.Repositories
{
    public interface IPersonManagementRepository
    {
        Task<PagedList<ApplicationUserDto>> GetAllUsers(EmployeesResourceParameter employeesResourceParameter);
        Task<ApplicationUserDto> GetUser(string id);
        Task<ApplicationUserDto> AddUser(ApplicationUserDto user);
        Task<ApplicationUserDto> UpdateUser(ApplicationUserDto user);
        Task Delete(string id);
    }
}
