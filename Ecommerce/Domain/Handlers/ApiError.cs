﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Handlers
{
    public class ApiError
    {
        public string Mensagem { get; set; }
        public IEnumerable<string> MensagemLog { get; set; }
    }
}
