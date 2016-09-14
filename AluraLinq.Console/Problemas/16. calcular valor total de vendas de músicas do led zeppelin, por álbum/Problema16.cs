using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema16
{
    /// <summary>
    /// 16. calcular valor total de vendas de músicas do led zeppelin, por álbum
    /// </summary>
    class Problema16 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = GetContextoComLog())
            {
                //Essa consulta é um pouco mais complexa. Vamos não só somar valores filtrados por nome 
                //de uma banda, vamos somar para cada álbum. Para isso, precisamos da cláusula GROUP BY,
                //no formato:
                //              group [consulta] by [chave] into [variável]

                var query = 
                        from inf in contexto.ItemsNotaFiscal
                        where inf.Faixa.Album.Artista.Nome == "Led Zeppelin"
                        group inf by inf.Faixa.Album into agrupado
                        orderby agrupado.Sum(a => a.PrecoUnitario * a.Quantidade) descending
                        select new
                        {
                            Album = agrupado.Key.Titulo,
                            Valor = agrupado.Sum(a => a.PrecoUnitario * a.Quantidade),
                            NumeroVendas = agrupado.Count()
                        };

                //A query acima toma o valor da consulta "inf", e a agrupa pela chave "inf.Faixa.Album", colocando
                //o resultado na variável "agrupado".

                    query =
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
