using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Unimed.API.Models
{
    public interface IPlanoRepository
    {
        void Adicionar(Plano plano);
        void Alterar(Plano plano);
        Task<IEnumerable<Plano>> ObterTodos();
        Task<Plano> ObterPorId(Guid id);
    }
}
