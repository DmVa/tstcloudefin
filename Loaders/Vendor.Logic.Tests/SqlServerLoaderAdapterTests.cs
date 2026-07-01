using SqlServerLoader;

namespace Vendor.Logic.Tests;

[TestClass]
public class SqlServerLoaderAdapterTests
{
    [TestMethod]
    public void SqlServerLoaderAdapter_CheckToTrader()
    {
        var vendor = new Dto.Vendor { Id = "1", Description = "Desc1", Address = "Addr1" };
        var trader = SqlServerLoaderAdapter.ToTrader(vendor);
        Assert.AreEqual(trader.Code, vendor.Id);
        Assert.AreEqual(trader.Description, vendor.Description);
        Assert.AreEqual(trader.Street, vendor.Address);
    }

    [TestMethod]
    public void SqlServerLoaderAdapter_CheckToVendor()
    {
        var trader = new Trader { Code = "1", Description = "Desc1", Street = "Addr1" };
        var vendor = SqlServerLoaderAdapter.ToVendor(trader);
        Assert.AreEqual(vendor.Id, trader.Code);
        Assert.AreEqual(vendor.Description, trader.Description);
        Assert.AreEqual(vendor.Address, trader.Street);
    }
}
