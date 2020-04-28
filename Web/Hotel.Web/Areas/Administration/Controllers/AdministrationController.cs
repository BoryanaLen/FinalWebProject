namespace Hotel.Web.Areas.Administration.Controllers
{
    using Hotel.Common;
    using Hotel.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        protected bool IsImageTypeValid(string fileType)
        {
            return fileType == GlobalConstants.JpgFormat || fileType == GlobalConstants.PngFormat || fileType == GlobalConstants.JpegFormat;
        }
    }
}
