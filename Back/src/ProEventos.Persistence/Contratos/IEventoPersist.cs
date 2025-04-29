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
        Task<Evento[]> GetAllEventosByTemaAsync (String tema, bool includePalastrantes = false);
        Task<Evento[]> GetAllEventosAsync (bool includePalastrantes = false);
        Task<Evento> GetEventoByIdAsync (int eventoId, bool includePalastrantes =  false);
    }
}