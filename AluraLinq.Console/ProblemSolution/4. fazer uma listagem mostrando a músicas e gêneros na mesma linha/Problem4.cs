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
            var musicas = new List<Musica>
            {
                new Musica { Id = 1, Nome = "Sweet Child O'Mine", GeneroId = 1 },
                new Musica { Id = 2, Nome = "I Shot The Sheriff", GeneroId = 2 },
                new Musica { Id = 3, Nome = "Danúbio Azul", GeneroId = 3 }
            };
        }
    }

    class Musica
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int GeneroId { get; set; }
    }
}
