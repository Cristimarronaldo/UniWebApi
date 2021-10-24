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
    public class ClientesController : MainController
    {
        private readonly IClienteDomain _clienteDomain;
        private readonly IPlanoDomain _planoDomain;
        private readonly UnimedContext _context;

        public ClientesController(IClienteDomain clienteDomain, IPlanoDomain planoDomain , UnimedContext context)
        {
            _context = context;
            _clienteDomain = clienteDomain;
            _planoDomain = planoDomain;
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> Index()
        {
            var resultado = await _clienteDomain.ObterTodos();

            return CustomizacaoResponse(resultado);
        }

        [HttpGet("clientes/id:Guid")]
        public async Task<IActionResult> Index(Guid id)
        {
            var resultado = await _clienteDomain.ObterPorId(id);

            return CustomizacaoResponse(resultado);
        }

        [HttpPost("clientes")]
        public async Task<IActionResult> AdicionarCliente([FromBody] ClienteDTO clienteDTO)
        {
            clienteDTO.Id = Guid.Empty;
            var cliente = AutoMapperManual(clienteDTO);
            
            if (!ValidarCliente(cliente)) return CustomizacaoResponse();

            _clienteDomain.Adicionar(cliente);
            await _context.SaveChangesAsync();
            return CustomizacaoResponse();
        }

        [HttpPut("clientes/id:Guid")]
        public async Task<IActionResult> AlterarCliente([FromQuery] Guid id, [FromBody] ClienteDTO clienteDTO)
        {
            if (string.IsNullOrEmpty(id.ToString())) return NotFound();

            if (id != clienteDTO.Id) return NotFound();

            var cliente = AutoMapperManual(clienteDTO);
            if (!ValidarCliente(cliente)) return CustomizacaoResponse();

            _clienteDomain.Alterar(cliente);
            await _context.SaveChangesAsync();
            return CustomizacaoResponse();
        }


        [HttpGet("clientesEndereco/id:Guid")]
        public async Task<IActionResult> Endereco(Guid id)
        {
            var resultado = await _clienteDomain.ObterEnderecoId(id);

            return CustomizacaoResponse(resultado);
        }

        [HttpPost("clientesEndereco")]
        public async Task<IActionResult> AdicionarEndereco([FromBody] EnderecoDTO enderecoDTO)
        {
            enderecoDTO.Id = Guid.Empty;
            var endereco = AutoMapperManual(enderecoDTO);
            if (!ValidarEndereco(endereco)) return CustomizacaoResponse();

            _clienteDomain.AdicionarEndereco(endereco);
            await _context.SaveChangesAsync();
            return CustomizacaoResponse();
        }

        [HttpPut("clientesEndereco/id:Guid")]
        public async Task<IActionResult> AlterarEndereco([FromQuery] Guid id, [FromBody] EnderecoDTO enderecoDTO)
        {
            if (string.IsNullOrEmpty(id.ToString())) return NotFound();

            if (id != enderecoDTO.Id) return NotFound();

            var endereco = AutoMapperManual(enderecoDTO);
            if (!ValidarEndereco(endereco)) return CustomizacaoResponse();

            _clienteDomain.AlterarEndereco(endereco);
            await _context.SaveChangesAsync();
            return CustomizacaoResponse();
        }

        private async Task PersistirDados()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0) AdicionarErroProcessamento("Não foi possível persistir os dados no banco");
        }

        private Cliente AutoMapperManual(ClienteDTO clienteDTO)
        {
            return new Cliente(clienteDTO.Id, clienteDTO.Nome, clienteDTO.CPF,
                               clienteDTO.DataNascimento, clienteDTO.NomeMae, clienteDTO.PlanoId);
        }

        private ClienteDTO AutoMapperManual(Cliente cliente)
        {
            return new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                DataNascimento = cliente.DataNascimento,
                NomeMae = cliente.NomeMae,
                PlanoId = cliente.PlanoId
            };
        }

        private Endereco AutoMapperManual(EnderecoDTO enderecoDTO)
        {
            return new Endereco(enderecoDTO.Id, enderecoDTO.Logradouro, 
                                enderecoDTO.Numero, enderecoDTO.Complemento, enderecoDTO.Bairro,
                                enderecoDTO.Cep, enderecoDTO.Cidade, enderecoDTO.Estado,
                                enderecoDTO.ClienteId);
        }

        private EnderecoDTO AutoMapperManual(Endereco endereco)
        {
            return new EnderecoDTO { Id = endereco.Id, Logradouro = endereco.Logradouro,
                                Numero = endereco.Numero, Complemento = endereco.Complemento, 
                                Bairro = endereco.Bairro,
                                Cep = endereco.Cep, Cidade = endereco.Cidade, Estado = endereco.Estado,
                                ClienteId = endereco.ClienteId};
        }

        private bool ValidarCliente(Cliente cliente)
        {            
            var plano = _planoDomain.ObterPorId(cliente.PlanoId).Result;
            if (plano == null) AdicionarErroProcessamento("Não existe esse plano");

            if (cliente.EhValido()) return true;

            cliente.ValidationResult.Errors.ToList().ForEach(e => AdicionarErroProcessamento(e.ErrorMessage));
            return false;
        }

        private bool ValidarEndereco(Endereco endereco)
        {
            if (endereco.EhValido()) return true;

            endereco.ValidationResult.Errors.ToList().ForEach(e => AdicionarErroProcessamento(e.ErrorMessage));
            return false;
        }
    }
}
