using System;
using Unimed.API.DomainObjects;

namespace Unimed.API.Models
{
    public class Exame : Entity
    {
        public string Descricao { get; private set; }
        public ClienteExame ClienteExame { get; private set; }

        //EF
        public Exame() { }

        public Exame(Guid id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }
    }
}
