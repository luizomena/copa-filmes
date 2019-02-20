using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CopaFilmes.Models;

namespace CopaFilmes.Controllers
{
    [Route("api/[controller]")]
    public class FilmeController : Controller
    {

        [HttpGet("[action]")]
        public IActionResult GerarCampeonato(string json)
        {
            try
            {
                List<Filme> filmes = JsonConvert.DeserializeObject<List<Filme>>(json);

                List<Disputa> disputas = MontarDisputas(filmes);

                Disputa final = CalcularFinal(disputas);

                ResultadoCampeonato resultado = CalcularResultado(final);

                var result = new
                {
                    Campeao = resultado.Campeao,
                    Vicecampeao = resultado.Vicecampeao
                };

                return Ok(result);
            }
            catch (Exception e)
            {
                var result = new
                {
                    Campeao = new Filme(),
                    Vicecampeao = new Filme()
                };

                return Ok(result);
            }
        }

        private List<Disputa> MontarDisputas(List<Filme> filmes)
        {
            Filme[] filmesOrdenados = filmes.OrderBy(o => o.Titulo).ToArray();

            List<Disputa> disputas = new List<Disputa>();
            for (int i = 0; i < 4; i++)
            {
                Disputa disputa = new Disputa();

                disputa.FilmeA = filmesOrdenados[i];
                disputa.FilmeB = filmesOrdenados[7 - i];

                disputas.Add(disputa);
            }

            return disputas;
        }

        private Filme CalcularVencedor(Disputa disputa)
        {
            Filme vencedor = new Filme();

            if (disputa.FilmeA.Nota > disputa.FilmeB.Nota)
                vencedor = disputa.FilmeA;                
            else if (disputa.FilmeA.Nota < disputa.FilmeB.Nota)
                vencedor = disputa.FilmeB;
            else
                vencedor = string.Compare(disputa.FilmeA.Titulo, disputa.FilmeB.Titulo) < 0 ? disputa.FilmeA : disputa.FilmeB;

            return vencedor;
        }

        private Disputa CalcularFinal(List<Disputa> disputas)
        {
            while(disputas.Count > 1)
            {                
                var novasDisputas = new List<Disputa>();

                for(int i = 0; i < disputas.Count; i = i + 2)
                {
                    Filme filmeA = CalcularVencedor(disputas[i]);
                    Filme filmeB = CalcularVencedor(disputas[i + 1]);

                    Disputa disputa = new Disputa();
                    disputa.FilmeA = filmeA;
                    disputa.FilmeB = filmeB;

                    novasDisputas.Add(disputa);
                }

                disputas = novasDisputas;
            }

            if(disputas.Count == 1)
                return disputas[0];
            else
                return null;
        }

        private ResultadoCampeonato CalcularResultado(Disputa disputa)
        {
            ResultadoCampeonato resultado = new ResultadoCampeonato();

            if (disputa.FilmeA.Nota > disputa.FilmeB.Nota)
            {
                resultado.Campeao = disputa.FilmeA;
                resultado.Vicecampeao = disputa.FilmeB;
            }            
            else if (disputa.FilmeA.Nota < disputa.FilmeB.Nota)
            {
                resultado.Campeao = disputa.FilmeB;
                resultado.Vicecampeao = disputa.FilmeA;
            }   
            else
            {
                if(string.Compare(disputa.FilmeA.Titulo, disputa.FilmeB.Titulo) < 0)
                {
                    resultado.Campeao = disputa.FilmeA;
                    resultado.Vicecampeao = disputa.FilmeB;
                }
                else
                {
                    resultado.Campeao = disputa.FilmeB;
                    resultado.Vicecampeao = disputa.FilmeA;
                }
            }

            return resultado;
        }
    }
}
