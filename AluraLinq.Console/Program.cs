using alura_linq.ProblemSolution._1._criar_uma_coleção_simples_e_pequena;
using alura_linq.ProblemSolution._2._listar_os_gêneros;
using alura_linq.ProblemSolution._3._criar_uma_nova_coleção_simples_e_pequena__músicas_;
using alura_linq.ProblemSolution._4._fazer_uma_listagem_mostrando_a_músicas_e_gêneros_na_mesma_linha;
using alura_linq.ProblemSolution._5._consultar_um_arquivo_XML__banco_de_dados__para_listar_os_nomes_dos_artistas_que_existem_na_nossa_loja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace alura_linq
{
    class Program
    {
        static void Main(string[] args)
        {
            new Problem1().Solve(args);
            new Problem2().Solve(args);
            new Problem3().Solve(args);
            new Problem4().Solve(args);
            new Problem5().Solve(args);

            Console.ReadKey();
        }

        private static void LinqToEntities()
        {
            using (var context = new AluraTunesEntities())
            {
                foreach (var genero in context.Generos)
                {
                    Console.WriteLine(genero.Nome);
                }
            }
        }

        private static void LinqToXML()
        {
            XElement root = XElement.Load(@"C:\Users\caelum\Repos\alura-linq\AluraLinq.Console\Data\Generos.xml");

            var query = from g in root.Descendants("Genero")
                        select new
                        {
                            GeneroId = g.Elements("GeneroId").First().Value,
                            Nome = g.Elements("Nome").First().Value
                        };

            foreach (var genero in query)
            {
                Console.WriteLine("{0} - {1}", genero.GeneroId, genero.Nome);
            }
        }
    }
}
