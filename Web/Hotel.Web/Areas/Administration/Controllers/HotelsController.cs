namespace Hotel.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Services.Data;

    public class HotelsController : AdministrationController
    {
        private readonly IHotelsService hotelsService;

        public HotelsController(IHotelsService hotelsService)
        {
            this.hotelsService = hotelsService;
        }
    }
}
