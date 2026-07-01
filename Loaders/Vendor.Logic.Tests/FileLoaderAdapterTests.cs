using FileLoader;

namespace Vendor.Logic.Tests;

[TestClass]
public class FileLoaderAdapterTests
{
    [TestMethod]
    public void FleLoaderAdapter_CheckToSupplier()
    {
        var vendor = new Dto.Vendor { Id = "1", Description = "Desc1", Address = "Addr1" };
        var supplier = FileLoaderAdapter.ToSupplier(vendor);
        Assert.AreEqual(supplier.Id, vendor.Id);
        Assert.AreEqual(supplier.Name, vendor.Description);
        Assert.AreEqual(supplier.Address, vendor.Address);
    }

    [TestMethod]
    public void FleLoaderAdapter_CheckToVendor()
    {
        var supplier = new Supplier { Id = "1", Name = "Name1", Address = "Addr1" };
        var vendor = FileLoaderAdapter.ToVendor(supplier);
        Assert.AreEqual(vendor.Id, supplier.Id);
        Assert.AreEqual(vendor.Description, supplier.Name);
        Assert.AreEqual(vendor.Address, supplier.Address);
    }
}
