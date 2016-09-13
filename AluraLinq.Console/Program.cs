using alura_linq.ProblemSolution._1._criar_uma_coleção_simples_e_pequena;
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
            Problem1.Solve(args);

            //LinqToEntities();

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
