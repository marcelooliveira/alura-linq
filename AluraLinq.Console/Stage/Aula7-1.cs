
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
                contexto.Database.Log = Console.WriteLine;

                var maiorVenda = contexto.NotasFiscais.Max(nf => nf.Total);
                var menorVenda = contexto.NotasFiscais.Min(nf => nf.Total);
                var vendaMedia = contexto.NotasFiscais.Average(nf => nf.Total);

                Console.WriteLine("A maior venda é de R$ {0}", maiorVenda);
                Console.WriteLine("A menor venda é de R$ {0}", menorVenda);
                Console.WriteLine("A venda média é de R$ {0}", vendaMedia);

                var vendas = (from nf in contexto.NotasFiscais
                             group nf by 1 into agrupado
                             select new
                             {
                                 maiorVenda = agrupado.Max(nf => nf.Total),
                                 menorVenda = agrupado.Min(nf => nf.Total),
                                 vendaMedia = agrupado.Average(nf => nf.Total)
                             }).Single();

                Console.WriteLine("A maior venda é de R$ {0}", vendas.maiorVenda);
                Console.WriteLine("A menor venda é de R$ {0}", vendas.menorVenda);
                Console.WriteLine("A venda média é de R$ {0}", vendas.vendaMedia);

            }

            Console.ReadKey();
        }
    }
}
