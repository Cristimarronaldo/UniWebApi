using FluentValidation;
using FluentValidation.Results;
using System;
using Unimed.API.DomainObjects;

namespace Unimed.API.Models
{
    public class Cliente : Entity
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string NomeMae { get; private set; }
        public Guid PlanoId { get; private set; }
        public Endereco Endereco { get; private set; }

        public Plano Plano { get; private set; }
        public ClienteExame ClienteExame { get; private set; }

        public ValidationResult ValidationResult { get; set; }

        //EF
        protected Cliente() { }

        public Cliente(Guid id, string nome, string cpf, DateTime dataNascimento, string nomeMae, Guid planoId)
        {
            Id = id != Guid.Empty ? id : Id;
            Nome = nome;
            CPF = cpf;
            DataNascimento = dataNascimento;
            NomeMae = nomeMae;
            PlanoId = planoId;
        }

        public bool EhValido()
        {
            ValidationResult = new ClienteValidation().Validate(this);

            return ValidationResult.IsValid;
        }

        public class ClienteValidation : AbstractValidator<Cliente>
        {
            public ClienteValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Código do Cliente inválido");

                RuleFor(c => c.PlanoId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Plano de Saúde inválido");

                RuleFor(c => c.Nome)
                    .NotEmpty()
                    .WithMessage("Nome do cliente está vazio");

                RuleFor(c => c.CPF)
                    .NotEmpty()
                    .WithMessage("CPF está vazio");

                RuleFor(c => c.CPF)
                    .Length(11, 11)
                    .WithMessage("CPF deve ter 11 caracteres");
            }
        }
    }
}
