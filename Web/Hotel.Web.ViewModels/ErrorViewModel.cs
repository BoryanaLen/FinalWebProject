namespace Hotel.Web.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);

        public int StatusCode { get; set; }

        public string RequestPath { get; set; }

        public bool ShowRequestPath => !string.IsNullOrEmpty(this.RequestPath);
    }
}
