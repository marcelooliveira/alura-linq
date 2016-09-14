﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema6
{
    public class Problema6 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                var query = from f in contexto.Faixas
                            join g in contexto.Generos
                                on f.GeneroId equals g.GeneroId
                            select new
                            {
                                FaixaId = f.FaixaId,
                                Nome = f.Nome,
                                Genero = g.Nome
                            };

                // antes de rodar com Take, rodar para listar tudo
                foreach (var faixaGenero in query.Take(10))
                {
                    Console.WriteLine("{0}\t{1}\t{2}",
                        faixaGenero.FaixaId,
                        faixaGenero.Nome,
                        faixaGenero.Genero);
                }

                // POSSO EVITAR O JOIN E UTILIZAR A PROPRIEDADE DE NAVEGACAO Faixa.Genero

                var querySemJoin = from f in contexto.Faixas
                            select new
                            {
                                FaixaId = f.FaixaId,
                                Nome = f.Nome,
                                Genero = f.Genero.Nome
                            };

                foreach (var faixaGenero in querySemJoin.Take(10))
                {
                    Console.WriteLine("{0}\t{1}\t{2}",
                        faixaGenero.FaixaId,
                        faixaGenero.Nome,
                        faixaGenero.Genero);
                }
            }
        }
    }
}