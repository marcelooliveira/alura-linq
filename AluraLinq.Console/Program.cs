using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            using (var context = new AluraTunesEntities())
            {
                foreach (var cliente in context.Clientes.Take(3))
                {
                    Console.WriteLine(cliente.Sobrenome);
                }
            }

            Console.ReadKey();
        }
    }
}
