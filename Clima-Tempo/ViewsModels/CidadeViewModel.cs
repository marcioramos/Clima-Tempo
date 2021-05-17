using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clima_Tempo.ViewsModels
{
    public class CidadeViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
        public int Temperatura { get; set; }
    }
}