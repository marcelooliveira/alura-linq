using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace alura_linq.Problemas.Problema5
{
    /// <summary>
    /// 05. consultar um arquivo XML para listar os nomes dos artistas que existem na nossa loja
    /// </summary>
    class Problema5 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            //Agora que vimos como manipular dados em memória (Linq to Objects), vamos explorar outras
            //coisas legais que o Linq consegue fazer. Navegar e buscar dados no XML é uma delas. O nome
            //dessa técnica é "Linq to XML".
            
            //Nesse projeto, criamos um arquivo XML chamado AluraTunes.xml, que contém uma pequena
            //parte do que seria nosso banco de dados de verdade. Vamos acessar o elemento raiz desse
            //arquivo:

            XElement root = XElement.Load(@"Data\AluraTunes.xml");

            var queryXML =
                from g in root.Element("Generos").Elements("Genero")
                select g;

            //P: Se o arquivo XML contém apenas texto, como conseguimos acessar via Linq?
            //R: Preste atenção nessa linha:
            //              root.Element("Generos").Elements("Genero")
            //  nessa linha, o método "Elements" da biblioteca do XML do .Net (System.Xml.Linq) fez o parse dos gêneros para nós, 
            //  isto é, leu texto e converteu para IEnumerable. Lembre-se de que todo IEnumerable pode ser usado 
            //  como fonte de dados para o Linq.

            //P: Eu vi que na assinatura do método "Elements" que ele retorna um IElement<XElement>. Como vou acessar GeneroId e Nome?
            //R: Nesse caso usamos o método "Element" para acessar o nome do elemento e a propriedade "Value" para obter os valores
            foreach (var genero in queryXML)
            {
                Console.WriteLine("{0}\t{1}",
                    genero.Element("GeneroId").Value,
                    genero.Element("Nome").Value);
            }

            Console.WriteLine();

            //Essa consulta parece bem simples. Como podemos alterá-la para combinar gêneros e músicas?
            //Agora que conhecemos um pouco de Linq to XML, faremos exatamente como se estivéssemos acessando 
            //coleções em memória
            
            var query = from g in root.Element("Generos").Elements("Genero")
                        join m in root.Element("Musicas").Elements("Musica")
                            on g.Element("GeneroId").Value equals m.Element("GeneroId").Value
                        select new
                        {
                            MusicaId = m.Element("MusicaId").Value,
                            Musica = m.Element("Nome").Value,
                            Genero = g.Element("Nome").Value
                        };
           
            //Que fará o mesmo que já vimos anteriormente, quando aprendemos sobre Linq to Objects.

            foreach (var musicaEgenero in query)
            {
                Console.WriteLine("{0}\t{1}\t{2}",
                    musicaEgenero.MusicaId,
                    musicaEgenero.Musica.PadRight(20),
                    musicaEgenero.Genero);
            }

            XElement automoveis = XElement.Load(@"Data\Automoveis.xml");
            var queryAutomoveis = 
                        from f in automoveis.Element("Fabricantes").Elements("Fabricante")
                        join m in automoveis.Element("Modelos").Elements("Modelo")
                            on f.Element("FabricanteId").Value equals m.Element("FabricanteId").Value
                        select new
                        {
                            ModeloId = m.Element("ModeloId").Value,
                            Modelo = m.Element("Nome").Value,
                            Fabricante = f.Element("Nome").Value
                        };

            foreach (var modeloEFabricante in queryAutomoveis)
            {
                Console.WriteLine("{0}\t{1}\t{2}",
                    modeloEFabricante.ModeloId,
                    modeloEFabricante.Modelo.PadRight(20),
                    modeloEFabricante.Fabricante);
            }
        }
    }
}
