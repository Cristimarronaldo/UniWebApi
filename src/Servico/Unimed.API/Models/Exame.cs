using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using Unimed.API.DomainObjects;

namespace Unimed.API.Models
{
    public class Exame : Entity
    {
        public string Descricao { get; private set; }
        public List<ClienteExame> ClienteExame { get; private set; }

        //EF
        public Exame() { }

        public Exame(Guid id, string descricao)
        {
            Id = id != Guid.Empty ? id : Id;
            Descricao = descricao;
        }
        public ValidationResult ValidationResult { get; set; }

        public bool EhValido()
        {
            ValidationResult = new ExameValidation().Validate(this);

            return ValidationResult.IsValid;
        }

        public class ExameValidation : AbstractValidator<Exame>
        {
            public ExameValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Código do Exame inválido");

                RuleFor(c => c.Descricao)
                     .NotEmpty()
                     .WithMessage("Descrição do exame não pode está vazio");

                
            }
        }
    }
}
