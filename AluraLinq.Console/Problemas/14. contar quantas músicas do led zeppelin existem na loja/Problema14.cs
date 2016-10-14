using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema14
{
    /// <summary>
    /// 14. contar quantas músicas do led zeppelin existem na loja
    /// </summary>
    class Problema14 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = GetContextoComLog())
            {
                //Nosso cliente quer saber quantas faixas disponíveis existem
                //para sua banda favorita. Podemos implementar a consulta da seguinte forma:
                var query = from f in contexto.Faixas
                            where f.Album.Artista.Nome == "Led Zeppelin"
                            select f;

                //Note como na consulta acima nós usamos duas propriedades de navegação: 
                // - Faixa > Album
                // - Album > Artista
                //Isso representa uma economia de 2 joins na nossa consulta.

                var quantidade = query.Count();
                //Note como a contagem acima é bastante simples de ser implementada.

                Console.WriteLine("Led Zeppelin tem {0} faixas na loja.", quantidade);
                Console.WriteLine();



                //O método Count tem um outro overload que recebe um predicado. Ele pode
                //ser usado para especificar quais faixas serão contadas.
                quantidade = contexto.Faixas.Count(f => f.Album.Artista.Nome == "Led Zeppelin");

                //Veja como a consulta SQL gerada para essa consulta é idêntica à gerada pela primeira
                //consulta. É possível fazer a mesma consulta com Linq de várias maneiras!

                Console.WriteLine("Led Zeppelin tem {0} faixas na loja.", quantidade);
                Console.WriteLine();
            }
        }
    }
}
