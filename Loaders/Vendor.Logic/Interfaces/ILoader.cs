namespace Vendor.Logic.Interfaces
{
    internal interface ILoader
    {
        public Task<IEnumerable<Dto.Vendor>> GetVendors();
        public Task<Dto.Vendor> GetVendor(string id);
        public Task Add(Dto.Vendor vendor);
        public Task Update(Dto.Vendor vendor);
        public Task Delete(string vendorId);
    }
}
