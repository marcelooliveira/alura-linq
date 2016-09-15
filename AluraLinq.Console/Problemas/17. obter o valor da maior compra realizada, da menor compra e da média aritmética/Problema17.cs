using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema17
{
    /// <summary>
    /// 17. obter o valor da maior compra realizada, da menor compra e da média aritmética
    /// </summary>
    class Problema17 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = GetContextoComLog())
            {
                //Para obter esses valores agregados, podemos simplesmente calcular e armazenar
                //cada um deles em variáveis separadas:

                var vendaMinima = contexto.NotasFiscais.Min(ag => ag.Total);
                var vendaMedia = contexto.NotasFiscais.Average(ag => ag.Total);
                var vendaMaxima = contexto.NotasFiscais.Max(ag => ag.Total);

                Console.WriteLine("Venda Mínima: R$ {0}", vendaMinima);
                Console.WriteLine("Venda Média: R$ {0}", vendaMedia);
                Console.WriteLine("Venda Máxima: R$ {0}", vendaMaxima);

                //Porém, existe um problema. Nossa solução está realizando 3 consultas diferentes ao
                //banco de dados. Se conseguirmos pegar os dados numa única chamada, vamos ter os seguintes benefícios:
                //  - execução mais rápida da nossa aplicação
                //  - menos processamento no servidor de banco de dados
                //  - menos tráfego de rede

                //Aplicando o que vimos anteriormente sobre agrupamento, podemos resolver o problema agrupando
                //por um valor constante (por exemplo, "1") e assim obtemos os valores agregados de uma só vez:

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

                //Note que essa consulta também poderia ter sido feita em sintaxe de método. Nesse caso teríamos

                var vendasSintaxeMetodo = contexto.NotasFiscais
                                            .GroupBy(nf => 1)
                                            .Select(grupo => new
                                            {
                                                VendaMinima = grupo.Min(ag => ag.Total),
                                                VendaMedia = grupo.Average(ag => ag.Total),
                                                VendaMaxima = grupo.Max(ag => ag.Total)
                                            })
                                            .Single();

                Console.WriteLine("Venda Mínima: R$ {0}", vendasSintaxeMetodo.VendaMinima);
                Console.WriteLine("Venda Média: R$ {0}", vendasSintaxeMetodo.VendaMedia);
                Console.WriteLine("Venda Máxima: R$ {0}", vendasSintaxeMetodo.VendaMaxima);

                Console.WriteLine();

            }
        }
    }
}
