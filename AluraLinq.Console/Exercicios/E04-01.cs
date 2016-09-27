using alura_linq.Problemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Exercicios.Problema04
{
    class E04 : ExercicioBase
    {
        public override void Solve(string[] args)
        {
            //A lista abaixo contém os finalistas da prova de 100m rasos das
            //Olimpíadas Rio 2016:

            var atletas = new List<Atleta>()
            {
                new Atleta { Posicao = 1, CodigoPais = "JAM", Nome = "BOLT Usain", Tempo = 9.81f },
                new Atleta { Posicao = 2, CodigoPais = "USA", Nome = "GATLIN Justin", Tempo = 9.89f },
                new Atleta { Posicao = 3, CodigoPais = "CAN", Nome = "DE GRASSE Andre", Tempo = 9.91f },
                new Atleta { Posicao = 4, CodigoPais = "JAM", Nome = "BLAKE Yohan", Tempo = 9.93f },
                new Atleta { Posicao = 5, CodigoPais = "RSA", Nome = "SIMBINE Akani", Tempo = 9.94f },
                new Atleta { Posicao = 6, CodigoPais = "CIV", Nome = "MEITE Ben Youssef", Tempo = 9.96f },
                new Atleta { Posicao = 7, CodigoPais = "FRA", Nome = "VICAUT Jimmy", Tempo = 10.04f },
                new Atleta { Posicao = 8, CodigoPais = "USA", Nome = "BROMELL Trayvon", Tempo = 10.06f }
            };

            //E abaixo está a lista dos países finalistas:

            var paises = new List<Pais>
            {
                new Pais { CodigoPais = "JAM", Nome = "Jamaica" },
                new Pais { CodigoPais = "USA", Nome = "Estados Unidos" },
                new Pais { CodigoPais = "CAN", Nome = "Canadá" },
                new Pais { CodigoPais = "RSA", Nome = "África do Sul" },
                new Pais { CodigoPais = "CIV", Nome = "Costa do Marfim" },
                new Pais { CodigoPais = "FRA", Nome = "França" }
            };

            //O código abaixo foi criado para listar os atletas finalistas e também seus
            //respectivos países:

            foreach (var atleta in atletas)
            {
                foreach (var pais in paises)
                {
                    if (atleta.CodigoPais == pais.CodigoPais)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}", atleta.Posicao, atleta.Nome, pais.Nome);
                    }
                }
            }

            //Reescreva o código acima em forma de consulta Linq, imprimindo uma listagem
            //com a colocação do atleta, o nome do atleta e o país de origem.

            //RESPOSTA ESPERADA:
            //=================

            var query = from a in atletas
                        join p in paises
                            on a.CodigoPais equals p.CodigoPais
                        select new
                        {
                            Posicao = a.Posicao,
                            NomeAtleta = a.Nome,
                            NomePais = p.Nome
                        };

            foreach (var atleta in query)
            {
                Console.WriteLine("{0}\t{1}\t{2}", atleta.Posicao, atleta.NomeAtleta, atleta.NomePais);
            }
        }
    }

    class Atleta
    {
        public int Posicao { get; set; }
        public string CodigoPais { get; set; }
        public string Nome { get; set; }
        public float Tempo { get; set; }
    }
   
    class Pais
    {
        public string CodigoPais { get; set; }
        public string Nome { get; set; }
    }
}
