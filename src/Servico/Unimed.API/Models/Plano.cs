using FluentValidation;
using FluentValidation.Results;
using System;
using Unimed.API.DomainObjects;

namespace Unimed.API.Models
{
    public class Plano : Entity
    {
        public string NumeroPlano { get; private set; }
        public string NomePlano { get; private set; }        

        //EF
        public Cliente Cliente { get; private set; }

        public ValidationResult ValidationResult { get; set; }

        public Plano()
        {}

        public Plano(Guid id, string numeroPlano, string nomePlano)
        {
            Id = id != Guid.Empty ? id : Id;
            NumeroPlano = numeroPlano;
            NomePlano = nomePlano;
        }

        public bool EhValido()
        {
            ValidationResult = new PlanoValidation().Validate(this);

            return ValidationResult.IsValid;
        }

        public class PlanoValidation : AbstractValidator<Plano>
        {
            public PlanoValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Código do Plano inválido");

               RuleFor(c => c.NumeroPlano)
                    .NotEmpty()
                    .WithMessage("Número do Plano não pode está vazio");

                RuleFor(c => c.NumeroPlano)
                    .Length(5, 20)
                    .WithMessage("Número da carteirinha deve ter entre 10 a 20 Caracteres");

                RuleFor(c => c.NomePlano)
                    .NotEmpty()
                    .WithMessage("Descrição do Plano não pode está vazio");

                RuleFor(c => c.NomePlano)
                    .Length(10, 200)
                    .WithMessage("Nome do Plano de está entre 10 a 200 caracteres");

            }
        }
    }
}
