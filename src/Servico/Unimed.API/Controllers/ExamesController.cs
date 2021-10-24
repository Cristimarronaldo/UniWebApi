using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Unimed.API.Domain.Interfaces;
using Unimed.API.Models;
using Unimed.API.Repositorio.Data;
using Unimed.API.ViewModel;

namespace Unimed.API.Controllers
{

    public class ExamesController : MainController
    {
        private readonly IExameDomain _exameDomain;
        private readonly UnimedContext _context;

        public ExamesController(IExameDomain exameDomain, UnimedContext context)
        {
            _exameDomain = exameDomain;
            _context = context;
        }

        [HttpGet("exames")]
        public async Task<IActionResult> Index()
        {
            var resultado = await _exameDomain.ObterTodos();

            return CustomizacaoResponse(resultado);
        }

        [HttpGet("exames/id:Guid")]
        public async Task<IActionResult> Index(Guid id)
        {
            var resultado = await _exameDomain.ObterPorId(id);

            return CustomizacaoResponse(resultado);
        }

        [HttpPost("exames")]
        public async Task<IActionResult> AdicionarExame([FromBody] ExameDTO exame)
        {
            _exameDomain.Adicionar(AutoMapperManual(exame));
            await _context.SaveChangesAsync();
            return CustomizacaoResponse();
        }

        [HttpPut("exames/id:Guid")]
        public async Task<IActionResult> AlterarExame([FromQuery] Guid id, [FromBody] ExameDTO exame)
        {
            if (string.IsNullOrEmpty(id.ToString())) return NotFound();

            if (id != exame.Id) return NotFound();

            _exameDomain.Alterar(AutoMapperManual(exame));
            await _context.SaveChangesAsync();
            return CustomizacaoResponse();
        }

        private async Task PersistirDados()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0) AdicionarErroProcessamento("Não foi possível persistir os dados no banco");
        }

        private Exame AutoMapperManual(ExameDTO exameDTO)
        {
            return new Exame(exameDTO.Id, exameDTO.Descricao);
        }

        private ExameDTO AutoMapperManual(Exame exame)
        {
            return new ExameDTO { Id = exame.Id, Descricao = exame.Descricao };
        }
    }
}
