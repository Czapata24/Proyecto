using AutoMapper;
using HankoSpa.DTOs;
using HankoSpa.Helpers;
using HankoSpa.Models;
using HankoSpa.Nucleo;
using HankoSpa.Repository;
using HankoSpa.Services.Interfaces;

namespace HankoSpa.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _repo;
        private readonly IMapper _mapper;

        public PermissionService(IPermissionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Response<List<PermissionDTO>>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return new Response<List<PermissionDTO>>(true, "OK", _mapper.Map<List<PermissionDTO>>(list));
        }

        public async Task<Response<PermissionDTO>> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                return ResponseHelper<PermissionDTO>.MakeResponseFail($"No existe registro con Id {id}");
            return new Response<PermissionDTO>(true, "OK", _mapper.Map<PermissionDTO>(entity));
        }

        public async Task<Response<PermissionDTO>> CreateAsync(PermissionDTO dto)
        {
            var entity = _mapper.Map<Permission>(dto);
            await _repo.AddAsync(entity);
            return new Response<PermissionDTO>(true, "Permiso creado correctamente", _mapper.Map<PermissionDTO>(entity));
        }

        public async Task<Response<PermissionDTO>> UpdateAsync(PermissionDTO dto)
        {
            if (dto.PermisoId == 0)
            {
                return ResponseHelper<PermissionDTO>.MakeResponseFail("El PermisoId es requerido");
            }
            var entity = await _repo.GetByIdAsync(dto.PermisoId);
            if (entity == null)
                return ResponseHelper<PermissionDTO>.MakeResponseFail("Permiso no encontrado");
            _mapper.Map(dto, entity);
            await _repo.UpdateAsync(entity);
            return ResponseHelper<PermissionDTO>.MakeResponseSuccess(_mapper.Map<PermissionDTO>(entity), "Permiso actualizado correctamente");
        }

        public async Task<Response<bool>> DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            return ResponseHelper<bool>.MakeResponseSuccess(true, "Permiso eliminado correctamente");
        }
    }
}

