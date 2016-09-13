using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.ProblemSolution._3._criar_uma_nova_coleção_simples_e_pequena__músicas_
{
    public class Problem3 : ProblemSolutionBase
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
