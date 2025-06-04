using AutoMapper;
using HankoSpa.Data;
using HankoSpa.DTOs;
using HankoSpa.Models;
using HankoSpa.Nucleo;
using HankoSpa.Repository;
using HankoSpa.Services;
using HankoSpa.Services.Interfaces;
namespace HankoSpa.Test.UnitTests.Services
{
    [TestClass]
    public class PermissionServiceTests : BaseTests
    {
        [TestMethod]
        public async Task GetAll_ReturnAllPermissions()
        {
            // Arrange
            string dbName = Guid.NewGuid().ToString();
            AppDbContext context = BuidContext(dbName);
            IMapper mapper = ConfigureAutoMapper();

            context.Permissions.AddRange(new List<Permission>
            {
                new Permission { NombrePermiso = "Permiso1", Module = "Descripcion1" },
                new Permission { NombrePermiso = "Permiso2", Module = "Descripcion2" }
            });

            await context.SaveChangesAsync();

            // Act
            AppDbContext context2 = BuidContext(dbName);
            IPermissionRepository permissionRepository = new PermissionRepository(context2);
            IPermissionService permissionService = new PermissionService(permissionRepository, mapper);

            Response<List<PermissionDTO>> response = await permissionService.GetAllAsync();

            // Assert
            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(2, response.Result.Count);
        }

        [TestMethod]
        public async Task GetId_ReturnByIdPermissions()
        {
            // Arrange
            string dbName = Guid.NewGuid().ToString();
            AppDbContext context = BuidContext(dbName);
            IMapper mapper = ConfigureAutoMapper();

            // Act
            AppDbContext context2 = BuidContext(dbName);
            IPermissionRepository permissionRepository = new PermissionRepository(context2);
            IPermissionService permissionService = new PermissionService(permissionRepository, mapper);

            Response<PermissionDTO> response = await permissionService.GetByIdAsync(3);

            // Assert
            Assert.IsFalse(response.IsSuccess);
        }
    }
}
