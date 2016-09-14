using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema12
{
    class Problema12 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            using (var contexto = GetContextoComLog())
            {
                var query = from alb in contexto.Albums
                            where alb.Artista.Nome == "Led Zeppelin"
                            orderby alb.Titulo
                            select alb;
                
                foreach (var album in query)
                {
                    Console.WriteLine(album.Titulo);
                }
            }
        }
    }
}
