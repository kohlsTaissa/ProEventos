using System;
using System.Threading.Tasks;
using ProEventos.Application.Dtos;
using ProEventos.Persistence.Models;

namespace ProEventos.Application.Contratos
{
    public interface IEventosService
    {
        Task<EventoDto> AddEventos(int userId, EventoDto model);
        Task<EventoDto> UpdateEventos(int userId, int id, EventoDto model);
        Task<bool> DeleteEventos(int userId, int eventoId);

        Task<PageList<EventoDto>> GetAllEventosAsync (int userId, PageParams pageParams, bool includePalastrantes = false);
        Task<EventoDto> GetEventoByIdAsync (int userId, int eventoId, bool includePalastrantes = false);

    }
}