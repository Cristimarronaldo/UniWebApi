using System;
using System.Text.Json.Serialization;

namespace Unimed.API.ViewModel
{
    public class ClienteExameDTO
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public DateTime DataExame { get; set; }
        
        public string NomeMedico { get; set; }
        public Guid ClienteId { get; set; }
        public Guid ExameId { get; set; }

    }
}
