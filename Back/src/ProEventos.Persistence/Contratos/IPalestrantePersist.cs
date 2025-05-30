using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IPalestrantePersist
    {
        //Palestrantes
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync (String Nome, bool includeEventos);
        Task<Palestrante[]> GetAllPalestrantesAsync (bool includeEventos);
        Task<Palestrante> GetPalestranteByIdAsync (int PalestranteId, bool includeEventos);
    }
}