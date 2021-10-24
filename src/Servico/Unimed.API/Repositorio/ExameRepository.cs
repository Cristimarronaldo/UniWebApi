using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unimed.API.Models;
using Unimed.API.Repositorio.Data;

namespace Unimed.API.Repositorio
{
    public class ExameRepository : IExameRepository
    {
        private readonly UnimedContext _context;

        public ExameRepository(UnimedContext context)
        {
            _context = context;
        }

        public void Adicionar(Exame exame)
        {
            _context.Exames.Add(exame);
        }

        public void Alterar(Exame exame)
        {
            _context.Exames.Update(exame);
        }

        public Task<Exame> ObterPorId(Guid id)
        {
            return _context.Exames.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Exame>> ObterTodos()
        {
            return await _context.Exames.AsNoTracking().ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
