using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unimed.API.Models;
using Unimed.API.Repositorio.Data;

namespace Unimed.API.Repositorio
{
    public class PlanoRepository : IPlanoRepository
    {
        private readonly UnimedContext _context;

        public PlanoRepository(UnimedContext context)
        {
            _context = context;
        }

        public void Adicionar(Plano plano)
        {
            _context.Planos.Add(plano);
        }

        public void Alterar(Plano plano)
        {
            _context.Planos.Update(plano);
        }

        public Task<Plano> ObterPorId(Guid id)
        {
            return _context.Planos.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Plano>> ObterTodos()
        {
            return await _context.Planos.AsNoTracking().ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
