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
            var supplier = ToSupplier(vendor);  
            _loader.InsertSupplier(supplier);
            return Task.CompletedTask;
        }

        public Task Delete(string vendorId)
        {
            _loader.DeleteSupplier(vendorId);
            return Task.CompletedTask;
        }

        public Task<Dto.Vendor> GetVendor(string id)
        {
            var result = _loader.LoadSupplier(id);
            return Task.FromResult(ToVendor(result));
        }

        public Task<IEnumerable<Dto.Vendor>> GetVendors()
        {
            var suppliers = _loader.LoadSuppliers();
            var vendors = suppliers.Select(s => ToVendor(s));
            return Task.FromResult(vendors);
        }

        public Task Update(Dto.Vendor vendor)
        {
            _loader.UpdateSupplier(ToSupplier(vendor));
            return Task.CompletedTask;
        }

        private static Supplier ToSupplier(Dto.Vendor vendor)
        {
            return new Supplier
            {
                Id = vendor.Id,
                Name = vendor.Description,
                Address = vendor.Address
            };
        }

        private static Dto.Vendor ToVendor(Supplier supplier)
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
