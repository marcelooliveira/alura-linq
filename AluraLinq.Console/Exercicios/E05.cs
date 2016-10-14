using alura_linq.Problemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace alura_linq.Exercicios.Problema05
{
    class E05 : ExercicioBase
    {
        public override void Solve(string[] args)
        {
            //Considere que o arquivo "\Data\Automoveis.xml" possui o seguinte conteúdo:

            //<?xml version="1.0" encoding="utf-8" ?>
            //<Automoveis>
            //  <Fabricantes>
            //    <Fabricante>
            //      <FabricanteId>1</FabricanteId>
            //      <Nome>Volkswagen</Nome>
            //    </Fabricante>
            //    <Fabricante>
            //      <FabricanteId>2</FabricanteId>
            //      <Nome>Hyundai</Nome>
            //    </Fabricante>
            //    <Fabricante>
            //      <FabricanteId>3</FabricanteId>
            //      <Nome>Toyota</Nome>
            //    </Fabricante>
            //  </Fabricantes>
            //  <Modelos>
            //    <Modelo>
            //      <ModeloId>1001</ModeloId>
            //      <Nome>Passat</Nome>
            //      <FabricanteId>1</FabricanteId>
            //    </Modelo>
            //    <Modelo>
            //      <ModeloId>1002</ModeloId>
            //      <Nome>Azera</Nome>
            //      <FabricanteId>2</FabricanteId>
            //    </Modelo>
            //    <Modelo>
            //      <ModeloId>1003</ModeloId>
            //      <Nome>Corolla</Nome>
            //      <FabricanteId>3</FabricanteId>
            //    </Modelo>    
            //  </Modelos>
            //</Automoveis>

            //Você precisa criar um relatório contendo: 
            //- Nome do modelo
            //- Nome do fabricante

            //Escreva uma consulta "Linq To XML" necessária para obter essas informações a partir
            //do arquivo xml.


            //RESULTADO ESPERADO
            //==================

            XElement root = XElement.Load(@"Data\Automoveis.xml");
            var query = from g in root.Element("Fabricantes").Elements("Fabricante")
                        join m in root.Element("Modelos").Elements("Modelo")
                            on g.Element("FabricanteId").Value equals m.Element("FabricanteId").Value
                        select new
                        {
                            ModeloId = m.Element("ModeloId").Value,
                            Modelo = m.Element("Nome").Value,
                            Fabricante = g.Element("Nome").Value
                        };









            foreach (var modeloEfabricante in query)
            {
                Console.WriteLine("{0}\t{1}",
                    modeloEfabricante.Modelo,
                    modeloEfabricante.Fabricante);
            }
        }
    }

    class Atleta
    {
        public int Posicao { get; set; }
        public string CodigoPais { get; set; }
        public string Nome { get; set; }
        public float Tempo { get; set; }
    }
   
    class Pais
    {
        public string CodigoPais { get; set; }
        public string Nome { get; set; }
    }
}
