using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application.Contratos
{
    public interface IEventosService
    {
        Task<Evento> AddEventos(Evento model);
        Task<Evento> UpdateEventos(int id, Evento model);
        Task<bool> DeleteEventos(int eventoId);

        Task<Evento[]> GetAllEventosAsync (bool includePalastrantes = false);
        Task<Evento[]> GetAllEventosByTemaAsync (String tema, bool includePalastrantes = false);
        Task<Evento> GetEventoByIdAsync (int eventoId, bool includePalastrantes = false);

    }
}