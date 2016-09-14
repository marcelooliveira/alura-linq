using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema8
{
    /// <summary>
    /// 08. Fazer a mesma busca acima, porém com sintaxe de métodos em vez de sintaxe de consulta
    /// </summary>
    class Problema8 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                //A sintaxe de método para essa query é um pouco mais compacta que a versão anterior:
                var query = contexto.Artistas.Where(a => a.Nome.StartsWith("Led"));

                foreach (var artista in query)
                {
                    Console.WriteLine("{0}\t{1}", artista.ArtistaId, artista.Nome);
                }

                //P: Como sei quando usar a sintaxe de consulta ou sintaxe de método?
                //R: Ambos vão realizar a mesma tarefa. Em geral, a sintaxe de método é mais
                //apropriada para consultas pequenas, como no exemplo acima. Já em consultas complexas,
                //envolvendo múltiplas fontes de dados, a sintaxe de consulta é mais legível e deve ser
                //considerada.
            }
        }
    }
}
