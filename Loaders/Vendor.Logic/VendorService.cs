using Vendor.Logic.Interfaces;

namespace Vendor.Logic
{
    public class VendorService : IVendorService
    {
        private ILoader _loader { get; set; }

        public VendorService(ILoaderFactory loaderFactory) {
            _loader = loaderFactory.GetLoader();
        }

        public async Task<IEnumerable<Dto.Vendor>> GetVendors(int skip, int count)
        {
            
            var vendors = await _loader.GetVendors();
            var result = vendors.Skip(skip).Take(count);
            return result;
        }

        public Task<Dto.Vendor> GetVendor(string id)
        {
            return _loader.GetVendor(id);
        }

        public Task Add(Dto.Vendor vendor)
        {
            return _loader.Add(vendor);
        }

        public Task Update(Dto.Vendor vendor)
        {
            return _loader.Update(vendor);
        }

        public Task Delete(string vendorId)
        {
            return _loader.Delete(vendorId);    
        }
    }
}
