using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema2
{
    /// <summary>
    /// 2. listar os gêneros que contenham a palavra Rock
    /// </summary>
    class Problema2 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            //Voltando à nossa lista de gêneros...
            var generos = new List<Genero>
            {
                new Genero { Id = 1, Nome = "Rock" },
                new Genero { Id = 2, Nome = "Reggae" },
                new Genero { Id = 3, Nome = "Rock Progressivo" },
                new Genero { Id = 4, Nome = "Jazz" },
                new Genero { Id = 5, Nome = "Punk Rock" },
                new Genero { Id = 6, Nome = "Classica" }
            };

            // normalmente faríamos o filtro dessa lista utilizando um IF:

            foreach (var genero in generos)
            {
                if (genero.Nome.Contains("Rock"))
                {
                    Console.WriteLine("{0} - {1}", genero.Id, genero.Nome);
                }
            }

            Console.WriteLine();

            //O filtro que fizemos acima atende as nossas necessidades nesse caso, porém
            //essa abordagem tem alguns problemas:
            // - Se aumentar a complexidade do nosso problema, o código se torna menos legível (mais IFs / IF NOTs)
            // - O código da filtragem está muito acoplado/misturado com o código que faz o loop (foreach)

            //Linq
            //- Foi baseado na sintaxe de consultas SQL de bancos relacionais
            //- funciona sobre fontes de dados IEnumerable (List, Array, Collection, Sistema de Arquivos, XML, 
            // Entity Framework, etc)

            //generos é do tipo List, portanto é um IEnumerable e pode ser usado pelo Linq

            var query = from g in generos
                        where g.Nome.Contains("Rock")
                        select g;

            //Isso é um script do SQL Server...só que não. É apenas código C#, com cara de script SQL
            //Se você conseguiu entender facilmente essa query, é um bom sinal. O Linq surgiu para isso mesmo.
            //Em vez de consultarmos uma tabela, estamos consultando uma lista em memória.
            //Em vez de filtrarmos uma coluna de tabela, estamos filtrando uma propriedade do objeto
            //Em vez de selecionarmos (select)colunas, estamos selecionando um objeto(mas poderia ser qualquer coisa)
            //Select pode ser colocado no começo da query ? Não.Por quê ? Por que não, é assim mesmo, se acostume com isso.
            //Quantos elementos a consulta acima irá retornar ? Nenhum.É só uma definição de consulta, e quando
            //  o programa passa por essa linha, apenas armazena consulta na variável query sem trazer os dados.

            //Agora sim vamos fazer o loop sobre o resultado

            //Veja que bonito:
            // - a consulta já está preparada, separada da exibição
            // - dentro do nosso loop não há nenhum filtro, apenas código para exibir dados.

            foreach (var genero in query)
            {
                Console.WriteLine("{0} - {1}", genero.Id, genero.Nome);
            }
            Console.WriteLine();

            //o que vimos acima se chama "Sintaxe de Consulta". Agora faremos a mesma consulta, porém 
            //usaremos a "Sintaxe de Método"

            query = generos.Where(g => g.Nome.Contains("Rock"));

            //É a mesma consulta, porém usando método.
            //A variável query será idêntica nos dois casos

            //Atenção acima para o método "Where".
            // - vá para definição de Where (F12). Veja que ele faz parte de System.Linq.Enumerable
            // - Ele é um método de extensão que tem a assinatura:
            //          IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source
            //                                              , Func<TSource, bool> predicate)
            // onde:
            //  - source: é o objeto da classe que está sendo extendida (no caso, a lista generos)
            //  - predicate: é o delegate do predicado (no caso, o filtro "g => g.Nome.Contains("Rock")")
            //
            // Perg: que é g => g.Nome.Contains("Rock")???
            // Resp: é uma EXPRESSÃO LAMBDA
            //
            // P: o que é uma expressão lambda? O que é um lambda?
            // R: é uma Função Anônima (que não tem nome) que é passada como parâmetro. Estamos dizendo
            //      para o Where: "Pegue essa lista, filtre para mim, usando essa função, execute para cada genero 
            //      e me traga só os que retornarem TRUE."
            // Nossa função recebe um parâmetro g (objeto da classe Genero) e retorna um bool (se
            //  g.Nome contém ou não a palavra "Rock").

            foreach (var genero in query)
            {
                Console.WriteLine("{0} - {1}", genero.Id, genero.Nome);
            }

            //P: Se a definição de consulta "query" não faz nada sozinha, quando é que a consulta é de fato
            //  realizada?
            //R: Quando o laço foreach começa a acessar os elementos, na linha: "foreach (var genero in query)"

            //A modalidade de Linq que acabamos de ver se chama "Linq To Objects", porque é Linq atuando sobre
            //objetos em memória.
        }
    }

    class Genero
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
