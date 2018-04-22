using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareHouse.Contract.Common;
using SoftwareHouse.Contract.DataContracts;
using SoftwareHouse.Contract.DataContracts.QueryClass;
using SoftwareHouse.Contract.Helpers;
using SoftwareHouse.Contract.Repositories;
using SoftwareHouse.Contract.Services;

namespace SoftwareHouse.Services.Services
{
    public class PersonManagementService : IPersonManagementService
    {
        private readonly IPersonManagementRepository _personRepository;

        public PersonManagementService(IPersonManagementRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PagedList<ApplicationUserDto>> GetAllUsers(EmployeesResourceParameter employeesResourceParameter)
        {
            var result = await _personRepository.GetAllUsers(employeesResourceParameter);

            if (!result.Any())
            {
                return null;
            }

            return result;
        }


        public async Task<CommonResult> Add(ApplicationUserDto user)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                return CommonResult.Failure("Cannot create user without name provided.");
            }

            if (string.IsNullOrEmpty(user.LastName))
            {
                return CommonResult.Failure("Cannot create user without last name provided.");
            }

            var person = await _personRepository.GetUser(user.Id);

            if (person != null && !person.IsDeleted && person.Name == user.Name && person.LastName == user.LastName)
            {
                return CommonResult.Failure("User name and lastname already exists.");
            }

            await _personRepository.AddUser(user);

            return CommonResult.Success();
        }

        public async Task Delete(string id)
        {
            await _personRepository.Delete(id);
      
        }

        public async Task<CommonResult<ApplicationUserDto>> GetUser(string id)
        {
            var person = await _personRepository.GetUser(id);

            if (person == null || person.IsDeleted)
            {
                return CommonResult<ApplicationUserDto>.Failure<ApplicationUserDto>("Problem occured during fetching project with given id.");
            }
            return CommonResult<ApplicationUserDto>.Success(person);
        }

        public async Task<CommonResult> Update(ApplicationUserDto user)
        {
            var updateApplicationUserDto = await _personRepository.UpdateUser(user);

            if (updateApplicationUserDto == null || updateApplicationUserDto.IsDeleted)
            {
                return CommonResult<ApplicationUserDto>.Failure<ApplicationUserDto>("Problem occured updating entity.");
            }

            return CommonResult<ApplicationUserDto>.Success(user);
        }
    }
}
