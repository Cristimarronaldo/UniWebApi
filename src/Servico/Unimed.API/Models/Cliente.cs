using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        //EF
        protected Cliente() { }

        public Cliente(Guid id, string nome, string cpf, DateTime dataNascimento, string nomeMae, Guid planoId)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            DataNascimento = dataNascimento;
            NomeMae = nomeMae;
            PlanoId = planoId;
        }
    }
}
