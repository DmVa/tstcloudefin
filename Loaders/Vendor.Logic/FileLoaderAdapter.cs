using FileLoader;
using Vendor.Logic.Interfaces;

namespace Vendor.Logic
{
    internal class FileLoaderAdapter : ILoader
    {
        private readonly Loader _loader;
        public FileLoaderAdapter(string filePath)
        { 
            _loader = new Loader(filePath);
        }

        public Task Add(Dto.Vendor vendor)
        {
            try
            {
                var supplier = ToSupplier(vendor);
                _loader.InsertSupplier(supplier);
            }
            catch (ApiException ex)
            {
                throw new VendorApiException(ex, ex.Message);
            }   
            return Task.CompletedTask;
        }

        public Task Delete(string vendorId)
        {
            try
            {
                _loader.DeleteSupplier(vendorId);
                return Task.CompletedTask;
            }
            catch (ApiException ex)
            {
                throw new VendorApiException(ex, ex.Message);
            }
        }

        public Task<Dto.Vendor> GetVendor(string id)
        {
            try
            {
                var result = _loader.LoadSupplier(id);
                return Task.FromResult(ToVendor(result));
            }
            catch (ApiException ex)
            {
                throw new VendorApiException(ex, ex.Message);
            }
        }

        public Task<IEnumerable<Dto.Vendor>> GetVendors()
        {
            try
            {
                var suppliers = _loader.LoadSuppliers();
                var vendors = suppliers.Select(s => ToVendor(s));
                return Task.FromResult(vendors);
            }
            catch (ApiException ex)
            {
                throw new VendorApiException(ex, ex.Message);
            }
        }

        public Task Update(Dto.Vendor vendor)
        {
            try
            {
                _loader.UpdateSupplier(ToSupplier(vendor));
                return Task.CompletedTask;
            }
            catch (ApiException ex)
            {
                throw new VendorApiException(ex, ex.Message);
            }
        }

        internal static Supplier ToSupplier(Dto.Vendor vendor)
        {
            return new Supplier
            {
                Id = vendor.Id,
                Name = vendor.Description,
                Address = vendor.Address
            };
        }

        internal static Dto.Vendor ToVendor(Supplier supplier)
        {
            return new Dto.Vendor
            {
                Id = supplier.Id,
                Description = supplier.Name,
                Address = supplier.Address
            };
        }
    }
}
