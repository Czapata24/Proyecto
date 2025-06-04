using HankoSpa.Controllers.Api;
using HankoSpa.DTOs;
using HankoSpa.Nucleo;
using HankoSpa.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HankoSpa.Test.UnitTests.Controllers.Api
{
    [TestClass]
    public class PermissionsControllerTests: BaseTests
    {
        [TestMethod]
        public async Task GetSections_ReturnsStatus200()
        {
            //Arrange
            Response<List<PermissionDTO>> mockresponse = new()
            {
                IsSuccess = true,
                Result = new List<PermissionDTO>()
            };

            Mock<IPermissionService> permissionServiceMock = new Mock<IPermissionService>();

            permissionServiceMock.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(mockresponse));

            //Act
            PermissionsController controller = new PermissionsController(permissionServiceMock.Object);
            IActionResult actionResult = await controller.GetPermissions();

            //Assert
            ObjectResult result = actionResult as ObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [TestMethod]
        public async Task GetSections_ReturnsStatus404()
        {
            // Arrange  
            var mockResponse = new Response<List<PermissionDTO>>()
            {
                IsSuccess = false,
                Result = new List<PermissionDTO>()
            };

            Mock<IPermissionService> permissionServiceMock = new Mock<IPermissionService>();

            permissionServiceMock.Setup(static x => x.GetAllAsync())
                .Returns(Task.FromResult(mockResponse));

            // Act  
            PermissionsController controller = new PermissionsController(permissionServiceMock.Object);
            IActionResult actionResult = await controller.GetPermissions();

            // Assert  
            ObjectResult result = actionResult as ObjectResult;
            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }
    }
}
