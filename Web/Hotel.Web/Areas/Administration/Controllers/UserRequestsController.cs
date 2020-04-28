namespace Hotel.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Common;
    using Hotel.Services.Data;
    using Hotel.Web.ViewModels.UserRequests;
    using Microsoft.AspNetCore.Mvc;

    public class UserRequestsController : AdministrationController
    {
        private readonly IUserRequestsService userRequestsService;

        public UserRequestsController(IUserRequestsService userRequestsService)
        {
            this.userRequestsService = userRequestsService;
        }

        public IActionResult Seen(int page = GlobalConstants.DefaultPageNumber, int perPage = GlobalConstants.PageSize)
        {
            int requestsCount = this.userRequestsService
               .GetAllUserRequests<DetailsUserRequestViewModel>()
               .Where(x => x.Seen == true)
               .OrderByDescending(x => x.RequestDate).Count();

            var pagesCount = (int)Math.Ceiling(requestsCount / (decimal)perPage);

            var requests = this.userRequestsService
               .GetAllUserRequests<DetailsUserRequestViewModel>()
               .Where(x => x.Seen == true)
               .OrderByDescending(x => x.RequestDate)
               .Skip(perPage * (page - 1))
               .Take(perPage)
               .ToList();

            var model = new AllUsersRequestsViewModel
            {
                UserRequests = requests,
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            return this.View(model);
        }

        public IActionResult UnSeen(int page = GlobalConstants.DefaultPageNumber, int perPage = GlobalConstants.PageSize)
        {
            int requestsCount = this.userRequestsService
               .GetAllUserRequests<DetailsUserRequestViewModel>()
               .Where(x => x.Seen == false)
               .OrderByDescending(x => x.RequestDate)
               .Count();

            var pagesCount = (int)Math.Ceiling(requestsCount / (decimal)perPage);

            var requests = this.userRequestsService
               .GetAllUserRequests<DetailsUserRequestViewModel>()
               .Where(x => x.Seen == false)
               .OrderByDescending(x => x.RequestDate)
               .Skip(perPage * (page - 1))
               .Take(perPage)
               .ToList();

            var model = new AllUnSeenUsersRequestsViewModel
            {
                UserRequests = requests,
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            return this.View(model);
        }

        public async Task<IActionResult> Read (string id)
        {
            var requestToEdit = await this.userRequestsService.GetUserRequestByIdAsync(id);

            var userRequestsViewModel = new ReadUserRequestViewModel
            {
                Id = id,
                Title = requestToEdit.Title,
                Content = requestToEdit.Content,
                RequestDate = requestToEdit.RequestDate,
                Email = requestToEdit.Email,
                Seen = requestToEdit.Seen,
            };

            await this.userRequestsService.Seen(id);

            return this.View(userRequestsViewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var requestToDelete = await this.userRequestsService.GetUserRequestByIdAsync(id);

            var userRequestsViewModel = new DeleteUserRequestViewModel
            {
                Id = id,
                Title = requestToDelete.Title,
                Content = requestToDelete.Content,
                RequestDate = requestToDelete.RequestDate,
                Email = requestToDelete.Email,
                Seen = requestToDelete.Seen,
            };

            return this.View(userRequestsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteUserRequestViewModel model)
        {
            var id = model.Id;

            await this.userRequestsService.DeleteByIdAsync(id);

            return this.Redirect($"/Administration/UserRequests/UnSeen");
        }

        public async Task<IActionResult> UnseenRequest(string id)
        {
            await this.userRequestsService.Unseen(id);

            return this.Redirect($"/Administration/UserRequests/UnSeen");
        }
    }
}
