using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unimed.API.Models
{
    public class ResponseResultado
    {
        public ResponseResultado()
        {
            Errors = new ResponseErroMessages();
        }

        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErroMessages Errors { get; set; }
    }

    public class ResponseErroMessages
    {
        public ResponseErroMessages()
        {
            Mensagens = new List<string>();
        }

        public List<string> Mensagens { get; set; }
    }
}
