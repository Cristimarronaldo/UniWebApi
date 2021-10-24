using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unimed.API.Domain.Interfaces;
using Unimed.API.Models;

namespace Unimed.API.Domain
{
    public class ClienteExameDomain : IClienteExameDomain
    {
        private readonly IClienteExameRepository _clienteExameRepository;

        public ClienteExameDomain(IClienteExameRepository clienteExameRepository)
        {
            _clienteExameRepository = clienteExameRepository;
        }

        public void Adicionar(ClienteExame clienteExame)
        {
            _clienteExameRepository.Adicionar(clienteExame);
        }

        public void Alterar(ClienteExame clienteExame)
        {
            _clienteExameRepository.Alterar(clienteExame);
        }

        public async Task<ClienteExame> ObterPorId(Guid id)
        {
            return await _clienteExameRepository.ObterPorId(id);
        }

        public async Task<IEnumerable<ClienteExame>> ObterTodos()
        {
            return await _clienteExameRepository.ObterTodos();
        }
    }
}
