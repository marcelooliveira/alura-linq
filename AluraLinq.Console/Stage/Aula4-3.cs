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
                GetFaixas(contexto, "Led Zeppelin", "");

                Console.WriteLine();

                GetFaixas(contexto, "Led Zeppelin", "Graffiti");

                Console.ReadKey();
            }
        }

        private static void GetFaixas(AluraTunesEntities contexto, string buscaArtista, string buscaAlbum)
        {
            var query = from f in contexto.Faixas
                        where f.Album.Artista.Nome.Contains(buscaArtista)
                        select f;

            if (!string.IsNullOrEmpty(buscaAlbum))
            {
                query = query.Where(q => q.Album.Titulo.Contains(buscaAlbum));
            }

            foreach (var faixa in query)
            {
                Console.WriteLine("{0}\t{1}", faixa.Album.Titulo.PadRight(40), faixa.Nome);
            }
        }
    }
}
