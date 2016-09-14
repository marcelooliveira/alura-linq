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
            using (var contexto = GetContextoComLog())
            {
                var query = from alb in contexto.Albums
                            where alb.Artista.Nome == "Led Zeppelin"
                            select alb;

                //Agora queremos mudar a consulta para filtrar pelos álbuns que contenham "Graffiti"
                query = query.Where(alb => alb.Titulo.Contains("Graffiti"));

                foreach (var album in query)
                {
                    Console.WriteLine(album.Titulo);
                }
            }
        }
    }
}
