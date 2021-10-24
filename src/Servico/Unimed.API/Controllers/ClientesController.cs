using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly UnimedContext _context;

        public ClientesController(IClienteDomain clienteDomain, UnimedContext context)
        {
            _context = context;
            _clienteDomain = clienteDomain;
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
        public async Task<IActionResult> AdicionarCliente([FromBody] ClienteDTO cliente)
        {
            _clienteDomain.Adicionar(AutoMapperManual(cliente));
            await _context.SaveChangesAsync();
            return CustomizacaoResponse();
        }

        [HttpPut("clientes/id:Guid")]
        public async Task<IActionResult> AlterarCliente([FromQuery] Guid id, [FromBody] ClienteDTO cliente)
        {
            if (string.IsNullOrEmpty(id.ToString())) return NotFound();

            if (id != cliente.Id) return NotFound();

            _clienteDomain.Alterar(AutoMapperManual(cliente));
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
        public async Task<IActionResult> AdicionarEndereco([FromBody] EnderecoDTO endereco)
        {
            _clienteDomain.AdicionarEndereco(AutoMapperManual(endereco));
            await _context.SaveChangesAsync();
            return CustomizacaoResponse();
        }

        [HttpPut("clientesEndereco/id:Guid")]
        public async Task<IActionResult> AdicionarPlano([FromQuery] Guid id, [FromBody] EnderecoDTO endereco)
        {
            if (string.IsNullOrEmpty(id.ToString())) return NotFound();

            if (id != endereco.Id) return NotFound();

            _clienteDomain.AlterarEndereco(AutoMapperManual(endereco));
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
    }
}
