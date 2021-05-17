using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clima_Tempo.ViewsModels
{
    public class IndexViewModel
    {
        public List<CidadeViewModel> MaisQuentes { get; set; }
        public List<CidadeViewModel> MaisFrias { get; set; }
        public List<PrevisaoViewModel> Previsoes { get; set; }

        public int CidadeId { get; set; }
        public string Cidade { get; set; }

        public int EstadoId { get; set; }
        public string Estado { get; set; }
    }
}