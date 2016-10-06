using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema18
{
    /// <summary>
    /// 18. obter o valor da compra mediana dos totais de notas fiscais
    /// </summary>
    class Problema18 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = GetContextoComLog())
            {
                var vendaMediana = Mediana(contexto.NotasFiscais.Select(ag => ag.Total));
                Console.WriteLine("Venda Mediana: R$ {0}", vendaMediana);

                vendaMediana = contexto.NotasFiscais.Mediana(ag => ag.Total);
                Console.WriteLine("Venda Mediana: R$ {0}", vendaMediana);
            }

            var tiposSanguineos = new List<TipoSanguineo> 
            {
                new TipoSanguineo { Codigo = "A" },
                new TipoSanguineo { Codigo = "B" },
                new TipoSanguineo { Codigo = "AB" },
                new TipoSanguineo { Codigo = "O" },
            };

            var segundo = tiposSanguineos.Segundo(s => s.Codigo.Contains("B"));
            Console.WriteLine(segundo.Codigo);
        }

        public static decimal Mediana(IQueryable<decimal> origem)
        {
            int contagem = origem.Count();
            var ordenado = origem.OrderBy(p => p);

            var elementoCentral_1 = ordenado.Skip((contagem - 1) / 2).First();
            var elementoCentral_2 = ordenado.Skip(contagem / 2).First();

            decimal mediana = (elementoCentral_1 + elementoCentral_2) / 2;

            return mediana;
        }
    }

    class TipoSanguineo
    {
        public string Codigo { get; set; }
    }

    public static class Extensions
    {
        public static decimal Mediana<TSource>(this IQueryable<TSource> origem, Expression<Func<TSource, decimal>> selector)
        {
            int contagem = origem.Count();
            
            var funcSeletor = selector.Compile();
            var ordenado = origem
                .Select(selector)
                .OrderBy(x => x);

            var elementoCentral_1 = ordenado.Skip((contagem - 1) / 2).First();
            var elementoCentral_2 = ordenado.Skip(contagem / 2).First();

            decimal mediana = (elementoCentral_1 + elementoCentral_2) / 2;
            
            return mediana;
        }

        public static TSource Segundo<TSource>(this IEnumerable<TSource> origem, Expression<Func<TSource, decimal>> selector)
        {
            var q = origem as IQueryable<TSource>;

            int contagem = origem.Count();

            var funcSeletor = selector.Compile();

            var ordenado = q
                .Select(selector)
                .OrderBy(x => x);

            return ordenado.Skip(1).Take(1);
        }
    }
}
