using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clima_Tempo.ViewsModels
{
    public class PrevisaoViewModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Clima { get; set; }
        public int TemperaturaMinima { get; set; }
        public int TemperaturaMaxima { get; set; }
    }
}