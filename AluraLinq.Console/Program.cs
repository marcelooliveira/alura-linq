using alura_linq.Problemas.Problema1;
using alura_linq.Problemas.Problema2;
using alura_linq.Problemas.Problema3;
using alura_linq.Problemas.Problema4;
using alura_linq.Problemas.Problema5;
using alura_linq.Problemas.Problema6;
using alura_linq.Problemas.Problema7;
using alura_linq.Problemas.Problema8;
using alura_linq.Problemas.Problema9;
using System;

namespace alura_linq
{
    class Program
    {
        static void Main(string[] args)
        {
            new Problema1().Solve(args);
            new Problema2().Solve(args);
            new Problema3().Solve(args);
            new Problema4().Solve(args);
            new Problema5().Solve(args);
            new Problema6().Solve(args);
            new Problema7().Solve(args);
            new Problema8().Solve(args);
            new Problema9().Solve(args);

            Console.ReadKey();
        }
    }
}
