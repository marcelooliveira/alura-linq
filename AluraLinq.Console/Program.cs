using alura_linq.ProblemSolution._1._criar_uma_coleção_simples_e_pequena;
using alura_linq.ProblemSolution._2._listar_os_gêneros;
using alura_linq.ProblemSolution._3._criar_uma_nova_coleção_simples_e_pequena__músicas_;
using alura_linq.ProblemSolution._4._fazer_uma_listagem_mostrando_a_músicas_e_gêneros_na_mesma_linha;
using alura_linq.ProblemSolution._5._consultar_um_arquivo_XML__banco_de_dados__para_listar_os_nomes_dos_artistas_que_existem_na_nossa_loja;
using alura_linq.ProblemSolution._6._Acessar_uma_nova_fonte_de_dados__sql_server_;
using alura_linq.ProblemSolution._7._procurar_o_artista_por_nome;
using alura_linq.ProblemSolution._8._Fazer_a_mesma_busca_acima__porém_com_sintaxe_de_métodos_em_vez_de_sintaxe_de_consulta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace alura_linq
{
    class Program
    {
        static void Main(string[] args)
        {
            new Problem1().Solve(args);
            new Problem2().Solve(args);
            new Problem3().Solve(args);
            new Problem4().Solve(args);
            new Problem5().Solve(args);
            new Problem6().Solve(args);
            new Problem7().Solve(args);
            new Problem8().Solve(args);

            Console.ReadKey();
        }
    }
}
