using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
        private readonly IClienteDomain _clienteDomain;
        private readonly IExameDomain _exameDomain;

        public ClienteExamesController(IClienteExameDomain clienteExameDomain, 
                                       IClienteDomain clienteDomain,
                                       IExameDomain exameDomain,
                                       UnimedContext context)
        {
            _clienteExamesDomain = clienteExameDomain;
            _clienteDomain = clienteDomain;
            _exameDomain = exameDomain;
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
        public async Task<IActionResult> AdicionarClienteExames([FromBody] ClienteExameDTO clienteExameDTO)
        {
            clienteExameDTO.Id = Guid.Empty;
            clienteExameDTO.DataExame = DateTime.Now;
            var clienteExame = AutoMapperManual(clienteExameDTO);
            if (!ValidarClienteExame(clienteExame)) return CustomizacaoResponse();

            _clienteExamesDomain.Adicionar(clienteExame);
            await _context.SaveChangesAsync();
            return CustomizacaoResponse();
        }

        [HttpPut("clienteExames/id:Guid")]
        public async Task<IActionResult> AlterarClienteExames([FromQuery] Guid id, [FromBody] ClienteExameDTO clienteExameDTO)
        {
            if (string.IsNullOrEmpty(id.ToString())) return NotFound();

            if (id != clienteExameDTO.Id) return NotFound();

            var clienteExameBD = _clienteExamesDomain.ObterPorId(id).Result;
            clienteExameDTO.DataExame = clienteExameBD.DataExame;

            var clienteExame = AutoMapperManual(clienteExameDTO);
            if (!ValidarClienteExame(clienteExame)) return CustomizacaoResponse();

            _clienteExamesDomain.Alterar(clienteExame);
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

        private bool ValidarClienteExame(ClienteExame cliente)
        {                       
            var clienteDB = _clienteDomain.ObterPorId(cliente.ClienteId).Result;

            if (clienteDB == null) AdicionarErroProcessamento("Cliente não exisnte");
            
            var exameDB = _exameDomain.ObterPorId(cliente.ExameId).Result;
            if (exameDB == null) AdicionarErroProcessamento("Exame não existente");

            if (!OperacaoValida()) return false;

            if (cliente.EhValido()) return true;

            cliente.ValidationResult.Errors.ToList().ForEach(e => AdicionarErroProcessamento(e.ErrorMessage));
            return false;
        }

    }
}
