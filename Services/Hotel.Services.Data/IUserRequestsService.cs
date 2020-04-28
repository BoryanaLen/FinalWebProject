namespace Hotel.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Hotel.Data.Models;

    public interface IUserRequestsService
    {
        IEnumerable<UserRequest> All();

        Task<bool> AddUserRequestAsync(UserRequest request);

        Task<bool> DeleteByIdAsync(string id);

        Task<UserRequest> GetUserRequestByIdAsync(string id);

        IEnumerable<UserRequest> GetUnseenRequests();

        Task<bool> Seen(string id);

        Task<bool> Unseen(string id);

        IEnumerable<T> GetAllUserRequests<T>(int? count = null);
    }
}
