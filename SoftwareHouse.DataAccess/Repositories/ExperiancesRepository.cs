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
using SoftwareHouse.DataAccess.Models.UserInformation;

namespace SoftwareHouse.DataAccess.Repositories
{
    public class ExperiancesRepository: GenericRepository<Experiance>, IExperiancesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ExperiancesRepository(ApplicationDbContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ExperianceDto>> GetAll()
        {
            var result = await GetAllAsync();

            return result.ToExperianceDtos();
        }

        public async Task<bool> CheckIfExperianceAlreadyExist(string userId, ExperianceType type)
        {
            var result = await DbSet.AnyAsync(x => x.UserId == userId && x.ExperianceType == type);

            return result;
        }

        public async Task<ExperianceDto> GetById(int id)
        {
            var experiance = await GetByIdAsync(id);

            return experiance?.ToExperianceDto();
        }

        public async Task<ExperianceDto> Add(ExperianceDto experianceDto)
        {
            var experiance = new Experiance
            {
               ExperianceType = experianceDto.ExperianceType,
               UserId = experianceDto.UserId
            };

            await DbSet.AddAsync(experiance);

            if (await _dbContext.SaveChangesAsync() == 0)
            {
                return null;
            }

            experianceDto.Id = experiance.Id;

            return experianceDto;
        }

        public async Task<ExperianceDto> Update(ExperianceDto user)
        {
            var result = await DbSet.SingleOrDefaultAsync(x => x.Id == user.Id);

            await UpdateAsync(result);
            if (await _dbContext.SaveChangesAsync() == 0)
            {
                return null;
            }

            return result.ToExperianceDto();
        }

        public async Task Delete(int id)
        {
            await DeleteAsync(id);
            await _dbContext.SaveChangesAsync();
        }
    }
}
