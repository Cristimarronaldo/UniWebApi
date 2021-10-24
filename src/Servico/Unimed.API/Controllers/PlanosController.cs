﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unimed.API.Domain.Interfaces;
using Unimed.API.Models;
using Unimed.API.Repositorio.Data;
using Unimed.API.ViewModel;

namespace Unimed.API.Controllers
{
    
    public class PlanosController : MainController
    {
        private readonly IPlanoDomain _planoDomain;
        private readonly UnimedContext _context;

       public PlanosController(IPlanoDomain planoDomain, UnimedContext context)
        {
            _planoDomain = planoDomain;
            _context = context;
        }

        [HttpGet("planos")]
        public async Task<IActionResult> Index()
        {
            var resultado = await _planoDomain.ObterTodos();

            return CustomizacaoResponse(resultado);
        }

        [HttpGet("planos/id:Guid")]
        public async Task<IActionResult> Index(Guid id)
        {
            var resultado = await _planoDomain.ObterPorId(id);

            return CustomizacaoResponse(resultado);
        }

        [HttpPost("planos")]
        public async Task<IActionResult> AdicionarPlano([FromBody] PlanoDTO plano)
        {
            _planoDomain.Adicionar(AutoMapperManual(plano));
            await _context.SaveChangesAsync();
            return CustomizacaoResponse();
        }

        [HttpPut("planos/id:Guid")]
        public async Task<IActionResult> AlterarPlano([FromQuery]Guid id, [FromBody] PlanoDTO plano)
        {
            if (string.IsNullOrEmpty(id.ToString())) return NotFound();

            if (id != plano.Id) return NotFound();

            _planoDomain.Alterar(AutoMapperManual(plano));
            await _context.SaveChangesAsync();
            return CustomizacaoResponse();
        }

        private async Task PersistirDados()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0) AdicionarErroProcessamento("Não foi possível persistir os dados no banco");
        }

        private Plano AutoMapperManual(PlanoDTO planoView)
        {
            return new Plano(planoView.Id, planoView.NumeroPlano, planoView.NomePlano);
        }

        private PlanoDTO AutoMapperManual(Plano plano)
        {
            return new PlanoDTO { Id = plano.Id, NumeroPlano = plano.NumeroPlano, NomePlano = plano.NomePlano };
        }
       
    }
}
