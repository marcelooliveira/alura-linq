using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema11
{
    /// <summary>
    /// 11. refinar a busca anterior para filtrar por título
    /// </summary>
    class Problema11 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = new AluraTunesEntities())// GetContextoComLog())
            {
                Console.BufferHeight = 1000;

                var query = from f in contexto.Faixas
                            where f.Album.Artista.Nome == "Metallica"
                            select f;

                //Agora queremos mudar a consulta para filtrar pelos álbuns que contenham "Graffiti"
                query = query.Where(f => f.Album.Titulo.Contains("Black Album"));

                foreach (var f in query)
                {
                    Console.WriteLine("{0}\t{1}", f.Album.Titulo, f.Nome);
                }
            }
        }
    }
}
