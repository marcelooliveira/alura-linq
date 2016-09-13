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

                foreach (var artista in query)
                {
                    Console.WriteLine("{0}\t{1}", artista.ArtistaId, artista.Nome);
                }
            }
        }
    }
}
