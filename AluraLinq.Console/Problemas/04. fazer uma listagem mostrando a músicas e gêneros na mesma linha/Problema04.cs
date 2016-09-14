﻿using System;
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

            var query = from m in musicas
                        join g in generos on m.GeneroId equals g.Id
                        select new
                        {
                            MusicaId = m.Id,
                            Musica = m.Nome,
                            Genero = g.Nome
                        };

            foreach (var musicaXgenero in query)
            {
                Console.WriteLine("{0}\t{1}\t{2}", 
                    musicaXgenero.MusicaId, 
                    musicaXgenero.Musica.PadRight(20), 
                    musicaXgenero.Genero);
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
