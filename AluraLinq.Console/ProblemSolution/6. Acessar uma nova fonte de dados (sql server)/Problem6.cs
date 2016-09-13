using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.ProblemSolution._6._Acessar_uma_nova_fonte_de_dados__sql_server_
{
    public class Problem6 : ProblemSolutionBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                var query = from f in contexto.Faixas
                            join g in contexto.Generos
                                on f.GeneroId equals g.GeneroId
                            select new
                            {
                                FaixaId = f.FaixaId,
                                Nome = f.Nome,
                                Genero = g.Nome
                            };

                foreach (var faixaGenero in query)
                {
                    Console.WriteLine("{0}\t{1}\t{2}",
                        faixaGenero.FaixaId,
                        faixaGenero.Nome,
                        faixaGenero.Genero);
                }
            }
        }
    }
}
