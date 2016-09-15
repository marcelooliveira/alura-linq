using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema1
{
    /// <summary>
    /// 1. criar uma coleção simples e pequena
    /// </summary>
    class Problema1 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            //Antes de criarmos a lista, vamos criar uma classe Genero com informações 
            //básicas sobre os gêneros das músicas digitais que vendemos em nossa loja.

            //Agora implementamos uma pequena lista com apenas alguns dos gêneros que serão 
            //disponibilizados, armazenando os valores na variável generos. 

            var generos = new List<Genero>
            {
                new Genero { Id = 1, Nome = "Rock" },
                new Genero { Id = 2, Nome = "Reggae" },
                new Genero { Id = 3, Nome = "Rock Progressivo" },
                new Genero { Id = 4, Nome = "Jazz" },
                new Genero { Id = 5, Nome = "Punk Rock" },
                new Genero { Id = 6, Nome = "Classica" }
            };
        }
    }

    class Genero
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
