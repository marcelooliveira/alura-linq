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
            using (var context = new AluraTunesEntities())
            {
                foreach (var genero in context.Generos)
                {
                    Console.WriteLine(genero.Nome);
                }
            }

            Console.WriteLine();

            using (var context = new AluraTunesEntities())
            {
                foreach (var cliente in context.Clientes.Take(3))
                {
                    Console.WriteLine(cliente.Sobrenome);
                }
            }

            Console.WriteLine();

            XElement root = XElement.Load(@"C:\Users\caelum\Repos\alura-linq\AluraLinq.Console\Data\Generos.xml");

            var query = from g in root.Descendants("Genero")
                        select g.Descendants("Nome").First().FirstNode;

            foreach (var genero in query)
            {
                Console.WriteLine("{0}", genero);
            }

            Console.ReadKey();
        }
    }
}
