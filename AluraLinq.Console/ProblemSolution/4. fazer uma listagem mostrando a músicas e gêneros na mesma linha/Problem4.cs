using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.ProblemSolution._4._fazer_uma_listagem_mostrando_a_músicas_e_gêneros_na_mesma_linha
{
    public class Problem4 : ProblemSolutionBase
    {
        public override void Solve(string[] args)
        {
            var generos = new List<Genero>
            {
                new Genero { Id = 1, Nome = "Rock" },
                new Genero { Id = 2, Nome = "Reggae" },
                new Genero { Id = 3, Nome = "Classica" }
            };

            var musicas = new List<Musica>
            {
                new Musica { Id = 1, Nome = "Sweet Child O'Mine", GeneroId = 1 },
                new Musica { Id = 2, Nome = "I Shot The Sheriff", GeneroId = 2 },
                new Musica { Id = 3, Nome = "Danúbio Azul", GeneroId = 3 }
            };

            var query = from m in musicas
                        join g in generos on m.GeneroId equals g.Id
                        select new
                        {
                            MusicaId = m.Id,
                            Musica = m.Nome,
                            Genero = g.Nome
                        };

            foreach (var musicaXgenero in query)
            {
                Console.WriteLine("{0}\t{1}\t{2}", 
                    musicaXgenero.MusicaId, 
                    musicaXgenero.Musica.PadRight(20), 
                    musicaXgenero.Genero);
            }
        }
    }

    class Genero
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    class Musica
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int GeneroId { get; set; }
    }
}
