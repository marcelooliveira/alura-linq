using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema15
{
    /// <summary>
    /// 15. calcular valor total de vendas de músicas do led zeppelin
    /// </summary>
    class Problema15 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = GetContextoComLog())
            {
                //Como a entidade ItemNotaFiscal não contém o valor total, precisamos
                //calcular multiplicando o preço unitário pela quantidade
                var query = from inf in contexto.ItemsNotaFiscal
                            where inf.Faixa.Album.Artista.Nome == "Led Zeppelin"
                            select inf.PrecoUnitario * inf.Quantidade;

                //Note como navegamos do item da nota fiscal até o nome do artista: ItemNotaFiscal > Faixa > Album > Artista

                var valorTotal = query.Sum();

                Console.WriteLine("Valor total de músicas vendidas do Led Zeppelin: R$ {0}.", valorTotal);
            }
        }
    }
}
