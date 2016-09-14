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
            //Em vez de selecionarmos (select) colunas, estamos selecionando um objeto (mas poderia ser qualquer coisa) 
            //Select pode ser colocado no começo da query? Não. Por quê? Por que não, é assim mesmo, se acostume com isso.
            //Quantos elementos a consulta acima irá retornar? Nenhum. É só uma definição de consulta, e quando
            //  o programa passa por essa linha, apenas armazena consulta na variável query sem trazer os dados.

            //Agora sim vamos fazer o loop sobre o resultado

            //Veja que bonito:
            // - a consulta já está preparada, separada da exibição
            // - dentro do nosso loop não há nenhum filtro, apenas código para exibir dados.
            
            foreach (var genero in query)
            {
                Console.WriteLine("{0} - {1}", genero.Id, genero.Nome);
            }
        }
    }

    class Genero
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
