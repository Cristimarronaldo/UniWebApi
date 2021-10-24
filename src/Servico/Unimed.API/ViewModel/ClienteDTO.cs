using System;

namespace Unimed.API.ViewModel
{
    public class ClienteDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NomeMae { get; set; }
        public Guid PlanoId { get; set; }        

    }
}
