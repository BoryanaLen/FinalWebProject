namespace Hotel.Web.ViewModels.UserRequests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllUsersRequestsViewModel : PagedListViewModel
    {
        public IEnumerable<DetailsUserRequestViewModel> UserRequests { get; set; }
    }
}
