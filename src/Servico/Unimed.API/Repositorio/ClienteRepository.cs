using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unimed.API.Models;
using Unimed.API.Repositorio.Data;

namespace Unimed.API.Repositorio
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly UnimedContext _context;

        public ClienteRepository(UnimedContext context)
        {
            _context = context;
        }        

        public void Adicionar(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
        }

        public void Alterar(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
        }
       
        public Task<Cliente> ObterPorId(Guid id)
        {
            return _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cliente>> ObterTodos()
        {
            return await _context.Clientes.AsNoTracking().ToListAsync();
        }

       
        public void AdicionarEndereco(Endereco endereco)
        {
            _context.Enderecos.Add(endereco);
        }

        public void AlterarEnderece(Endereco endereco)
        {
            _context.Enderecos.Update(endereco);
        }

        public void AlterarEndereco(Endereco endereco)
        {
            _context.Enderecos.Update(endereco);
        }

        public Task<Endereco> ObterEnderecoId(Guid id)
        {
            return _context.Enderecos.FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
