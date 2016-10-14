using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema12
{
    /// <summary>
    /// 12. ordenar os álbuns do led zeppelin por nome
    /// </summary>
    class Problema12 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = new AluraTunesEntities())// GetContextoComLog())
            {
                Console.BufferHeight = 1000;

                //var query = from art in contexto.Artistas
                //            orderby art.Nome
                //            select art;

                //foreach (var artista in query)
                //{
                //    Console.WriteLine(artista.Nome);
                //}

                //var query = from f in contexto.Faixas
                //            where f.Album.Artista.Nome == "Iron Maiden"
                //            orderby f.Nome, f.Album.Titulo
                //            select f;

                //foreach (var faixa in query)
                //{
                //    Console.WriteLine("{0}{1}", faixa.Nome.PadRight(30), faixa.Album.Titulo);
                //}

                //var query = from alb in contexto.Albums
                //            where alb.Artista.Nome == "Iron Maiden"
                //            orderby alb.Titulo
                //            select alb;

                //foreach (var album in query)
                //{
                //    Console.WriteLine(album.Titulo);
                //}

                //var query = from f in contexto.Faixas
                //            where f.Album.Artista.Nome == "Iron Maiden"
                //            //orderby f.Album.Titulo
                //            select f;

                //foreach (var faixa in query)
                //{
                //    Console.WriteLine("{0}\t{1}", faixa.Album.Titulo, faixa.Nome);
                //},

                var query = from nf in contexto.NotasFiscais
                            orderby nf.Total descending, nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome
                            select new
                            {
                                nf.DataNotaFiscal,
                                Cliente = nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome,
                                Total = nf.Total
                            };

                foreach (var nf in query)
                {
                    Console.WriteLine("{0:dd/MM/yyyy}\t{1}\t{2}", nf.DataNotaFiscal, nf.Cliente, nf.Total);
                }
            }
        }
    }
}
