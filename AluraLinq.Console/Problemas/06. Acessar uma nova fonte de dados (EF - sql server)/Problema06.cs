using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            //Agora vamos criar um banco de dados SQL Server vazio e colocá-lo dentro da nossa aplicação.
            //No mundo real é aconselhável manter esse banco de dados num servidor SQL Server separado e dedicado, mas
            //para as nossas aulas vamos colocar o banco dentro do projeto.

            //Com esse banco SQL criado, vamos rodar agora um script que vai criar todo o modelo (tabelas, colunas,
            //chaves primárias, chaves estrangeiras, etc). Criamos uma nova query e copiamos o script. Agora rodamos
            //esse script no banco de dados: (BATER PALMA E PEDIR PRA ACELERAR)

            //Agora que o script rodou com sucesso, podemos ver que os objetos foram criados como esperado

            //MOSTRAR TABELAS E COLUNAS QUE FORAM CRIADAS

            //Apenas para conferir as informações, vamos ver quais os gêneros o nosso banco de dados contém

            //FAZER UMA QUERY E BUSCAR DADOS DE GENEROS

            //Veja como o script populou os dados de gêneros musicais...

            //FAZER UMA QUERY E BUSCAR DADOS DE FAIXAS DE MÚSICAS

            //Veja como o script populou os dados de faixas de músicas...

            //Agora que o nosso banco de dados está pronto pra ser usado, vamos criar o nosso modelo de entidades
            //com o Entity Framework. 
            //  - Add > New Item > Data > EF 6.x DbContext Generator

            //PEDIR PRA ACELERAR

            //COMEÇAR UMA CLASSE VAZIA

            //É verdade que o Linq é muito poderoso, pelo que já vimos isso em Linq to Objects (Linq para Objetos) e o Linq to XML (Linq para XML).
            //Mas e quanto ao Entity Framework? Linq to Entities (ou Linq para Entidades) formam o casal perfeito. Não é possível utilizar
            //todo o potencial do Entity Framework sem conhecer o Linq to Entities.

            //Nosso cliente pediu para criarmos uma listagem com as faixas de música e os respectivos gêneros, como já
            //fizemos anteriormente com Linq to Objects e Linq to XML, mas dessa vez teremos que acessar o banco de dados através do EF.

            //Mas como utilizamos o Linq to Entities? Primeiro, precisamos instanciar o CONTEXTO do Entity Framework, como já sabemos:

            using (var contexto = new AluraTunesEntities())
            {
                //aqui vai a definição da query

                //aqui vai o código para exibir os dados
            }

            //Agora a ideia é encaixamos nosso código dentro do bloco using desse contexto, da seguinte forma:

            using (var contexto = new AluraTunesEntities())
            {
                //Cada entidade do Entity Framework é um objeto da classe DBSet<T>, que por sua vez implementa 
                //a interface IEnumerable. Toda classe IEnumerable pode ser usada como fonte de dados do Linq.

                //Vamos começar nossa consulta de forma bem simples, listando apenas os gêneros.

                var generoQuery = 
                            from g in contexto.Generos
                            select g;

                Console.WriteLine();
                foreach (var genero in generoQuery)
                {
                    Console.WriteLine("{0}\t{1}", genero.GeneroId, genero.Nome);
                }

                //Perceba que até agora tínhamos visto como usar o Linq para acessar objetos em memória
                //e dados de arquivos XML. Mas agora o resultado que estamos vendo é o retrato da tabela 
                //Generos do banco de dados, só que acessada através da entidade Generos do Entity Framework. Mas
                //e se quisermos trazer também as faixas de música na nossa consulta?

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
                foreach (var faixaEgenero in query)
                {
                    Console.WriteLine("{0}\t{1}\t{2}",
                        faixaEgenero.FaixaId,
                        faixaEgenero.Nome,
                        faixaEgenero.Genero);
                }
                Console.WriteLine();

                //PALMAS - ACELERAR ESSA PARTE POR FAVOR

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
                //  O que acontece é que criar uma definição de consulta é diferente de executar essa consulta. No final
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

                //Então é isso.Nesse vídeo aprendemos a criar uma consulta simples
                //trazendo apenas os gêneros e uma consulta um pouco mais complexa
                //combinando dados de faixas de músicas e gêneros musicais, e a limitar
                //o número de linhas do resultado.

                //Aprendemos que todo objeto que implementa interface IEnumerable pode ser usado
                //como fonte de dados numa consulta Linq.Também aprendemos a diferenciar uma definição
                //de consulta da execução da consulta
                //No final, aprendemos também a visualizar a consulta SQL gerada a partir da consulta Linq.

                //A seguir, vamos aprender como consultar um artista pelo nome.
                //Espero que vocês tenham gostado do vídeo! Obrigado e até à próxima!
            }
        }
    }
}
