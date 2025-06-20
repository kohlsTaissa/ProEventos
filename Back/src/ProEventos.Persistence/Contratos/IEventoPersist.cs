using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;
using ProEventos.Persistence.Models;

namespace ProEventos.Persistence.Contratos
{
    public interface IEventoPersist
    {
        //Eventos
        Task<PageList<Evento>> GetAllEventosAsync (int userId, PageParams pageParams, bool includePalastrantes = false);
        Task<Evento> GetEventoByIdAsync (int userId, int eventoId, bool includePalastrantes =  false);
    }
}