using System;
using System.Collections.Generic;
using System.Text;

namespace Financial.Models
{
    public class ContaPessoaFisica : Conta
    {
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
    }
}
