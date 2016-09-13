using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema10
{
    class Problema10: ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                var query = from art in contexto.Artistas
                            where art.Nome == "Led Zeppelin"
                            select art;

                var artista = query.Single();
                
                foreach (var album in artista.Albums)
                {
                    Console.WriteLine(album.Titulo);
                }
            }
        }
    }
}
