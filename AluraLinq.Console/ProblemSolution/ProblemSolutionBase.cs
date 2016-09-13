using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace alura_linq.ProblemSolution
{
    public abstract class ProblemSolutionBase
    {
        public ProblemSolutionBase()
        {
            var ns = this.GetType().Namespace;
            var problema = new Regex(@"(\d+\..*)").Match(ns).Captures[0].Value.Replace("_", " ");
            Console.WriteLine("\n" + problema + "\n");
        }

        public virtual void Solve(string[] args)
        {
            
        }
    }
}
