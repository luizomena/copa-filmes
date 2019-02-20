using CopaFilmes.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CopaFilmes.Tests
{
    public class FilmeControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public FilmeControllerTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                                     .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task Testar_Geracao_Campeonato()
        {
            List<Filme> filmes = new List<Filme>();

            Filme filmeA = new Filme();
            filmeA.Titulo = "Os Incríveis 2";
            filmeA.Nota = (float)8.5;
            filmes.Add(filmeA);

            Filme filmeB = new Filme();
            filmeB.Titulo = "Jurassic World: Reino Ameaçado";
            filmeB.Nota = (float)6.7;
            filmes.Add(filmeB);

            Filme filmeC = new Filme();
            filmeC.Titulo = "Oito Mulheres e um Segredo";
            filmeC.Nota = (float)6.3;
            filmes.Add(filmeC);

            Filme filmeD = new Filme();
            filmeD.Titulo = "Hereditário";
            filmeD.Nota = (float)7.8;
            filmes.Add(filmeD);

            Filme filmeE = new Filme();
            filmeE.Titulo = "Vingadores: Guerra Infinita";
            filmeE.Nota = (float)8.8;
            filmes.Add(filmeE);

            Filme filmeF = new Filme();
            filmeF.Titulo = "Deadpool 2";
            filmeF.Nota = (float)8.1;
            filmes.Add(filmeF);

            Filme filmeG = new Filme();
            filmeG.Titulo = "Han Solo: Uma História Star Wars";
            filmeG.Nota = (float)7.2;
            filmes.Add(filmeG);

            Filme filmeH = new Filme();
            filmeH.Titulo = "Thor: Ragnarok";
            filmeH.Nota = (float)7.9;
            filmes.Add(filmeH);

            var response = await _client.GetAsync("/api/Filme/GerarCampeonato?json=" + JsonConvert.SerializeObject(filmes));

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            var resultado = JsonConvert.DeserializeObject<ResultadoCampeonato>(responseString);
            resultado.Campeao.Titulo.Should().Be("Vingadores: Guerra Infinita");
            resultado.Vicecampeao.Titulo.Should().Be("Os Incríveis 2");
        }
    }
}
