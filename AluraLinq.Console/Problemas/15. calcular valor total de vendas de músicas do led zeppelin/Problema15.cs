using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema15
{
    class Problema15 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = GetContextoComLog())
            {
                var query = from f in contexto.Faixas
                            join inf in contexto.ItemsNotaFiscal on f.FaixaId equals inf.FaixaId //posso remover este join
                            where f.Album.Artista.Nome == "Led Zeppelin"
                            select inf.PrecoUnitario * inf.Quantidade;

                var valorTotal = query.Sum();

                Console.WriteLine("Total de músicas do Led Zeppelin vendidas: R$ {0}.", valorTotal);
            }
        }
    }
}
