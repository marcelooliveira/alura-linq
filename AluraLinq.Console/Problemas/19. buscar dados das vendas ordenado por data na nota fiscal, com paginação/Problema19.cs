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
        public override void Solve(string[] args)
        {
            using (var contexto = GetContextoComLog())
            {
                //Aplicando o que já aprendemos antes, vamos montar uma consulta ordenada,
                //trazendo número da nota, data, cliente e valor da nota
                var query = from nf in contexto.NotasFiscais
                            orderby nf.DataNotaFiscal
                            select new
                            {
                                Numero = nf.NotaFiscalId,
                                Data = nf.DataNotaFiscal,
                                Cliente = nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome,
                                Valor = nf.Total
                            };

                //Obs.: Uma "página" é uma quantidade pequena de elementos que são obtidos a cada
                //consulta do banco de dados. Uma consulta paginada é muito útil porque evita
                //excesso de processamento no servidor, além de reduzir o tráfego de dados pela
                //rede e melhorar o desempenho da aplicação.

                //Agora usamos o método Skip(n) para pular o número de elementos necessários
                //para alcançarmos a página desejada, e invocamos o método Take(n) para
                //pegarmos a quantidade de elementos da página.
            }

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
                var notasPagina = GetNotasDaPagina(TAMANHO_PAGINA, pagina, contexto);
                ImprimeNotasDaPagina(pagina, notasPagina);
            }
        }

        private IQueryable<DetalheNota> GetNotasDaPagina(int TAMANHO_PAGINA, int pagina, AluraTunesEntities contexto)
        {
            var query = from nf in contexto.NotasFiscais
                        orderby nf.DataNotaFiscal
                        select new DetalheNota
                        {
                            Numero = nf.NotaFiscalId,
                            Data = nf.DataNotaFiscal,
                            Cliente = nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome,
                            Valor = nf.Total
                        };

            var notasPagina = query
                .Skip(TAMANHO_PAGINA * pagina)
                .Take(TAMANHO_PAGINA);
            return notasPagina;
        }

        private void ImprimeNotasDaPagina(int pagina, IQueryable<DetalheNota> notasPagina)
        {
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

    class DetalheNota
    {
        public int Numero { get; set; }
        public DateTime Data { get; set; }
        public string Cliente { get; set; }
        public decimal Valor { get; set; }
    }
}
