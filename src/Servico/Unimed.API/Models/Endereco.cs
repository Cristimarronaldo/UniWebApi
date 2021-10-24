using FluentValidation;
using FluentValidation.Results;
using System;
using Unimed.API.DomainObjects;

namespace Unimed.API.Models
{
    public class Endereco : Entity
    {
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cep { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public Guid ClienteId { get; private set; }

        // EF Relation
        public Cliente Cliente { get; protected set; }

        public Endereco(Guid id, string logradouro, string numero, string complemento, string bairro, 
                        string cep, string cidade, string estado, Guid clienteId)
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
            ClienteId = clienteId;
        }

        public ValidationResult ValidationResult { get; set; }

        public bool EhValido()
        {
            ValidationResult = new EnderecoValidation().Validate(this);

            return ValidationResult.IsValid;
        }

        public class EnderecoValidation : AbstractValidator<Endereco>
        {
            public EnderecoValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Código do Endereço inválido");

                RuleFor(c => c.Logradouro)
                     .NotEmpty()
                     .WithMessage("Logradouro não pode está vazio");

                RuleFor(c => c.Numero)
                    .NotEmpty()
                    .WithMessage("Número do Logradouro não pode está vazio");

                RuleFor(c => c.Cidade)
                    .NotEmpty()
                    .WithMessage("Cidade não pode está vazio");

                RuleFor(c => c.Estado)
                    .NotEmpty()
                    .WithMessage("Estado não pode está vazio");

                RuleFor(c => c.Cep)
                    .NotEmpty()
                    .WithMessage("CEP não pode está vazio");

            }
        }
    }
}
