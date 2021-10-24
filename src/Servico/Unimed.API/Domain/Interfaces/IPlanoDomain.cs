using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unimed.API.Models;

namespace Unimed.API.Domain.Interfaces
{
    public interface IPlanoDomain
    {
        void Adicionar(Plano plano);
        void Alterar(Plano plano);
        Task<IEnumerable<Plano>> ObterTodos();
        Task<Plano> ObterPorId(Guid id);
    }
}
