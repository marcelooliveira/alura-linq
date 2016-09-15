using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema19
{
    class Problema19 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            const int TAMANHO_PAGINA = 5;
            
            for (var pagina = 0; pagina < 4; pagina++)
            {
                MostraPagina(TAMANHO_PAGINA, pagina);
            }
        }

        private void MostraPagina(int TAMANHO_PAGINA, int pagina)
        {
            using (var contexto = GetContextoComLog())
            {
                var query = from nf in contexto.NotasFiscais
                            orderby nf.DataNotaFiscal
                            select new
                            {
                                Numero = nf.NotaFiscalId,
                                Data = nf.DataNotaFiscal,
                                Cliente = nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome,
                                Valor = nf.Total
                            };

                var notasPagina = query
                    .Skip(TAMANHO_PAGINA * pagina)
                    .Take(TAMANHO_PAGINA);

                Console.WriteLine("\nPágina {0}", pagina);
                foreach (var notaFiscal in notasPagina)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}",
                        notaFiscal.Numero,
                        notaFiscal.Data,
                        notaFiscal.Cliente.PadRight(30),
                        notaFiscal.Valor
                        );
                }
            }
        }
    }
}
