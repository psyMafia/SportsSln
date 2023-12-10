using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;

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
            IEnumerable<Product>? result =
            (controller.Index() as ViewResult)?.ViewData.Model
            as IEnumerable<Product>;

            // Assert
            Product[] prodArray = result?.ToArray() ?? Array.Empty<Product>();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual("P1", prodArray[0].Name);
            Assert.AreEqual("P2", prodArray[1].Name);
        }
    }
}