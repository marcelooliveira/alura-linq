using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema19
{
    /// <summary>
    /// 19. buscar dados das vendas ordenado por data na nota fiscal, com paginação
    /// </summary>
    class Problema19 : ProblemaBase
    {
        private const int TAMANHO_PAGINA = 10;

        public override void Solve(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                int linhasRelatorio = contexto.NotasFiscais.Count();
                double numeroDePaginas = 
                    Math.Ceiling((double)linhasRelatorio / TAMANHO_PAGINA);

                for (int p = 1; p <= numeroDePaginas; p++)
                {
                    ImprimirPagina(contexto, p);
                }

                Console.ReadKey();
            }
        }

        private static void ImprimirPagina(AluraTunesEntities contexto, int pagina)
        {
            var query =
            from nf in contexto.NotasFiscais
            orderby nf.NotaFiscalId
            select new
            {
                Numero = nf.NotaFiscalId,
                Data = nf.DataNotaFiscal,
                Cliente = nf.Cliente.PrimeiroNome + " " +
                    nf.Cliente.Sobrenome,
                Total = nf.Total
            };

            query = query.Skip((pagina - 1) * TAMANHO_PAGINA);

            query = query.Take(TAMANHO_PAGINA);

            Console.WriteLine();
            Console.WriteLine("Página {0}", pagina);
            Console.WriteLine();

            foreach (var nf in query)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", nf.Numero, nf.Data, nf.Cliente, nf.Total);
            }
        }
    }
}
