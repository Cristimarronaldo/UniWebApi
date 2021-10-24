using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unimed.API.Domain.Interfaces;
using Unimed.API.Models;

namespace Unimed.API.Domain
{
    public class ClienteDomain : IClienteDomain
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteDomain(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public void Adicionar(Cliente cliente)
        {
            _clienteRepository.Adicionar(cliente);
        }        

        public void Alterar(Cliente cliente)
        {
            _clienteRepository.Alterar(cliente);
        }        

        public async Task<Cliente> ObterPorId(Guid id)
        {
            return await _clienteRepository.ObterPorId(id);
        }

        public async Task<IEnumerable<Cliente>> ObterTodos()
        {
            return await _clienteRepository.ObterTodos();
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            _clienteRepository.AdicionarEndereco(endereco);
        }

        public void AlterarEndereco(Endereco endereco)
        {
            _clienteRepository.AlterarEndereco(endereco);
        }

        public async Task<Endereco> ObterEnderecoId(Guid id)
        {
            return await _clienteRepository.ObterEnderecoId(id);
        }
    }
}
