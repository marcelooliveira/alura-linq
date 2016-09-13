using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema11
{
    class Problema11 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                var query = from art in contexto.Artistas
                            where art.Nome == "Led Zeppelin"
                            select art;

                var artista = query.Single();
                var albums = artista.Albums;
                var filtrado = albums.Where(alb => alb.Titulo.Contains("Graffiti"));

                foreach (var album in filtrado)
                {
                    Console.WriteLine(album.Titulo);
                }
            }
        }
    }
}
