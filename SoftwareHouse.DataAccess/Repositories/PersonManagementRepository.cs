using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftwareHouse.Contract.DataContracts;
using SoftwareHouse.Contract.DataContracts.QueryClass;
using SoftwareHouse.Contract.Helpers;
using SoftwareHouse.Contract.Repositories;
using SoftwareHouse.DataAccess.CommonGeneric;
using SoftwareHouse.DataAccess.Models;
using SoftwareHouse.DataAccess.Models.UserInformation;

namespace SoftwareHouse.DataAccess.Repositories
{
    public class PersonManagementRepository : GenericRepository<ApplicationUser>, IPersonManagementRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PersonManagementRepository(ApplicationDbContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }
         
        public async Task<PagedList<ApplicationUserDto>> GetAllUsers(EmployeesResourceParameter employeesResourceParameter)
        {
            var result = DbSet.Include(x => x.Qualifications)
                .Include(x => x.UserRatings)
                .Include(x => x.WorkPhotos)
                .Include(x => x.Experiances);

            var pagedList = PagedList<ApplicationUser>
                .Create(result, employeesResourceParameter.pageNumber, employeesResourceParameter.PageSize);

            var list = result.ToApplicationUserDtoList().ToList();

            return new PagedList<ApplicationUserDto>(list, pagedList.Count,
                employeesResourceParameter.pageNumber, employeesResourceParameter.PageSize );
        }

        public async Task<ApplicationUserDto> GetUser(string id)
        {
            var user = await DbSet
                .Include(x=>x.Qualifications)
                .Include(x=>x.UserRatings)
                .Include(x=>x.WorkPhotos)
                .Include(x=>x.Experiances)
                .SingleOrDefaultAsync(x => x.Id.Equals(id));
            
            return user?.ToApplicationUserDto();
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

    public static class PersonManagementExtension
    {
        public static ApplicationUserDto ToApplicationUserDto(this ApplicationUser user)
        {
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
        public static IEnumerable<ApplicationUserDto> ToApplicationUserDtoList(this IEnumerable<ApplicationUser> users)
        {
            return users.Where(x => x.IsDeleted == false).Select(
                user => new ApplicationUserDto
                {
                    Id = user.Id,
                    Address = user.Address,
                    Email = user.Email,
                    Name = user.Name,
                    LastName = user.LastName,
                    BirthDayDateTime = user.BirthDayDateTime,
                    WorkPhotos = user.WorkPhotos.ToPhotoDtos(),
                    UserRatings = user.UserRatings.ToUserRatingDtos(),
                    Experiances = user.Experiances.ToExperianceDtos(),
                    Qualifications = user.Qualifications.ToQualificationDtos()
                    
                }).ToList();
        }
    }

    public static class PersonInfo
    {
        /*--------------------------------*/
        public static IEnumerable<UserWorkPhotoDto> ToPhotoDtos(this IEnumerable<UserWorkPhoto> photos)
        {
            if (photos == null)
            {
                return null;
            }

            return photos.Select(x =>
            new UserWorkPhotoDto
            {
                Id = x.Id,
                PhotoUrl = x.PhotoUrl
            });
        }

        public static UserWorkPhotoDto ToPhotoDto(this UserWorkPhoto photo)
        {
            if (photo == null)
            {
                return null;
            }

            return
                new UserWorkPhotoDto
                {
                    Id = photo.Id,
                    PhotoUrl = photo.PhotoUrl
                };
        }

        /*--------------------------------*/
        public static IEnumerable<ExperianceDto> ToExperianceDtos(this IEnumerable<Experiance> experiances)
        {
            return experiances?.Select(x =>
                new ExperianceDto
                {
                    ExperianceType = (ExperianceType) x.ExperianceType
                });
        }
        public static ExperianceDto ToExperianceDto(this Experiance experiance)
        {
            if (experiance == null)
            {
                return null;
            }

            return
                new ExperianceDto
                {
                    ExperianceType = (ExperianceType) experiance.ExperianceType
                };
        }
        /*--------------------------------*/
        public static IEnumerable<UserRatingDto> ToUserRatingDtos(this IEnumerable<UserRating> ratings)
        {
            if (ratings == null)
            {
                return null;
            }

            return ratings.Select(x =>
                new UserRatingDto
                {
                   UserAssessorId = x.UserAssessorId,
                   UserEvaluatedId = x.UserEvaluatedId,
                   Feedback = x.Feedback
                });
        }
        public static UserRatingDto ToUserRatingDto(this UserRating rating)
        {
            if (rating == null)
            {
                return null;
            }

            return 
                new UserRatingDto
                {
                    UserAssessorId = rating.UserAssessorId,
                    UserEvaluatedId = rating.UserEvaluatedId,
                    Feedback = rating.Feedback
                };
        }
        /*--------------------------------*/
        public static IEnumerable<QualificationDto> ToQualificationDtos(this IEnumerable<Qualification> qualifications)
        {
            if (qualifications == null)
            {
                return null;
            }

            return qualifications.Select(x =>
                new QualificationDto
                {
                    Id = x.Id,
                    QualificationField = x.QualificationField
                });
        }

        public static QualificationDto ToQualificationDto(this Qualification qualification)
        {
            if (qualification == null)
            {
                return null;
            }

            return
                new QualificationDto
                {
                    Id = qualification.Id,
                    ApplicationUserId = qualification.ApplicationUserId,
                    QualificationField = qualification.QualificationField,
                };
        }


    }
}
