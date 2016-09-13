using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.ProblemSolution._8._Fazer_a_mesma_busca_acima__porém_com_sintaxe_de_métodos_em_vez_de_sintaxe_de_consulta
{
    class Problem8 : ProblemSolutionBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                var query =
                    contexto
                        .Artistas
                        .Where(a => a.Nome.StartsWith("Led"));

                foreach (var artista in query)
                {
                    Console.WriteLine("{0}\t{1}", artista.ArtistaId, artista.Nome);
                }
            }
        }
    }
}
