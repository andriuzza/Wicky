using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftwareHouse.Contract.DataContracts;
using SoftwareHouse.Contract.Repositories;
using SoftwareHouse.DataAccess.CommonGeneric;
using SoftwareHouse.DataAccess.Models.UserInformation;

namespace SoftwareHouse.DataAccess.Repositories
{
    public class QualificationsRepository : GenericRepository<Qualification>, IQualificationsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public QualificationsRepository(ApplicationDbContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<QualificationDto>> GetAll()
        {
            var result = await GetAllAsync();

            return result.ToQualificationDtos();
        }

        public async Task<QualificationDto> Add(QualificationDto qualificationDto)
        {
            var qualification = new Qualification
            {
                ApplicationUserId = qualificationDto.ApplicationUserId,
                QualificationField = qualificationDto.QualificationField
            };

            await DbSet.AddAsync(qualification);

            if (await _dbContext.SaveChangesAsync() == 0)
            {
                return null;
            }

            qualificationDto.Id = qualification.Id;

            return qualificationDto;
        }
        public async Task<QualificationDto> GetById(int id)
        {
            var qualification = await GetByIdAsync(id);

            return qualification.ToQualificationDto();
        }
        public async Task<QualificationDto> Update(QualificationDto qualificationDto)
        {
            var result = await DbSet.SingleOrDefaultAsync(x => x.Id == qualificationDto.Id);

            await UpdateAsync(result);
            if (await _dbContext.SaveChangesAsync() == 0)
            {
                return null;
            }

            return new QualificationDto
            {
                Id = result.Id,
                ApplicationUserId = qualificationDto.ApplicationUserId,
                QualificationField = qualificationDto.QualificationField
            };
        }

        public async Task Delete(int id)
        {
            await DeleteAsync(id);
            await _dbContext.SaveChangesAsync();
        }
    }
}
