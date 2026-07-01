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
            try
            {
                var trader = ToTrader(vendor);
                return _loader.InsertTrader(trader);
            }
            catch (Exception ex)
            {
                throw new VendorApiException(ex, ex.Message);
            }
        }

        public Task Delete(string vendorId)
        {
            try
            {
                return _loader.DeleteTrader(vendorId);
            }
            catch (Exception ex)
            {
                throw new VendorApiException(ex, ex.Message);
            }
        }

        public async Task<Dto.Vendor> GetVendor(string id)
        {
            try
            {
                var trader = await _loader.LoadTrader(id);
                return ToVendor(trader);
            }
            catch (Exception ex)
            {
                throw new VendorApiException(ex, ex.Message);
            }
        }
        public async Task<IEnumerable<Dto.Vendor>> GetVendors()
        {
            try
            {
                var traders = await _loader.LoadTraders();
                var vendors = traders.Select(s => ToVendor(s));
                return vendors;
            }
            catch (Exception ex)
            {
                throw new VendorApiException(ex, ex.Message);
            }
        }


        public Task Update(Dto.Vendor vendor)
        {
            try
            {
                return _loader.UpdateTrader(ToTrader(vendor));
            }
            catch (Exception ex)
            {
                throw new VendorApiException(ex, ex.Message);
            }
        }

        internal static Trader ToTrader(Dto.Vendor vendor)
        {
            return new Trader
            {
                Code = vendor.Id,
                Description = vendor.Description,
                Street = vendor.Address
            };
        }

        internal static Dto.Vendor ToVendor(Trader supplier)
        {
            return new Dto.Vendor
            {
                Id = supplier.Code,
                Description = supplier.Description,
                Address = supplier.Street
            };
        }

       
    }
}
