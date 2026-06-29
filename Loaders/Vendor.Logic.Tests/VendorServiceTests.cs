using Microsoft.Extensions.Options;
using Moq;
using System.Runtime;
using Vendor.Logic.Interfaces;
using Vendor.Logic.Settings;

namespace Vendor.Logic.Tests
{
    [TestClass]
    public sealed class VendorServiceTests
    {
        private static  Mock<ILoader> _loaderMock;
        private static  Mock<ILoaderFactory> _loaderFactoryMock;
        
        [ClassInitialize]
        public static void TestsInit(TestContext context)
        {
            _loaderMock = new Mock<ILoader>();
            var vendors = new List<Dto.Vendor>
                {
                    new() { Id = "1", Description = "Desc1", Address = "Addr1" },
                    new() { Id = "2", Description = "Desc2", Address = "Addr2" },
                    new() { Id = "3", Description = "Desc3", Address = "Addr3" }
                };
            _loaderMock.Setup(l => l.GetVendors()).ReturnsAsync(vendors);
            _loaderFactoryMock = new Mock<ILoaderFactory>();
            _loaderFactoryMock.Setup(l => l.GetLoader()).Returns(_loaderMock.Object);
        }

        [TestMethod]
        public void VendorService_GetVendors_CheckPaging() 
        { 
            var service = new VendorService(_loaderFactoryMock.Object);
            var paged = service.GetVendors(0, 2).Result;

            Assert.IsNotNull(paged);
            Assert.AreEqual(2, paged.Count());

            var noData = service.GetVendors(4, 5).Result;
            Assert.AreEqual(0, noData.Count());
        }
    }
}
