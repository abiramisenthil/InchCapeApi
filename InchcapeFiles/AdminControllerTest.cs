using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.Protected;
using WebApiUsingEF.Controllers;
using WebApiUsingEF.Data;
using WebApiUsingEF.Model;

namespace WebApiUsingEF.UnitTests.ControllersTest
{
    public class AdminControllerTest
    {
        public AdminControllerTest()
        {
            stub = new List<Make>()
            {
                new Make()
                {
                    MakeId = 1,
                    MakeName = "Audi",
                },
                new Make()
                {
                    MakeId = 2,
                    MakeName = "BMW",
                }
            };
            var data = stub.AsQueryable();

            var mockdbContext = new Mock<AppDbContext>();
            var mockSet = new Mock<DbSet<Make>>();
            mockSet.As<IQueryable<Make>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Make>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Make>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Make>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            mockdbContext.Setup(x => x.Makes).Returns(mockSet.Object);

            mockdbContext.Setup(x => x.SaveChanges()).Returns(1);

            _controller = new AdminController(mockdbContext.Object);

        }

        List<Make> stub;
        AdminController _controller;

        [Fact]
        public async void AddMake_Returns200Status()
        {
            // Arrange
            var data = new Make();
            data.MakeName = "Audi";

            var mockModule = new Mock<AdminController>() { CallBase = true };
            mockModule.Protected().Setup<Task<Make>> ("InsertOrUpdateVehicleMake", ItExpr.IsAny<int>(), ItExpr.IsAny<string>(), ItExpr.IsAny<string>()).Returns(Task.FromResult<Make>(data));

            // Act
            var result = await _controller.AddMake(data);
            //var result = await mockModule.Object.AddMake(data);

            // Asset
            result.GetType().Should().Be(typeof(OkObjectResult));
        }
    }
}
