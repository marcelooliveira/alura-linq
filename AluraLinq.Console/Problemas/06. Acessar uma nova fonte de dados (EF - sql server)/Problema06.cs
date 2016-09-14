using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema6
{
    /// <summary>
    /// 06. Acessar uma nova fonte de dados (EF - sql server)
    /// </summary>
    public class Problema6 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            //Já vimos Linq to Objects e Linq to XML. Vamos ver como acessar dados do Sql Server através
            //do Entity Framework.

            //A solução contém um arquivo de banco de dados SQL Server (AluraTunes.mdf) e também um 
            //modelo Entity Framework para esse banco de dados (AluraTunes.mdf). Esse modelo é
            //acessível através do DBContext AluraTunesEntities.

            //A ideia é encaixamos nosso código dentro do bloco using do contexto do Entity Framework,
            //da seguinte forma:

            using (var contexto = new AluraTunesEntities())
            {
                //aqui vai a definição da query

                //aqui vai o código para exibir os dados
            }
            
            //Depois de alguns minutos, teríamos:

            using (var contexto = new AluraTunesEntities())
            {
                //Cada entidade do Entity Framework é um objeto da classe DBSet<T>, que por sua vez implementa 
                //a interface IEnumerable. Através do contexto, chegamos a uma sintaxe de consulta
                //bastante familiar:

                var query = from f in contexto.Faixas
                            join g in contexto.Generos
                                on f.GeneroId equals g.GeneroId
                            select new
                            {
                                FaixaId = f.FaixaId,
                                Nome = f.Nome,
                                Genero = g.Nome
                            };

                //Agora obtemos os valores da nossa query:
                foreach (var faixaGenero in query)
                {
                    Console.WriteLine("{0}\t{1}\t{2}",
                        faixaGenero.FaixaId,
                        faixaGenero.Nome,
                        faixaGenero.Genero);
                }
                Console.WriteLine();

                //P: Essa consulta retornou um número enorme de linhas na tela. Eu só precisava de 10 linhas. O que faço?
                //R: Nesse caso precisamos utilizar o método Take, que irá exibir somente os 10 primeiros elementos da consulta:

                query = query.Take(10);
                foreach (var faixaGenero in query)
                {
                    Console.WriteLine("{0}\t{1}\t{2}",
                        faixaGenero.FaixaId,
                        faixaGenero.Nome,
                        faixaGenero.Genero);
                }

                //P: Agora mostrou somente os 10 primeiros elementos, mas não é ineficiente o método Take trazer 
                // do banco de dados milhares de linhas e filtrar em memória?
                //R: Mas não foi isso que aconteceu. O método Take não executou a consulta. Ele não acessou dados. Ele apenas a redefiniu.
                //  Lembre-se de que criar uma definição de consulta é diferente de executar essa consulta. No final
                //  das contas, o Linq gera uma consulta SQL Server que diz ao banco de dados para trazer somente 10 elementos.

                //P: Mas o que estamos fazendo já não é um script de query SQL? Pensei que era pra isso que existia
                //  consulta em Linq to Entities.
                //R: Não. O que estamos fazendo é uma query em Linq, usando código C#, e não uma consulta SQL. 
                //  Essa query Linq é TRADUZIDA internamente para uma consulta SQL Server, e enviada para o banco de
                //  dados.

                //É meio frustrante não saber exatamente o que está sendo enviado para o banco de dados. Nâo seria bom
                //se pudéssemos VER a consulta Sql Server que está sendo emitida?

                //Felizmente, existe uma maneira simples de fazer isso. Basta adicionar a sequinte linha após
                //a declaração da variável contexto. Mostrará não só a consulta SQL gerada, como também
                //logs de erros e alertas do banco de dados:

                contexto.Database.Log = Console.WriteLine;
                
                //Agora varremos novamente nossa consulta e vemos o script SQL que é gerado no console
                foreach (var faixaGenero in query)
                {
                    Console.WriteLine("{0}\t{1}\t{2}",
                        faixaGenero.FaixaId,
                        faixaGenero.Nome,
                        faixaGenero.Genero);
                }

                //Apenas para finalizar a introdução a Linq to Entities, vamos vazer uma crítica à cláusula JOIN.
                //Apesar de atender às nossas necessidades, o uso do join pode dificultar a leitura, principalmente
                //se houver muitas entidades envolvidas em joins.

                //No nosso último exemplo, vimos como fazer o join entre Faixas e Generos. Isso não é necessário,
                //porque o objeto Faixa do nosso modelo já possui a propriedade Genero. Aliás, quando o modelo
                //Entity Framework é gerado, todos os relacionamentos "chave estrangeira" já são importados para
                //nosso modelo como propriedades de navegação. Então podemos navegar por vários entidades e por vários 
                //níveis através dessas propriedades, como se estivéssemos navegando pelas chaves estrangeiras do
                //banco de dados relacional.

                //O principal motivo para usarmos propriedades de navegação em vez de joins é a facilidade de leitura
                //e entendimento. Resumindo:
                //  - "Qualquer tolo pode escrever código que um computador possa entender.
                //      Bons programadores escrevem código que humanos podem entender." (Martin Fowler)

                var querySemJoin = 
                    from f in contexto.Faixas
                    select new
                    {
                        FaixaId = f.FaixaId,
                        Nome = f.Nome,
                        Genero = f.Genero.Nome //f.Genero: Propriedade de Navegação
                    };

                foreach (var faixaGenero in querySemJoin.Take(10))
                {
                    Console.WriteLine("{0}\t{1}\t{2}",
                        faixaGenero.FaixaId,
                        faixaGenero.Nome,
                        faixaGenero.Genero);
                }
            }
        }
    }
}
