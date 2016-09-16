using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema4
{
    /// <summary>
    /// 04. fazer uma listagem mostrando a músicas e gêneros na mesma linha
    /// </summary>
    public class Problema4 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            //Estamos colocando as 2 listas no mesmo código agora
            var generos = new List<Genero>
            {
                new Genero { Id = 1, Nome = "Rock" },
                new Genero { Id = 2, Nome = "Reggae" },
                new Genero { Id = 3, Nome = "Rock Progressivo" },
                new Genero { Id = 4, Nome = "Jazz" },
                new Genero { Id = 5, Nome = "Punk Rock" },
                new Genero { Id = 6, Nome = "Classica" }
            };

            var musicas = new List<Musica>
            {
                new Musica { Id = 1, Nome = "Sweet Child O'Mine", GeneroId = 1 },
                new Musica { Id = 2, Nome = "I Shot The Sheriff", GeneroId = 2 },
                new Musica { Id = 3, Nome = "Danúbio Azul", GeneroId = 6 }
            };

            //Sem o Linq faríamos algo como...

            foreach (var m in musicas)
            {
                foreach (var g in generos)
                {
                    if (g.Id == m.GeneroId)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}",
                            m.Id,
                            m.Nome.PadRight(20),
                            g.Nome);
                    }
                }
            }

            Console.WriteLine();

            // De novo, no código acima, estamos misturando lógica de consulta com lógica de exibição do resultado.
            // ...isso não é bom.

            //Com o Linq, faríamos primeiro...

            var musicaQuery = from m in musicas
                        select m;

            //... porém objeto Musica não tem nome do gênero. Para isso, precisamos combinar esses dados com a lista generos

            var querySimples1 = 
                    from m in musicas
                    join g in generos on m.GeneroId equals g.Id
                    select m;

            // Hm, agora ficou mais parecido ainda com consulta SQL.
            // Preste atenção nesse JOIN:
            // - Ele vai combinar os dados de músicas e de generos através do GeneroId
            // - Ele associa a propriedade de um com a propriedade do outro objeto
            // - Ele usa o operador EQUALS, em vez do sinal de igualdade "=". É assim mesmo.

            // Perceba que acima estamos trazendo somente a música (m). Precisamos trazer também dados de gênero.

            var querySimples2 =
                from m in musicas
                join g in generos on m.GeneroId equals g.Id
                select new { m, g };

            // P: O que é "new { m, g }"?
            // R: É um objeto anônimo, que usamos para "transformar" o resultado da query. Estamos definindo
            //  como saída um objeto com duas propriedades (m e g) e elas contém os objetos Musica e Genero,
            //  respectivamente. Mas podemos melhorar um pouco esse resultado:

            var query = from m in musicas
                    join g in generos on m.GeneroId equals g.Id
                    select new
                    {
                        MusicaId = m.Id,
                        Musica = m.Nome,
                        Genero = g.Nome
                    };

            //Agora o resultado não fez muita diferença, porém ficou mais legível, o que é ótimo.
            //Podemos sempre transformar/projetar o resultado da nossa consulta da maneira que quisermos. O nome disso
            //é "Projeção de Dados" 

            //Agora varremos a nossa query, tabulando os dados conforme desejamos.

            foreach (var musicaEgenero in query)
            {
                Console.WriteLine("{0}\t{1}\t{2}",
                    musicaEgenero.MusicaId,
                    musicaEgenero.Musica.PadRight(20),
                    musicaEgenero.Genero);
            }
        }
    }

    class Genero
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    class Musica
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int GeneroId { get; set; }
    }
}
