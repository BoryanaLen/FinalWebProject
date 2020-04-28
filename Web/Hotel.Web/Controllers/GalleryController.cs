namespace Hotel.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class GalleryController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
