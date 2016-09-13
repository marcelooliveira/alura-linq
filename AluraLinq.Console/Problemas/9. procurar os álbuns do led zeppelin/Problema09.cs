using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema9
{
    class Problema9 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                var query = from art in contexto.Artistas
                            join alb in contexto.Albums
                                on art.ArtistaId equals alb.ArtistaId
                            where art.Nome == "Led Zeppelin"
                            select alb;

                foreach (var album in query)
                {
                    Console.WriteLine(album.Titulo);
                }
            }
        }
    }
}
