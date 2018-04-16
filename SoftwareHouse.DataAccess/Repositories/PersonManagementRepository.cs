using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftwareHouse.Contract.DataContracts;
using SoftwareHouse.Contract.Repositories;
using SoftwareHouse.DataAccess.CommonGeneric;
using SoftwareHouse.DataAccess.Models;

namespace SoftwareHouse.DataAccess.Repositories
{
    public class PersonManagementRepository : GenericRepository<ApplicationUser>, IPersonManagementRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PersonManagementRepository(ApplicationDbContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ApplicationUserDto>> GetAllUsers()
        {
            var result = await GetAllAsync();

            return result.Select(x =>
                new ApplicationUserDto
                {
                    Address = x.Address,
                    Email = x.Email,
                    Name = x.Name,
                    LastName = x.LastName,
                    BirthDayDateTime = x.BirthDayDateTime

                }).ToList();
        }

        public async Task<ApplicationUserDto> GetUser(string id)
        {
            var user = await DbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (user == null)
            {
                return null;
            }
            return new ApplicationUserDto
            {
                Id = user.Id,
                Address = user.Address,
                Email = user.Email,
                Name = user.Name,
                LastName = user.LastName,
                BirthDayDateTime = user.BirthDayDateTime
            };
        }

        public async Task<ApplicationUserDto> AddUser(ApplicationUserDto user)
        {
            var userEf = new ApplicationUser
            {
                Address = user.Address,
                Email = user.Email,
                Name = user.Name,
                LastName = user.LastName,
                BirthDayDateTime = user.BirthDayDateTime
            };
            
            await DbSet.AddAsync(userEf);
            
            if (await _dbContext.SaveChangesAsync() == 0)
            {
                return null;
            }

            user.Id = userEf.Id;

            return user;
        }

        public async Task<ApplicationUserDto> UpdateUser(ApplicationUserDto user)
        {
            var result = await DbSet.SingleOrDefaultAsync(x => x.Id == user.Id);

            await UpdateAsync(result);
            if (await _dbContext.SaveChangesAsync() == 0)
            {
                return null;
            }
                
     
            return new ApplicationUserDto
            {
                Id = user.Id,
                Address = user.Address,
                Email = user.Email,
                Name = user.Name,
                LastName = user.LastName,
                BirthDayDateTime = user.BirthDayDateTime
            };
        }

        public async Task Delete(string id)
        {
           await Delete(id);
           await _dbContext.SaveChangesAsync();
        }
    }
}
