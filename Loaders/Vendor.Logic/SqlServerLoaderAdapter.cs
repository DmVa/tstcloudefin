using SqlServerLoader;
using Vendor.Logic.Interfaces;

namespace Vendor.Logic
{
    internal class SqlServerLoaderAdapter : ILoader
    {
        private readonly DataLoader _loader;

        public SqlServerLoaderAdapter(string server, string user, string password)
        {
            _loader = new DataLoader(server, user, password);
        }

        public Task Add(Dto.Vendor vendor)
        {
            var trader = ToTrader(vendor);
            return _loader.InsertTrader(trader);
        }

        public Task Delete(string vendorId)
        {
            return _loader.DeleteTrader(vendorId);
        }

        public async Task<Dto.Vendor> GetVendor(string id)
        {
            var trader = await _loader.LoadTrader(id);
            return ToVendor(trader);
        }
        public async Task<IEnumerable<Dto.Vendor>> GetVendors()
        {
            var traders = await _loader.LoadTraders();
            var vendors = traders.Select(s => ToVendor(s));
            return vendors;
        }
        

        public Task Update(Dto.Vendor vendor)
        {
            return _loader.UpdateTrader(ToTrader(vendor));
        }

        private static Trader ToTrader(Dto.Vendor vendor)
        {
            return new Trader
            {
                Code = vendor.Id,
                Description = vendor.Description,
                Street = vendor.Address
            };
        }

        private static Dto.Vendor ToVendor(Trader supplier)
        {
            return new Dto.Vendor
            {
                Id = supplier.Code,
                Description = supplier.Street,
                Address = supplier.Description
            };
        }

       
    }
}
