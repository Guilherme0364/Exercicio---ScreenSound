using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;

namespace ScreenSound.Banco
{
    public class MusicaDAL
    {
        private readonly ScreenSoundContext context;

        public MusicaDAL(ScreenSoundContext context)
        {
            this.context = context;
        }

        public IEnumerable<Musica> Listar()
        {
            return context.Musicas.ToList();
        }

        public void Adicionar(Musica musica)
        {
            context.Musicas.Add(musica);
            context.SaveChanges();
        }

        public void Atualizar(Musica musica)
        {
            context.Musicas.Update(musica);
            context.SaveChanges();
        }

        public void Deletar(Musica musica)
        {
            context.Musicas.Remove(musica);
            context.SaveChanges();
        }
    }
}
