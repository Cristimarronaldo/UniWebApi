using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unimed.API.Models;

namespace Unimed.API.Domain.Interfaces
{
    public interface IClienteExameDomain
    {
        void Adicionar(ClienteExame clienteExame);
        void Alterar(ClienteExame clienteExame);

        Task<IEnumerable<ClienteExame>> ObterTodos();
        Task<ClienteExame> ObterPorId(Guid id);
    }
}
