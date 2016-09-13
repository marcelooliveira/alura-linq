using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema16
{
    class Problema16 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                var query = 
                        from inf in contexto.ItemsNotaFiscal
                        where inf.Faixa.Album.Artista.Nome == "Led Zeppelin"
                        group inf by inf.Faixa.AlbumId into agrupado
                        select new
                        {
                            Nome = agrupado.Key,
                            Valor = agrupado.Sum(a => a.PrecoUnitario * a.Quantidade)
                        };

                var total = 0M;
                foreach (var grupo in query)
                {
                    total += grupo.Valor;
                    Console.WriteLine("{0}: {1}", grupo.Nome, grupo.Valor);
                }
                Console.WriteLine(total);
                Console.WriteLine();

            }
        }
    }
}
