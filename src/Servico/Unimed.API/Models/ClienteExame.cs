using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            Id = id;
            ClienteId = clienteId;
            ExameId = exameId;
            NomeMedico = nomeMedico;
            DataExame = dataExame;
        }
    }
}
