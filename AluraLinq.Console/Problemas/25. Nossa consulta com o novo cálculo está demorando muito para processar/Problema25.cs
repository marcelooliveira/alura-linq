using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema25
{
    class Problema25 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                var minhaFaixa = contexto.Faixas.Where(f => f.Nome == "Smells Like Teen Spirit").First();

                var comprouTambem =
                                  from esteItem in contexto.ItemsNotaFiscal.AsParallel()
                                  join outroItem in contexto.ItemsNotaFiscal.AsParallel()
                                    on esteItem.NotaFiscalId equals outroItem.NotaFiscalId
                                  where esteItem.FaixaId == minhaFaixa.FaixaId
                                  && esteItem.FaixaId != outroItem.FaixaId
                                  && esteItem.Faixa.Genero.GeneroId == outroItem.Faixa.Genero.GeneroId
                                  group outroItem by outroItem into agrupado
                                  let quantidade = agrupado.Count()
                                  orderby quantidade
                                  select new
                                  {
                                      Faixa = agrupado.Key.Faixa.Nome,
                                      Quantidade = quantidade
                                  };

                foreach (var item in comprouTambem)
                {
                    Console.WriteLine("{0}\t{1}", item.Faixa, item.Quantidade);
                }
            }
        }
    }
}