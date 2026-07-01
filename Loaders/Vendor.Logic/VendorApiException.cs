namespace Vendor.Logic
{
    public class VendorApiException : Exception
    {
        public VendorApiException(Exception innerExeption, string message) : base(message, innerExeption)
        {
        }
    }
}
