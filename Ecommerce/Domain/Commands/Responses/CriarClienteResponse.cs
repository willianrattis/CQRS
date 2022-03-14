using System;

namespace Ecommerce.Domain.Commands.Responses
{
       public class CriarClienteResponse 
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}