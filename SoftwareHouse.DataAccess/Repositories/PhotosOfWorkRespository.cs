using System;
using System.Collections.Generic;
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
    public class PhotosOfWorkRespository : GenericRepository<UserWorkPhoto>, IPhotosOfWorkRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public PhotosOfWorkRespository(ApplicationDbContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserWorkPhotoDto>> GetAll()
        {
            var result = await GetAllAsync();

            return result.ToPhotoDtos();
        }

        public async Task<UserWorkPhotoDto> GetById(int id)
        {
            var photo = await GetByIdAsync(id);

            return photo?.ToPhotoDto();
        }

        public async Task<UserWorkPhotoDto> Add(UserWorkPhotoDto photoDto)
        {
            var photo = new UserWorkPhoto
            {
                ApplicationUserId = photoDto.ApplicationUserId,
                PhotoUrl = photoDto.PhotoUrl
            };

            await DbSet.AddAsync(photo);

            if (await _dbContext.SaveChangesAsync() == 0)
            {
                return null;
            }

            photoDto.Id = photo.Id;

            return photoDto;
        }

        public async Task<UserWorkPhotoDto> Update(UserWorkPhotoDto photo)
        {
            var result = await DbSet.SingleOrDefaultAsync(x => x.Id == photo.Id);

            await UpdateAsync(result);
            if (await _dbContext.SaveChangesAsync() == 0)
            {
                return null;
            }
            return photo;
        }

        public async Task Delete(int id)
        {
            await DeleteAsync(id);
            await _dbContext.SaveChangesAsync();
        }
    }
}
