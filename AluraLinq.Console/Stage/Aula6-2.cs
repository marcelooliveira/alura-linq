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
                var query = from inf in contexto.ItemsNotaFiscal
                            where inf.Faixa.Album.Artista.Nome == "Led Zeppelin"
                            select new { totalDoItem = inf.Quantidade * inf.PrecoUnitario };

                //foreach (var inf in query)
                //{
                //    Console.WriteLine("{0}", inf.totalDoItem);
                //}

                var totalDoArtista = query.Sum(q => q.totalDoItem);

                Console.WriteLine("Total do artista: R$ {0}", totalDoArtista);

                Console.ReadKey();
            }
        }
    }
}
