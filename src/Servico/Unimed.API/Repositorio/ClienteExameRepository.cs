using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unimed.API.Models;
using Unimed.API.Repositorio.Data;

namespace Unimed.API.Repositorio
{
    public class ClienteExameRepository : IClienteExameRepository
    {
        private readonly UnimedContext _context;

        public ClienteExameRepository(UnimedContext context)
        {
            _context = context;
        }
        public void Adicionar(ClienteExame clienteExame)
        {
            _context.ClienteExames.Add(clienteExame);
        }

        public void Alterar(ClienteExame clienteExame)
        {
            _context.ClienteExames.Update(clienteExame);
        }

        public Task<ClienteExame> ObterPorId(Guid id)
        {
            return _context.ClienteExames.Include(c => c.Cliente).Include(c => c.Exame).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<ClienteExame>> ObterTodos()
        {
            return await _context.ClienteExames.Include(c => c.Cliente).Include(c => c.Exame).AsNoTracking().ToListAsync();
        }
    }
}
