using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema3
{
    /// <summary>
    /// 03. criar uma nova coleção simples e pequena (músicas)
    /// </summary>
    public class Problema3 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            //Ok, já fizemos algo parecido com isso...
            var musicas = new List<Musica>
            {
                new Musica { Id = 1, Nome = "Sweet Child O'Mine", GeneroId = 1 },
                new Musica { Id = 2, Nome = "I Shot The Sheriff", GeneroId = 2 },
                new Musica { Id = 3, Nome = "Danúbio Azul", GeneroId = 3 }
            };
        }
    }

    class Musica
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int GeneroId { get; set; }
    }
}
