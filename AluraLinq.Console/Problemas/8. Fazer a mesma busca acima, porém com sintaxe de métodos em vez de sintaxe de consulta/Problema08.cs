using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema8
{
    class Problema8 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                var query =
                    contexto
                        .Artistas
                        .Where(a => a.Nome.StartsWith("Led"));
                //explicar o lambda

                //clicar na definição de Where e mostrar que pode ser aplicado em 
                //tudo quer for IEnumerable e IQueryable

                foreach (var artista in query)
                {
                    Console.WriteLine("{0}\t{1}", artista.ArtistaId, artista.Nome);
                }

                //explicar que existem casos em que a sintaxe de consulta é melhor 
                //que a de método, e vice-versa

            }
        }
    }
}
