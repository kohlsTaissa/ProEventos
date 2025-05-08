using System;
using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Contratos
{
    public interface IEventosService
    {
        Task<EventoDto> AddEventos(EventoDto model);
        Task<EventoDto> UpdateEventos(int id, EventoDto model);
        Task<bool> DeleteEventos(int eventoId);

        Task<EventoDto[]> GetAllEventosAsync (bool includePalastrantes = false);
        Task<EventoDto[]> GetAllEventosByTemaAsync (String tema, bool includePalastrantes = false);
        Task<EventoDto> GetEventoByIdAsync (int eventoId, bool includePalastrantes = false);

    }
}