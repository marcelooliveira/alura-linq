using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema18
{
    class Problema18 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = GetContextoComLog())
            {
                var vendaMediana = contexto.NotasFiscais.Select(nf => nf.Total).Mediana();
                Console.WriteLine("Venda Mediana: R$ {0}", vendaMediana);
                
            }
        }
    }

    public static class Extensions
    {
        public static decimal Mediana(this IEnumerable<decimal> origem)
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
}
