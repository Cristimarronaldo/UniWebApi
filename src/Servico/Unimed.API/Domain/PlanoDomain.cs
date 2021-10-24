using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unimed.API.Domain.Interfaces;
using Unimed.API.Models;
using Unimed.API.Repositorio.Data;

namespace Unimed.API.Domain
{
    public class PlanoDomain : IPlanoDomain
    {
        private readonly IPlanoRepository _planoRepository;       

        public PlanoDomain(IPlanoRepository planoRepository)
        {
            _planoRepository = planoRepository;
        }

        public void Adicionar(Plano plano)
        {
            _planoRepository.Adicionar(plano);
        }

        public void Alterar(Plano plano)
        {
            _planoRepository.Alterar(plano);
        }

        public Task<Plano> ObterPorId(Guid id)
        {
            return _planoRepository.ObterPorId(id);
        }

        public async Task<IEnumerable<Plano>> ObterTodos()
        {
            return await _planoRepository.ObterTodos();
        }

    }
}
