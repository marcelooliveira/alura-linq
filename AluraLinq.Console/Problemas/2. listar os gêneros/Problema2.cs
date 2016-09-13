using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema2
{
    class Problema2 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            var generos = new List<Genero>
            {
                new Genero { Id = 1, Nome = "Rock" },
                new Genero { Id = 2, Nome = "Reggae" },
                new Genero { Id = 3, Nome = "Classica" }
            };

            var query = from g in generos
                        select g;

            foreach (var genero in query)
            {
                Console.WriteLine("{0} - {1}", genero.Id, genero.Nome);
            }
        }
    }

    class Genero
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
