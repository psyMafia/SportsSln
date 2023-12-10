using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Can_Use_Repository()
        {
            // Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
new Product {ProductID = 1, Name = "P1"},
new Product {ProductID = 2, Name = "P2"}
}).AsQueryable<Product>());
            HomeController controller = new HomeController(mock.Object);
            // Act
            ProductsListViewModel result =
            controller.Index()?.ViewData.Model as ProductsListViewModel ?? new();
            // Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual("P1", prodArray[0].Name);
            Assert.AreEqual("P2", prodArray[1].Name);
        }

        [TestMethod]
        public void Can_Paginate()
        {
            // Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
new Product {ProductID = 1, Name = "P1"},
new Product {ProductID = 2, Name = "P2"},
new Product {ProductID = 3, Name = "P3"},
new Product {ProductID = 4, Name = "P4"},
new Product {ProductID = 5, Name = "P5"}
}).AsQueryable<Product>());
            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;

            // Act
            ProductsListViewModel result =
controller.Index(2)?.ViewData.Model as ProductsListViewModel ?? new();
            // Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual("P4", prodArray[0].Name);
            Assert.AreEqual("P5", prodArray[1].Name);
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
new Product {ProductID = 1, Name = "P1"},
new Product {ProductID = 2, Name = "P2"},
new Product {ProductID = 3, Name = "P3"},
new Product {ProductID = 4, Name = "P4"},
new Product {ProductID = 5, Name = "P5"}
}).AsQueryable<Product>());
            // Arrange
            HomeController controller =
            new HomeController(mock.Object) { PageSize = 3 };
            // Act
            ProductsListViewModel result =
            controller.Index(2)?.ViewData.Model as ProductsListViewModel
            ?? new();
            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(2, pageInfo.CurrentPage);
            Assert.AreEqual(3, pageInfo.ItemsPerPage);
            Assert.AreEqual(5, pageInfo.TotalItems);
            Assert.AreEqual(2, pageInfo.TotalPages);
        }
    }
}