using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

                //Mas isso cria um novo problema: cada método do Linq (Min, Max, Average) é traduzido pelo
                //Linq to Entities para uma função nativa do Sql Server. Como não existe uma função Sql Server
                //(e nem Entity Framework) para "Mediana", precisamos trazer todos os dados necessários do banco
                //de dados e realizar o cálculo em memória.

                //Dito isso, podemos adaptar um pouco a nossa função e implementar um novo
                //método de extensão chamado "Mediana", como extensão da interface IEnumerable<decimal>. Dessa forma,
                //poderemos utilizá-lo dentro da nossa sintaxe Linq:

                vendaMediana = contexto.NotasFiscais.AsEnumerable().Mediana(ag => ag.Total);
                Console.WriteLine("Venda Mediana: R$ {0}", vendaMediana);

                //Note que, antes de usarmos o método Mediana, primeiro invocamos o método AsEnumerable(), que
                //traz os totais de notas do banco de dados para a memória. E então a mediana é calculada em memória.
            }
        }

        public static decimal Mediana(IQueryable<decimal> origem)
        {
            // Cria uma cópia da origem, e ordena essa cópia
            decimal[] temp = origem.ToArray();
            Array.Sort(temp);

            int count = temp.Length;
            if (count == 0)
            {
                throw new InvalidOperationException("Coleção está vazia");
            }
            else if (count % 2 == 0)
            {
                // contagem é par, então pegamos a média dos 2 elementos do meio
                decimal a = temp[count / 2 - 1];
                decimal b = temp[count / 2];
                return (a + b) / 2m;
            }
            else
            {
                // contagem é ímpar, retorna o elemento do meio
                return temp[count / 2];
            }
        }
    }

    public static class Extensions
    {
        public static decimal Mediana<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, decimal>> selector)
        {
            var func = selector.Compile();
            //var func = selector.Reduce();
            var origem = source.Select(s => func(s));

            // Cria uma cópia da origem, e ordena essa cópia
            decimal[] temp = origem.ToArray();
            Array.Sort(temp);

            int count = temp.Length;
            if (count == 0)
            {
                throw new InvalidOperationException("Coleção está vazia");
            }
            else if (count % 2 == 0)
            {
                // contagem é par, então pegamos a média dos 2 elementos do meio
                decimal a = temp[count / 2 - 1];
                decimal b = temp[count / 2];
                return (a + b) / 2m;
            }
            else
            {
                // contagem é ímpar, retorna o elemento do meio
                return temp[count / 2];
            }
        }

    }
}
