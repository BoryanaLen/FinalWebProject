namespace Hotel.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Services.Data;

    public class PaymentTypesController : AdministrationController
    {
        private readonly IPaymentTypesService paymentTypesService;

        public PaymentTypesController(IPaymentTypesService paymentTypesService)
        {
            this.paymentTypesService = paymentTypesService;
        }
    }
}
