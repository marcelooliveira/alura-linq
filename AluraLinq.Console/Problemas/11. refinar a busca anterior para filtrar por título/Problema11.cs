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
                contexto.Database.Log = Console.WriteLine;

                var query = from art in contexto.Artistas
                            where art.Nome == "Led Zeppelin"
                            select art;

                var artista = query.Single(); //explicar o que ocorre quando não encontra nenhum ou encontra vários
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
