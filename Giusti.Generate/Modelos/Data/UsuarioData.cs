using System.Collections.Generic;
using System.Linq;
using System.Data;
using [NOMEPROJETO].Model;
using [NOMEPROJETO].Data.Library;

namespace [NOMEPROJETO].Data
{
    public class UsuarioData : DataBase
    {
        public Usuario RetornaUsuario_Id(int id)
        {
            IQueryable<Usuario> query = Context.Usuarios;

            query = query.Where(d => d.Id == id);
            return query.FirstOrDefault();
        }
        public Usuario RetornaUsuario_Email(string email)
        {
            IQueryable<Usuario> query = Context.Usuarios;

            if (!string.IsNullOrEmpty(email))
                query = query.Where(d => d.Email == email);
            return query.FirstOrDefault();
        }
        public IList<Usuario> RetornaUsuarios()
        {
            IQueryable<Usuario> query = Context.Usuarios;

            return query.ToList();
        }
        public void SalvaUsuario(Usuario itemGravar)
        {
            Usuario itemBase = Context.Usuarios.Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            if (itemBase == null)
            {
                itemBase = Context.Usuarios.Create();
                Context.Entry<Usuario>(itemBase).State = System.Data.Entity.EntityState.Added;
            }
            AtualizaPropriedades<Usuario>(itemBase, itemGravar);
            Context.SaveChanges();
            itemGravar.Id = itemBase.Id;
        }
        public void ExcluiUsuario(Usuario itemGravar)
        {
            Usuario itemExcluir = Context.Usuarios.Where(f => f.Id == itemGravar.Id).FirstOrDefault();

            Context.Entry<Usuario>(itemExcluir).State = System.Data.Entity.EntityState.Deleted;
            Context.SaveChanges();
        }
    }
}
