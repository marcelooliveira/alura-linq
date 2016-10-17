using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace alura_linq.Exercicios
{
    public abstract class ExercicioBase
    {
        public ExercicioBase()
        {
            //Console.Clear();
            var ns = this.GetType().Namespace;
            var problema = ns.Split('.').Last();
            Console.WriteLine("\n" + problema + "\n");
            Console.WriteLine();
        }

        protected AluraTunesEntities GetContextoComLog()
        {
            var contexto = new AluraTunesEntities();
            contexto.Database.Log = Console.WriteLine;
            return contexto;
        }

        public virtual void Solve(string[] args)
        {
            
        }
    }
}
