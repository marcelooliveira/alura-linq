using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alura_linq.Problemas.Problema26
{
    class Problema26 : ProblemaBase
    {
        public override void Solve(string[] args)
        {
            var sufixo = DateTime.Now.Ticks.ToString();
            var nomeArtista = "artista_" + sufixo;
            var nomeAlbum = "album_" + sufixo;
            var nomeFaixa = "faixa_" + sufixo;

            try
            {
                using (var contexto = new AluraTunesEntities())
                {
                    var genero = contexto.Generos.Where(g => g.Nome == "Rock").Single();
                    var tipoMidia = contexto.TipoMidias.Where(tm => tm.Nome == "MPEG audio file").Single();

                    Console.WriteLine("Inserindo artista...");
                    var novoArtista =
                    contexto.Artistas.Add(new Artista
                    {
                        Nome = nomeArtista
                    });

                    var artistaFoiInseridoQuery = contexto.Artistas.Where(a => a.Nome == nomeArtista);

                    if (artistaFoiInseridoQuery.Any())
                    {
                        Console.WriteLine("artista já foi inserido");
                    }
                    else
                    {
                        Console.WriteLine("artista ainda não foi inserido");
                    }

                    Console.WriteLine("Inserindo álbum...");
                    var novoAlbum =
                        contexto.Albums.Add(new Album
                        {
                            ArtistaId = novoArtista.ArtistaId,
                            Titulo = nomeAlbum
                        });

                    var albumFoiInseridoQuery = contexto.Albums.Where(a => a.Titulo == nomeAlbum);

                    if (albumFoiInseridoQuery.Any())
                    {
                        Console.WriteLine("álbum já foi inserido");
                    }
                    else
                    {
                        Console.WriteLine("álbum ainda não foi inserido");
                    }

                    Console.WriteLine("Inserindo faixa...");
                    var novaFaixa =
                        contexto.Faixas.Add(new Faixa
                        {
                            Nome = nomeFaixa,
                            AlbumId = novoAlbum.AlbumId,
                            Bytes = 1000000,
                            Compositor = nomeArtista,
                            GeneroId = genero.GeneroId,
                            Milissegundos = 180000,
                            PrecoUnitario = .99M,
                            TipoMidiaId = tipoMidia.TipoMidiaId
                        });

                    var faixaFoiInseridaQuery = contexto.Faixas.Where(a => a.Nome == nomeFaixa);

                    if (faixaFoiInseridaQuery.Any())
                    {
                        Console.WriteLine("faixa já foi inserida");
                    }
                    else
                    {
                        Console.WriteLine("faixa ainda não foi inserida");
                    }

                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }

}
