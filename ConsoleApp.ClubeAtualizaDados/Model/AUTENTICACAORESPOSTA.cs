using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.ClubeAtualizaDados.Model
{
    public class AUTENTICACAORESPOSTA
    {
        public string message { get; set; }
        public string data { get; set; }
        public object redirect { get; set; }
        public bool success { get; set; }
    }
}
