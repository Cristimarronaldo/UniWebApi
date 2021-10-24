using FluentValidation;
using FluentValidation.Results;
using System;
using Unimed.API.DomainObjects;

namespace Unimed.API.Models
{
    public class ClienteExame : Entity
    {
        public DateTime DataExame { get; private set; }
        public string NomeMedico { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid ExameId { get; private set; }

        public Cliente Cliente { get; private set; }
        public Exame Exame { get; private set; }

        public ClienteExame() { }

        public ClienteExame(Guid id, Guid clienteId, Guid exameId, string nomeMedico, DateTime dataExame)
        {
            Id = id != Guid.Empty ? id : Id;
            ClienteId = clienteId;
            ExameId = exameId;
            NomeMedico = nomeMedico;
            DataExame = dataExame;
        }

        public ValidationResult ValidationResult { get; set; }

        public bool EhValido()
        {
            ValidationResult = new ClienteExameValidation().Validate(this);

            return ValidationResult.IsValid;
        }

        public class ClienteExameValidation : AbstractValidator<ClienteExame>
        {
            public ClienteExameValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Código do ClienteExame inválido");

                RuleFor(c => c.ClienteId)
                     .NotEmpty()
                     .WithMessage("Cliente não informado");

                RuleFor(c => c.ExameId)
                    .NotEmpty()
                    .WithMessage("Exame não informado");

                RuleFor(c => c.NomeMedico)
                    .NotEmpty()
                    .WithMessage("Nome do médico não informado");               

            }
        }
    }
}
