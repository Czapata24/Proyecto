using HankoSpa.DTOs;
using HankoSpa.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HankoSpa.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAccountController : ApiController
    {
        private readonly IUserService _userService;
        public ApiAccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            return ControllerBasicValidation(await _userService.LoginApiAsync(dto));
        }
    }
}
