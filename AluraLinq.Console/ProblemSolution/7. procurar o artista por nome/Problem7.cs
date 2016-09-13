using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.ProblemSolution._7._procurar_o_artista_por_nome
{
    class Problem7 : ProblemSolutionBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                var query = from a in contexto.Artistas
                            where a.Nome.StartsWith("Led")
                            select a;

                foreach (var artista in query)
                {
                    Console.WriteLine("{0}\t{1}", artista.ArtistaId, artista.Nome);
                }
            }
        }
    }
}
