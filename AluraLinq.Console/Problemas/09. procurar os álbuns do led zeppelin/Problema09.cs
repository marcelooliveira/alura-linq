using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema9
{
    /// <summary>
    /// 09. procurar os álbuns do led zeppelin
    /// </summary>
    class Problema9 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                //Agora vamos fazer uma nova busca por texto,
                //mas desta vez trazendo os álbuns de um artista:

                var textoBusca = "Led Zeppelin";
                var query = from alb in contexto.Albums
                            where alb.Artista.Nome == textoBusca
                            select alb;

                foreach (var album in query)
                {
                    Console.WriteLine(album.Titulo);
                }
            }
        }
    }
}
