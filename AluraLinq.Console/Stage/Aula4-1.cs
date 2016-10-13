using AluraTunes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraTunes
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                var textoBusca = "Led";

                var query = from a in contexto.Artistas
                            join alb in contexto.Albums
                                on a.ArtistaId equals alb.ArtistaId
                            where a.Nome.Contains(textoBusca)
                            select new
                            {
                                NomeArtista = a.Nome,
                                NomeAlbum = alb.Titulo
                            };
                foreach (var item in query)
                {
                    Console.WriteLine("{0}\t{1}", item.NomeArtista, item.NomeAlbum);
                }

                Console.ReadKey();
            }
        }
    }
}
