using Clima_Tempo.Models;
using Clima_Tempo.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Clima_Tempo.Extensions;

namespace Clima_Tempo.Controllers
{
    public class HomeController : Controller
    {
        readonly ContextoBd contexto = new ContextoBd();

        public ActionResult Index(int? id)
        {
            var hoje = DateTime.Now.Date;

            var maisQuentes = contexto.PrevisaoClima
                .Where(n => n.DataPrevisao == hoje)
                .OrderByDescending(n => n.TemperaturaMaxima)
                .ThenBy(n => n.Cidade.Nome)
                .ThenBy(n => n.Cidade.Estado.UF)
                .Take(3)
                .Select(n => new CidadeViewModel
                {
                    Nome = n.Cidade.Nome,
                    UF = n.Cidade.Estado.UF,
                    Temperatura = (int)n.TemperaturaMaxima.Value
                })
                .ToList();

            var maisFrias = contexto.PrevisaoClima
                .Where(n => n.DataPrevisao == hoje)
                .OrderBy(n => n.TemperaturaMaxima)
                .ThenBy(n => n.Cidade.Nome)
                .ThenBy(n => n.Cidade.Estado.UF)
                .Take(3)
                .Select(n => new CidadeViewModel
                {
                    Nome = n.Cidade.Nome,
                    UF = n.Cidade.Estado.UF,
                    Temperatura = (int)n.TemperaturaMaxima.Value
                })
                .ToList();

            var model = new IndexViewModel
            {
                MaisQuentes = maisQuentes.OrderByDescending(n => n.Temperatura).ThenBy(n => n.Nome).ThenBy(n => n.UF).ToList(),
                MaisFrias = maisFrias.OrderBy(n => n.Temperatura).ThenBy(n => n.Nome).ThenBy(n => n.UF).ToList()
            };

            if (id > 0)
            {
                var cidade = contexto.Cidade
                    .Include(n => n.Estado)
                    .FirstOrDefault(n => n.Id == id.Value);

                if (cidade == null)
                    return View(model);

                model.CidadeId = cidade.Id;
                model.Cidade = cidade.Nome;
                model.EstadoId = cidade.Estado.Id;
                model.Estado = cidade.Estado.Nome;

                model.Previsoes = contexto.PrevisaoClima
                    .Where(n => n.DataPrevisao >= hoje)
                    .Where(n => n.CidadeId == id.Value)
                    .OrderBy(n => n.DataPrevisao)
                    .Take(7)
                    .Select(n => new PrevisaoViewModel
                    {
                        Id = n.Id,
                        Data = n.DataPrevisao,
                        Clima = n.Clima,
                        TemperaturaMinima = (int)n.TemperaturaMinima,
                        TemperaturaMaxima = (int)n.TemperaturaMaxima
                    })
                    .ToList();
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult GetCidades(string searchTerm, int pageSize, int pageNum)
        {
            var query = contexto.Cidade.AsEnumerable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(n => n.Nome.ToUpper().Contains(searchTerm.ToUpper()));
            }

            var result = new
            {
                Total = query.Count(),
                Results = query.OrderBy(n => n.Nome)
                    .Skip((pageNum * pageSize) - 10)
                    .Take(pageSize)
                    .Select(n => new
                    {
                        n.Id,
                        Nome = n.Nome + " / " + n.Estado.UF
                    }).ToList()
            };

            var json = new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            return json;
        }

    }
}