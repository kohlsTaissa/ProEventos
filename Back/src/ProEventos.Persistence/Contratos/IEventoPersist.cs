using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IEventoPersist
    {
        //Eventos
        Task<Evento[]> GetAllEventosByTemaAsync (int userId, String tema, bool includePalastrantes = false);
        Task<Evento[]> GetAllEventosAsync (int userId, bool includePalastrantes = false);
        Task<Evento> GetEventoByIdAsync (int userId, int eventoId, bool includePalastrantes =  false);
    }
}