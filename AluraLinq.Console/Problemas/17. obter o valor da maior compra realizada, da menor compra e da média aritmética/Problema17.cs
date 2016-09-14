using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema17
{
    class Problema17 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = GetContextoComLog())
            {
                var vendas =
                    (from nf in contexto.NotasFiscais
                    group nf by 1 into agrupado
                    select new
                    {
                        VendaMinima = agrupado.Min(ag => ag.Total),
                        VendaMedia = agrupado.Average(ag => ag.Total),
                        VendaMaxima = agrupado.Max(ag => ag.Total)
                    }).Single();

                Console.WriteLine("Venda Mínima: R$ {0}", vendas.VendaMinima);
                Console.WriteLine("Venda Média: R$ {0}", vendas.VendaMedia);
                Console.WriteLine("Venda Máxima: R$ {0}", vendas.VendaMaxima);

                Console.WriteLine();

            }
        }
    }
}
