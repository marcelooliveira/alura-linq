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
                        group inf by inf.Faixa.Album into agrupado
                        let valorTotal = agrupado.Sum(a => a.PrecoUnitario * a.Quantidade)
                        orderby valorTotal descending
                        select new
                        {
                            Album = agrupado.Key.Titulo,
                            Valor = valorTotal,
                            NumeroVendas = agrupado.Count()
                        };

                foreach (var grupo in query)
                {
                    Console.WriteLine("{0}: R$ {1} ({2})", grupo.Album.PadRight(35), grupo.Valor, grupo.NumeroVendas);
                }
                Console.WriteLine();

            }
        }
    }
}
