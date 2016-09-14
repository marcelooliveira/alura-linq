using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema10
{
    /// <summary>
    /// 10. procurar os álbuns do led zeppelin, sem usar join
    /// </summary>
    class Problema10 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            //O exemplo anterior rodou perfeitamente, porém vamos vazer uma pequena crítica à cláusula JOIN.
            //Apesar de atender às nossas necessidades, o uso do join pode dificultar a leitura, principalmente
            //se houver muitas entidades envolvidas em joins.

            //No nosso último exemplo, vimos como fazer o join entre Albuns e Artistas. Isso não é necessário,
            //porque o objeto Album do nosso modelo já possui a propriedade Artista. Aliás, quando o modelo
            //Entity Framework é gerado, todos os relacionamentos "chave estrangeira" já são importados para
            //nosso modelo como propriedades de navegação. Então podemos navegar por vários entidades e por vários 
            //níveis através dessas propriedades, como se estivéssemos navegando pelas chaves estrangeiras do
            //banco de dados relacional.

            //O principal motivo para usarmos propriedades de navegação em vez de joins é a facilidade de leitura
            //e entendimento. Resumindo:
            //  - "Qualquer tolo pode escrever código que um computador possa entender.
            //      Bons programadores escrevem código que humanos podem entender." (Martin Fowler)

            //Obs.: É claro que, se não houver propriedades de navegação (como nos nossos exemplos de Linq to Objects e Linq to
            //XML), o join deverá ser usado.

            using (var contexto = GetContextoComLog())
            {
                var query = from alb in contexto.Albums
                            where alb.Artista.Nome == "Led Zeppelin"
                            select alb;
              
                foreach (var album in query)
                {
                    Console.WriteLine(album.Titulo);
                }
            }
        }
    }
}
