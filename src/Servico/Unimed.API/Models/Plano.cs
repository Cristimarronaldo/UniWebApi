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

        public Plano()
        {}

        public Plano(Guid id, string numeroPlano, string nomePlano)
        {
            Id = id;
            NumeroPlano = numeroPlano;
            NomePlano = nomePlano;
        }
    }
}
