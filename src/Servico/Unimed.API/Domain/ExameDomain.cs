using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unimed.API.Domain.Interfaces;
using Unimed.API.Models;

namespace Unimed.API.Domain
{
    public class ExameDomain : IExameDomain
    {
        private readonly IExameRepository _exameRepository;

        public ExameDomain(IExameRepository exameRepository)
        {
            _exameRepository = exameRepository;
        }

        public void Adicionar(Exame exame)
        {
            _exameRepository.Adicionar(exame);
        }

        public void Alterar(Exame exame)
        {
            _exameRepository.Alterar(exame);
        }

        public Task<Exame> ObterPorId(Guid id)
        {
            return _exameRepository.ObterPorId(id);
        }

        public Task<IEnumerable<Exame>> ObterTodos()
        {
            return _exameRepository.ObterTodos();
        }
    }
}
