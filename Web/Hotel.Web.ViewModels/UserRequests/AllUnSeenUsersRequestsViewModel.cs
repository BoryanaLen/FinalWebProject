namespace Hotel.Web.ViewModels.UserRequests
{
    using System.Collections.Generic;

    public class AllUnSeenUsersRequestsViewModel : PagedListViewModel
    {
        public IEnumerable<DetailsUserRequestViewModel> UserRequests { get; set; }
    }
}
