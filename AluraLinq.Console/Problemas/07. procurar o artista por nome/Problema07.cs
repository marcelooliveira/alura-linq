using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema7
{
    /// <summary>
    /// 07. procurar o artista por nome
    /// </summary>
    class Problema7 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = GetContextoComLog())
            {
                //Agora que temos o banco de dados da nossa loja virtual, precisamos implementar algumas
                //funcionalidades, como a busca por artista. Quando nosso usuário digitar o nome do artista,
                //iremos montar a consulta da seguinte maneira:

                var textoBusca = "Led";
                var query = from a in contexto.Artistas
                            where a.Nome.Contains(textoBusca)
                            select a;

                foreach (var artista in query)
                {
                    Console.WriteLine("{0}\t{1}", artista.ArtistaId, artista.Nome);
                }
            }
        }
    }
}
