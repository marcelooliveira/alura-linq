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
            Console.Clear();
            var ns = this.GetType().Namespace;
            var problema = ns.Split('.').Last();
            Console.WriteLine("\n" + problema + "\n");
        }

        protected AluraTunesEntities GetContextoComLog()
        {
            return new AluraTunesEntities();
        }

        public virtual void Solve(string[] args)
        {
            
        }
    }
}
