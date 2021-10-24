using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unimed.API.Models;

namespace Unimed.API.Domain.Interfaces
{
    public interface IExameDomain
    {
        void Adicionar(Exame exame);
        void Alterar(Exame exame);
        Task<IEnumerable<Exame>> ObterTodos();
        Task<Exame> ObterPorId(Guid id);
    }
}
