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
    public class UserRatingsRepository : GenericRepository<UserRating>, IUserRatingsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRatingsRepository(ApplicationDbContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserRatingDto>> GetAll()
        {
            var result = await GetAllAsync();

            return result.ToUserRatingDtos();
        }

        public async Task<UserRatingDto> Add(UserRatingDto userRatingDto)
        {
            var userRating = new UserRating
            {
                UserAssessorId = userRatingDto.UserAssessorId,
                UserEvaluatedId = userRatingDto.UserEvaluatedId,
                Feedback = userRatingDto.Feedback,
                StarType = userRatingDto.StarType

            };

            await DbSet.AddAsync(userRating);

            if (await _dbContext.SaveChangesAsync() == 0)
            {
                return null;
            }

            userRatingDto.Id = userRating.Id;

            return userRatingDto;
        }
        public async Task<UserRatingDto> GetById(int id)
        {
            var userRating = await GetByIdAsync(id);

            return userRating.ToUserRatingDto();
        }
        public async Task<UserRatingDto> Update(UserRatingDto userRatingDto)
        {
            var result = await DbSet.SingleOrDefaultAsync(x => x.Id == userRatingDto.Id);

            await UpdateAsync(result);
            if (await _dbContext.SaveChangesAsync() == 0)
            {
                return null;
            }

            return new UserRatingDto
            {
                Id = result.Id,
                UserAssessorId = userRatingDto.UserAssessorId,
                UserEvaluatedId = userRatingDto.UserEvaluatedId,
                Feedback = userRatingDto.Feedback,
                StarType = userRatingDto.StarType
            };
        }

        public async Task Delete(int id)
        {
            await DeleteAsync(id);
            await _dbContext.SaveChangesAsync();
        }
    }
}

