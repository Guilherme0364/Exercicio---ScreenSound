using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenSound.Modelos;

namespace ScreenSound.Banco
{
    // Classe para Generics, por isso usamos "<T>", para especificar um tipo genrérico
    public abstract class DAL<T> where T : class // "T" irá representar uma classe
    {
        protected readonly ScreenSoundContext context;

        public DAL(ScreenSoundContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> Listar()
        {
            return context.Set<T>().ToList();
        }

        public void Adicionar(T obj)
        {
            context.Set<T>().Add(obj);
            context.SaveChanges();
        }

        public void Atualizar(T obj)
        {
            context.Set<T>().Update(obj);
            context.SaveChanges();
        }

        public void Deletar(T obj)
        {
            context.Set<T>().Remove(obj);
            context.SaveChanges();
        }

        public T? RecuperarPor(Func<T, bool> condicao) // "Func" é uma nomenclatura de função, onde passamos o tipo (genérico) e o retorno
        {
            return context.Set<T>().FirstOrDefault(condicao);
        }

    }
}
