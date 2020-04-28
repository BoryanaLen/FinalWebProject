namespace Hotel.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Hotel.Data.Common.Repositories;
    using Hotel.Data.Models;
    using Hotel.Services.Data.Common;
    using Hotel.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class UserRequestsService : IUserRequestsService
    {
        private readonly IDeletableEntityRepository<UserRequest> userRequestRepository;

        public UserRequestsService(IDeletableEntityRepository<UserRequest> userRequestRepository)
        {
            this.userRequestRepository = userRequestRepository;
        }

        public async Task<bool> AddUserRequestAsync(UserRequest request)
        {
            if (request.Title == null ||
                request.Content == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidPropertyErrorMessage));
            }

            await this.userRequestRepository.AddAsync(request);

            var result = await this.userRequestRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var request = await this.userRequestRepository
                .All()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (request == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidUserRequestIdErrorMessage, id));
            }

            request.IsDeleted = true;

            this.userRequestRepository.Update(request);

            var result = await this.userRequestRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<UserRequest> GetUserRequestByIdAsync(string id)
        {
            var request = await this.userRequestRepository.GetByIdWithDeletedAsync(id);

            if (request == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidUserRequestIdErrorMessage, id));
            }

            return request;
        }

        public IEnumerable<T> GetAllUserRequests<T>(int? count = null)
        {
            IQueryable<UserRequest> query =
                 this.userRequestRepository.All().OrderByDescending(x => x.RequestDate);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<UserRequest> All()
        {
            return this.userRequestRepository.All().ToList();
        }

        public IEnumerable<UserRequest> GetUnseenRequests()
        {
            return this.userRequestRepository
                .All()
                .Where(x => x.Seen == false)
                .ToList();
        }

        public async Task<bool> Seen(string id)
        {
            var request = await this.GetUserRequestByIdAsync(id);

            if (request == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidUserRequestIdErrorMessage, id));
            }

            request.Seen = true;

            this.userRequestRepository.Update(request);

            var result = await this.userRequestRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Unseen(string id)
        {
            var request = await this.userRequestRepository
               .All()
               .FirstOrDefaultAsync(d => d.Id == id);

            if (request == null)
            {
                throw new ArgumentNullException(string.Format(ServicesDataConstants.InvalidUserRequestIdErrorMessage, id));
            }

            request.Seen = false;

            this.userRequestRepository.Update(request);

            var result = await this.userRequestRepository.SaveChangesAsync();

            return result > 0;
        }
    }
}
