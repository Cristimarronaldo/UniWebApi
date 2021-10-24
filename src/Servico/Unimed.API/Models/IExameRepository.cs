using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Unimed.API.Models
{
    public interface IExameRepository
    {
        void Adicionar(Exame exame);
        void Alterar(Exame exame);
        Task<IEnumerable<Exame>> ObterTodos();
        Task<Exame> ObterPorId(Guid id);
    }
}
