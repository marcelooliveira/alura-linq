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
                var query = from f in contexto.Faixas
                            where f.Album.Artista.Nome == "Led Zeppelin"
                            select f;

                //var quantidade = query.Count();

                //Console.WriteLine("Led Zeppelin tem {0} músicas no banco de dados.", quantidade);

                var quantidade = contexto.Faixas
                                .Count(f => f.Album.Artista.Nome == "Led Zeppelin");

                Console.WriteLine("Led Zeppelin tem {0} faixas de música.", quantidade);
            }

            Console.ReadKey();
        }
    }
}
