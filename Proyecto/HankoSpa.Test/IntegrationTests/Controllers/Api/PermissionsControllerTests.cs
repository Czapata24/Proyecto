using HankoSpa.DTOs;
using HankoSpa.Nucleo;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HankoSpa.Test.IntegrationTests.Controllers.Api
{
    [TestClass]
    public class PermissionsControllerTests: BaseTests
    {
        [TestMethod]
        public async Task GetPermissions_ReturnsUnauthorized()
        {
            // Arrange
            string dbName = Guid.NewGuid().ToString();
            WebApplicationFactory<Program> factory = BuildWebApplicationFactory(dbName);
            HttpClient client = factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync("/api/Permissions");

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [TestMethod]
        public async Task GetPermissions_Returns200()
        {
            // Arrange
            string dbName = Guid.NewGuid().ToString();
            WebApplicationFactory<Program> factory = BuildWebApplicationFactory(dbName);
            HttpClient client = factory.CreateClient();

            string token = await GetJwtTokenAsync(client, "cristian@gmail.com", "123456");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Act
            HttpResponseMessage response = await client.GetAsync("/api/Permissions");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task<string> GetJwtTokenAsync(HttpClient client, string email, string password)
        {
            LoginDTO loginDto = new LoginDTO
            {
                Email = email,
                Password = password
            };
            StringContent content = new StringContent(System.Text.Json.JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("/api/ApiAccount/Login", content);
            response.EnsureSuccessStatusCode();
            var tokenResponse = await response.Content.ReadAsStringAsync();
            Response<UserTokenDTO> loginData = JsonConvert.DeserializeObject<Response<UserTokenDTO>>(tokenResponse);
            return loginData.Result.Token;
        }

        [TestMethod]
        public async Task GetPermissions_Returns403()
        {
            // Arrange
            string dbName = Guid.NewGuid().ToString();
            WebApplicationFactory<Program> factory = BuildWebApplicationFactory(dbName);
            HttpClient client = factory.CreateClient();

            string token = await GetJwtTokenAsync(client, "danielito@gmail.com", "123456");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Act
            HttpResponseMessage response = await client.GetAsync("/api/Permissions");

            // Assert
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
