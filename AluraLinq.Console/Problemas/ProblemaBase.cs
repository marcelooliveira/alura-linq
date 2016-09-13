using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace alura_linq.Problemas
{
    public abstract class ProblemaBase
    {
        public ProblemaBase()
        {
            var ns = this.GetType().Namespace;
            var problema = ns.Split('.').Last();
            Console.WriteLine("\n" + problema + "\n");
            Console.Clear();
        }

        public virtual void Solve(string[] args)
        {
            
        }
    }
}
