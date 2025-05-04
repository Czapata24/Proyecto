using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HankoSpa.DTOs;
using HankoSpa.Models;
using HankoSpa.Nucleo;
using HankoSpa.Repository;
using HankoSpa.Services.Interfaces;

namespace HankoSpa.Services
{
    public class CitasService : ICitaServices
    {
        private readonly ICitaRepository _repository;
        private readonly IMapper _mapper;

        public CitasService(ICitaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Obtener todas las citas
        public async Task<Response<List<CitaDTO>>> GetAllAsync()
        {
            try
            {
                var citas = await _repository.GetAllAsync();
                var dtoList = _mapper.Map<List<CitaDTO>>(citas);
                return new Response<List<CitaDTO>>(true, "Citas obtenidas correctamente.", dtoList);
            }
            catch (Exception ex)
            {
                return HandleException<List<CitaDTO>>(ex, "Error al obtener las citas.");
            }
        }

        // Obtener una cita por su ID
        public async Task<Response<CitaDTO>> GetOneAsync(int id)
        {
            try
            {
                var cita = await _repository.GetByIdAsync(id);
                if (cita == null)
                    return new Response<CitaDTO>(false, "Cita no encontrada.");

                var dto = _mapper.Map<CitaDTO>(cita);
                return new Response<CitaDTO>(true, "Cita obtenida correctamente.", dto);
            }
            catch (Exception ex)
            {
                return HandleException<CitaDTO>(ex, "Error al obtener la cita.");
            }
        }

        // Crear una nueva cita
        public async Task<Response<CitaDTO>> CreateAsync(CitaDTO dto)
        {
            try
            {
                var cita = _mapper.Map<Cita>(dto);
                await _repository.AddAsync(cita);

                var resultDto = _mapper.Map<CitaDTO>(cita);
                return new Response<CitaDTO>(true, "Cita creada correctamente.", resultDto);
            }
            catch (Exception ex)
            {
                return HandleException<CitaDTO>(ex, ex.Message); 

            }
        }

        // Editar una cita existente
        public async Task<Response<CitaDTO>> EditAsync(CitaDTO dto)
        {
            try
            {
                var cita = await _repository.GetByIdAsync(dto.CitaId);
                if (cita == null)
                    return new Response<CitaDTO>(false, "Cita no encontrada.");

                // Actualizar propiedades
                cita.FechaCita = dto.FechaCita;
                cita.HoraCita = dto.HoraCita;
                cita.EstadoCita = dto.EstadoCita;
                cita.UsuarioID = dto.UsuarioID;

                await _repository.UpdateAsync(cita);

                var resultDto = _mapper.Map<CitaDTO>(cita);
                return new Response<CitaDTO>(true, "Cita actualizada correctamente.", resultDto);
            }
            catch (Exception ex)
            {
                return HandleException<CitaDTO>(ex, "Error al editar la cita.");
            }
        }

        // Eliminar una cita por su ID
        public async Task<Response<object>> DeleteAsync(int id)
        {
            try
            {
                var cita = await _repository.GetByIdAsync(id);
                if (cita == null)
                    return new Response<object>(false, "Cita no encontrada.");

                await _repository.DeleteAsync(id);
                return new Response<object>(true, "Cita eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return HandleException<object>(ex, "Error al eliminar la cita.");
            }
        }

        // Manejo centralizado de excepciones
        private Response<T> HandleException<T>(Exception ex, string message)
        {
            return new Response<T>(false, message)
            {
                Errors = new List<string> { ex.Message }
            };
        }
    }
}
