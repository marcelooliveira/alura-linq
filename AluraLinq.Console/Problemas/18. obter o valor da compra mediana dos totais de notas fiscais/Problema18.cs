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

                //No último problema, vimos como obter máximo, mínimo e média das vendas. Porém,
                //agora precisamos calcular a mediana dos valores de nota fiscal.

                //Numa definição bem simples, a mediana é valor que divide um conjunto de valores 
                //ordenados em partes iguais. Se tivermos um conjunto ordenado [1,2,3,7,7], a mediana é o número 3,
                //pois é o valor que está no meio do conjunto. Se tivermos um conjunto ordenado [3,4,6,6], temos
                //dois elementos no centro (4 e 6), portanto a mediana é a média aritmética entre eles (5).

                //O problema é que a classe System.Linq.Enumerable contém funções como Average, Min e Max, mas não
                //contém nenhuma função para mediana.

                //Poderíamos implementar uma função comum chamada Mediana, que recebesse uma coleção de dados
                //e retorna o valor da mediana. Depois disso, usamos a função da seguinte forma:

                var vendaMediana = Mediana(contexto.NotasFiscais.Select(ag => ag.Total));
                Console.WriteLine("Venda Mediana: R$ {0}", vendaMediana);

                //Sem dúvida essa função resolveu o nosso problema, porém seria melhor se pudéssemos chamá-la
                //numa consulta Linq.
 
                //Dito isso, podemos adaptar um pouco a nossa função e implementar um novo
                //método de extensão chamado "Mediana", como extensão da interface IEnumerable<decimal>. Dessa forma,
                //poderemos utilizá-lo dentro da nossa sintaxe Linq:

                vendaMediana = contexto.NotasFiscais.Mediana(ag => ag.Total);
                Console.WriteLine("Venda Mediana: R$ {0}", vendaMediana);
            }
        }

        public static decimal Mediana(IQueryable<decimal> origem)
        {
            int contagem = origem.Count();
            var ordenado = origem.OrderBy(p => p);
            decimal mediana =
                    ordenado.Skip(contagem / 2).First() 
                +   ordenado.Skip((contagem - 1) / 2).First();

            mediana /= 2;
            return mediana;
        }
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

            decimal mediana =
                    ordenado.Skip(contagem / 2).First()
                + ordenado.Skip((contagem - 1) / 2).First();

            mediana /= 2;

            return mediana;
        }
    }
}
