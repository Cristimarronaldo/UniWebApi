using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Unimed.API.Domain.Interfaces;
using Unimed.API.Models;
using Unimed.API.Repositorio.Data;
using Unimed.API.ViewModel;

namespace Unimed.API.Controllers
{

    public class ClienteExamesController : MainController
    {
        private readonly IClienteExameDomain _clienteExamesDomain;
        private readonly UnimedContext _context;

        public ClienteExamesController(IClienteExameDomain clienteExameDomain, UnimedContext context)
        {
            _clienteExamesDomain = clienteExameDomain;
            _context = context;
        }

        [HttpGet("clienteExames")]
        public async Task<IActionResult> Index()
        {
            var resultado = await _clienteExamesDomain.ObterTodos();

            return CustomizacaoResponse(resultado);
        }

        [HttpGet("clienteExames/id:Guid")]
        public async Task<IActionResult> Index(Guid id)
        {
            var resultado = await _clienteExamesDomain.ObterPorId(id);

            return CustomizacaoResponse(resultado);
        }

        [HttpPost("clienteExames")]
        public async Task<IActionResult> AdicionarClienteExames([FromBody] ClienteExameDTO clienteExame)
        {
            clienteExame.DataExame = DateTime.Now;
            _clienteExamesDomain.Adicionar(AutoMapperManual(clienteExame));
            await _context.SaveChangesAsync();
            return CustomizacaoResponse();
        }

        [HttpPut("clienteExames/id:Guid")]
        public async Task<IActionResult> AlterarClienteExames([FromQuery] Guid id, [FromBody] ClienteExameDTO clienteExame)
        {
            if (string.IsNullOrEmpty(id.ToString())) return NotFound();

            if (id != clienteExame.Id) return NotFound();

            var clienteExameBD = _clienteExamesDomain.ObterPorId(id).Result;
            clienteExame.DataExame = clienteExameBD.DataExame;

            _clienteExamesDomain.Alterar(AutoMapperManual(clienteExame));
            await _context.SaveChangesAsync();
            return CustomizacaoResponse();
        }

        private async Task PersistirDados()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0) AdicionarErroProcessamento("Não foi possível persistir os dados no banco");
        }

        private ClienteExame AutoMapperManual(ClienteExameDTO clienteExameDTO)
        {
            return new ClienteExame(clienteExameDTO.Id, clienteExameDTO.ClienteId, clienteExameDTO.ExameId, 
                                    clienteExameDTO.NomeMedico, clienteExameDTO.DataExame);
        }

        private ClienteExameDTO AutoMapperManual(ClienteExame clienteExame)
        {
            return new ClienteExameDTO
            {
                Id = clienteExame.Id,
                NomeMedico = clienteExame.NomeMedico,
                ClienteId = clienteExame.ClienteId,
                ExameId = clienteExame.ExameId
            };
        }

    }
}
