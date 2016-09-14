﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace alura_linq.Problemas.Problema5
{
    class Problema5 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            XElement root = XElement.Load(@"Data\AluraTunes.xml");

            //COMEÇAR COM UM EXEMPLO MAIS SIMPLES ANTES DE MOSTRAR O JOIN

            var query = from g in root.Element("Generos").Elements("Genero")
                        join m in root.Element("Musicas").Elements("Musica")
                            on g.Element("GeneroId").Value equals m.Element("GeneroId").Value
                        select new
                        {
                            MusicaId = m.Element("MusicaId").Value,
                            Musica = m.Element("Nome").Value,
                            Genero = g.Element("Nome").Value
                        };

            foreach (var musicaXgenero in query)
            {
                Console.WriteLine("{0}\t{1}\t{2}",
                    musicaXgenero.MusicaId,
                    musicaXgenero.Musica.PadRight(20),
                    musicaXgenero.Genero);
            }
        }
    }
}