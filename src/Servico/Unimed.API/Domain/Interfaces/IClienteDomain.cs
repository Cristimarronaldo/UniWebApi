using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unimed.API.Models;

namespace Unimed.API.Domain.Interfaces
{
    public interface IClienteDomain
    {
        void Adicionar(Cliente cliente);
        void Alterar(Cliente cliente);

        Task<IEnumerable<Cliente>> ObterTodos();
        Task<Cliente> ObterPorId(Guid id);

        void AdicionarEndereco(Endereco endereco);
        void AlterarEndereco(Endereco endereco);

        Task<Endereco> ObterEnderecoId(Guid id);
    }
}
