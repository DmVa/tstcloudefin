using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Vendor.Logic;
using Vendor.Logic.Interfaces;
using Xunit;

namespace Vendor.Logic.Tests
{
    public class VendorServiceTests
    {
        private readonly Mock<ILoader> _loaderMock;
        private readonly Mock<LoaderFactory> _loaderFactoryMock;
        private readonly VendorService _service;

        public VendorServiceTests()
        {
            _loaderMock = new Mock<ILoader>();
            _loaderFactoryMock = new Mock<LoaderFactory>(null);
            _loaderFactoryMock.Setup(f => f.GetLoader()).Returns(_loaderMock.Object);
            _service = new VendorService(_loaderFactoryMock.Object);
        }

        [Fact]
        public async Task GetVendors_ReturnsPagedVendors()
        {
            // Arrange
            var vendors = new List<Dto.Vendor>
            {
                new() { Id = "1", Description = "desc1", Address = "addr1" },
                new() { Id = "2", Description = "desc2", Address = "addr2" },
                new() { Id = "3", Description = "desc3", Address = "addr3" }
            };
            _loaderMock.Setup(l => l.GetVendors()).ReturnsAsync(vendors);

            // Act
            var result = await _service.GetVendors(1, 2);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("2", result.First().Id);
            Assert.Equal("3", result.Last().Id);
        }

        [Fact]
        public async Task GetVendor_ReturnsVendor()
        {
            // Arrange
            var vendor = new Dto.Vendor { Id = "1", Description = "desc", Address = "addr" };
            _loaderMock.Setup(l => l.GetVendor("1")).ReturnsAsync(vendor);

            // Act
            var result = await _service.GetVendor("1");

            // Assert
            Assert.Equal("1", result.Id);
        }

        [Fact]
        public async Task Add_CallsLoaderAdd()
        {
            // Arrange
            var vendor = new Dto.Vendor { Id = "1", Description = "desc", Address = "addr" };
            _loaderMock.Setup(l => l.Add(vendor)).Returns(Task.CompletedTask);

            // Act
            await _service.Add(vendor);

            // Assert
            _loaderMock.Verify(l => l.Add(vendor), Times.Once);
        }

        [Fact]
        public async Task Update_CallsLoaderUpdate()
        {
            // Arrange
            var vendor = new Dto.Vendor { Id = "1", Description = "desc", Address = "addr" };
            _loaderMock.Setup(l => l.Update(vendor)).Returns(Task.CompletedTask);

            // Act
            await _service.Update(vendor);

            // Assert
            _loaderMock.Verify(l => l.Update(vendor), Times.Once);
        }

        [Fact]
        public async Task Delete_CallsLoaderDelete()
        {
            // Arrange
            _loaderMock.Setup(l => l.Delete("1")).Returns(Task.CompletedTask);

            // Act
            await _service.Delete("1");

            // Assert
            _loaderMock.Verify(l => l.Delete("1"), Times.Once);
        }
    }
}
