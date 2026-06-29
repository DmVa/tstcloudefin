namespace Vendor.Logic.Interfaces
{
    public interface IVendorService
    {
        public Task<IEnumerable<Dto.Vendor>> GetVendors(int skip, int count);
        public Task<Dto.Vendor> GetVendor(string vendorId);
        public Task Add(Dto.Vendor vendor);
        public Task Update(Dto.Vendor vendor);
        public Task Delete(string vendorId);
    }
}
