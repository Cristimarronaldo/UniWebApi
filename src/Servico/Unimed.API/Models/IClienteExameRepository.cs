using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Unimed.API.Models
{
    public interface IClienteExameRepository
    {
        void Adicionar(ClienteExame clienteExame);
        void Alterar(ClienteExame clienteExame);       

        Task<IEnumerable<ClienteExame>> ObterTodos();
        Task<ClienteExame> ObterPorId(Guid id);
        
    }
}
