using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema23
{
    class Problema23 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = GetContextoComLog())
            {
                var minhaFaixa = contexto.Faixas.Where(f => f.Nome == "Smells Like Teen Spirit").First();

                var comprouTambem =
                                  from esteItem in contexto.ItemsNotaFiscal
                                  join outroItem in contexto.ItemsNotaFiscal
                                    on esteItem.NotaFiscalId equals outroItem.NotaFiscalId
                                  where esteItem.FaixaId == minhaFaixa.FaixaId
                                  && esteItem.FaixaId != outroItem.FaixaId
                                  && esteItem.Faixa.Genero.GeneroId == outroItem.Faixa.Genero.GeneroId
                                  group outroItem by outroItem into agrupado
                                  let quantidade = agrupado.Count()
                                  orderby quantidade
                                  select new
                                  {
                                      Artista = agrupado.Key.Faixa.Album.Artista.Nome,
                                      Faixa = agrupado.Key.Faixa.Nome,
                                      Genero = agrupado.Key.Faixa.Genero.Nome,
                                      Quantidade = quantidade
                                  };

                foreach (var item in comprouTambem)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", item.Artista, item.Faixa, item.Genero, item.Quantidade);
                }
            }
        }
    }
}
