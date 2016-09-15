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
                //Essa consulta pode se tornar um pouco complexa.
                //Por isso, vamos começar com uma consulta simples, e modificá-la com pequenos
                //passos até chegar ao resultado esperado.

                //Podemos começar com uma consulta que toma os itens de notas fiscais

                var itensQuery =
                    from inf in contexto.ItemsNotaFiscal
                    select inf;

                // agora fazemos o filtro por nome do artista (utilizando propriedades de navegação para
                //chegar até o nome do artista: ItemNotaFiscal > Faixa > Album > Artista)

                var itensDeUmArtista =
                    from inf in contexto.ItemsNotaFiscal
                    where inf.Faixa.Album.Artista.Nome == "Led Zeppelin"
                    select inf;

                // o problema é que os itens de nota fiscal retornados pela consulta acima não possuem
                // o valor total do item. Para isso, precismos usar Projeção de Dados para um objeto com
                // uma propriedade que é o valor total calculado para cada item:

                var itensDeUmArtistaComProjecao =
                    from inf in contexto.ItemsNotaFiscal
                    where inf.Faixa.Album.Artista.Nome == "Led Zeppelin"
                    select new
                    {
                        Faixa = inf.Faixa.Nome,
                        Valor = inf.PrecoUnitario * inf.Quantidade
                    };

                // agora podemos trazer no select também o título do álbum, através de propriedade de navegação:
                var albumFaixaValor =
                    from inf in contexto.ItemsNotaFiscal
                    where inf.Faixa.Album.Artista.Nome == "Led Zeppelin"
                    select new
                    {
                        Album = inf.Faixa.Album.Titulo,
                        Faixa = inf.Faixa.Nome,
                        Valor = inf.PrecoUnitario * inf.Quantidade
                    };

                foreach (var item in albumFaixaValor)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", item.Album, item.Faixa, item.Valor);
                }
                Console.WriteLine();

                //Agora precisamos agrupar os valores por álbum. Essa consulta é um pouco mais complexa. 
                //Vamos precisar da cláusula GROUP BY, no formato:
                //              group [consulta] by [chave] into [variável]

                var query =
                        from inf in contexto.ItemsNotaFiscal
                        where inf.Faixa.Album.Artista.Nome == "Led Zeppelin"
                        group inf by inf.Faixa.Album into agrupado
                        select new
                        {
                            Album = agrupado.Key.Titulo,
                            Valor = agrupado.Sum(a => a.PrecoUnitario * a.Quantidade),
                            NumeroVendas = agrupado.Count()
                        };

                //P: Essa cláusula group by funciona como na consulta SQL? Ela parece um pouco diferente.
                //R: Sim, ela funciona como nas consultas SQL, porém com algumas diferenças:
                //  - Nas consultas SQL, agrupamos sempre por um valor (inteiro, string, data, etc.) enquanto
                //      no group by do Linq podemos agrupar também por um objeto inteiro (inf.Faixa.Album)
                //  - Quando agrupamos, "jogamos" o valor agrupado numa variável "range" da query (agrupado)
                //  - Essa variável "range" (agrupado) possui a chave do agrupamento e permite acessar funções agregadas (soma, máximo, média, quantidade, etc.) 

                foreach (var grupo in query)
                {
                    Console.WriteLine("{0}: R$ {1} ({2})", grupo.Album.PadRight(35), grupo.Valor, grupo.NumeroVendas);
                }
                Console.WriteLine();

                //Para melhorar essa consulta, podemos ordenar o resultado de modo a mostrar no topo os álbuns
                //mais vendidos. Para isso, usamos a cláusula order by com descending, ordenando o valor total do item.

                query =
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

                foreach (var grupo in query)
                {
                    Console.WriteLine("{0}: R$ {1} ({2})", grupo.Album.PadRight(35), grupo.Valor, grupo.NumeroVendas);
                }
                Console.WriteLine();

                //O resultado agora está satisfatório. Porém, note que temos uma repetição da expressão:
                //      agrupado.Sum(a => a.PrecoUnitario * a.Quantidade)
                //Para resolver isso, vamos utilizar uma variável interna para armazenar essa expressão
                //e utilizá-la sempre que necessário. Vamos chamar essa variável de valorTotal:

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
