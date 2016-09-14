using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema21
{
    class Problema21 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = GetContextoComLog())
            {
                var produtoMaisVendido =
                    (from f in contexto.Faixas
                    let total = f.ItemNotaFiscals.Sum(inf => inf.PrecoUnitario * inf.Quantidade)
                    orderby total descending
                    select new
                    {
                        FaixaId = f.FaixaId,
                        Nome = f.Nome,
                        Total = total
                    }).First();

                var query = from inf in contexto.ItemsNotaFiscal
                            where inf.FaixaId == produtoMaisVendido.FaixaId
                            let cliente = inf.NotaFiscal.Cliente.PrimeiroNome + " " + inf.NotaFiscal.Cliente.Sobrenome
                            orderby cliente
                            select cliente;

                foreach (var cliente in query)
                {
                    Console.WriteLine(cliente);
                }
            }
        }
    }
}
