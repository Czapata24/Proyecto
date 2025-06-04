using AutoMapper;
using HankoSpa.Attributes;
using HankoSpa.DTOs;
using HankoSpa.Nucleo;
using HankoSpa.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HankoSpa.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PermissionsController : ApiController
    {
        private readonly IPermissionService _permissionService;
        //private readonly IMapper _mapper;
        public PermissionsController(IPermissionService permissionService) //IMapper mapper
        {
            _permissionService = permissionService;
            //_mapper = mapper;
        }

        [HttpGet]
        [ApiAuthorize(permission: "showSection", module: "Secciones")]
        public async Task<IActionResult> GetPermissions()
        {
            var response = await _permissionService.GetAllAsync();
            if (!response.IsSuccess)
            {
                return StatusCode(StatusCodes.Status404NotFound, response.IsSuccess);
            }
            return ControllerBasicValidation(await _permissionService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        [ApiAuthorize(permission: "showSection", module: "Secciones")]
        public async Task<IActionResult> GetOnePermission([FromRoute] int id)
        {
            return ControllerBasicValidation(await _permissionService.GetByIdAsync(id));
        }


        [HttpPost]
        [ApiAuthorize(permission: "createSection", module: "Secciones")]
        public async Task<IActionResult> CreatePermissions([FromBody] PermissionDTO dto)
        {

            return ControllerBasicValidation(await _permissionService.CreateAsync(dto), ModelState);
        }

        [HttpPut]
        [ApiAuthorize(permission: "editSection", module: "Secciones")]
        public async Task<IActionResult> EditPermissions([FromBody] PermissionDTO dto)
        {
            return ControllerBasicValidation(await _permissionService.UpdateAsync(dto), ModelState);
        }

        [HttpDelete("{id:int}")]
        [ApiAuthorize(permission: "deleteSection", module: "Secciones")]
        public async Task<IActionResult> DeletePermission([FromRoute] int id)
        {
            return ControllerBasicValidation(await _permissionService.DeleteAsync(id));
        }
    }
}
