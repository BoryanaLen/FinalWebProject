namespace Hotel.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Hotel.Data.Models;
    using Hotel.Services.Data;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.UserRequests;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : BaseController
    {
        private readonly IUserRequestsService userRequestsService;

        public ContactsController(IUserRequestsService userRequestsService)
        {
            this.userRequestsService = userRequestsService;
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                UserRequestViewModel model = new UserRequestViewModel
                {
                    Email = this.User.Identity.Name,
                };

                return this.View(model);
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(UserRequestViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.TempData["info"] = "Your request was accepted!";

            UserRequest request = AutoMapperConfig.MapperInstance.Map<UserRequest>(model);

            request.RequestDate = DateTime.UtcNow;

            await this.userRequestsService.AddUserRequestAsync(request);

            return this.Redirect($"/Contacts/Index");
        }
    }
}
