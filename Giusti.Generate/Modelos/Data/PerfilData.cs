using System.Collections.Generic;
using System.Linq;
using System.Data;
using [NOMEPROJETO].Model;
using [NOMEPROJETO].Data.Library;

namespace [NOMEPROJETO].Data
{
    public class PerfilData : DataBase
    {
        public Perfil RetornaPerfil_Id(int id)
        {
            IQueryable<Perfil> query = Context.Perfis.Include("PerfilFuncionalidades");

            query = query.Where(d => d.Id == id);
            return query.FirstOrDefault();
        }
        public IList<Perfil> RetornaPerfis()
        {
            IQueryable<Perfil> query = Context.Perfis;

            return query.ToList();
        }
        public void SalvaPerfil(Perfil itemGravar)
        {
            Perfil itemBase = Context.Perfis.Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            if (itemBase == null)
            {
                itemBase = Context.Perfis.Create();
                Context.Entry<Perfil>(itemBase).State = System.Data.Entity.EntityState.Added;
            }
            AtualizaPropriedades<Perfil>(itemBase, itemGravar);

            foreach (PerfilFuncionalidade itemPerfilFuncionalidade in new List<PerfilFuncionalidade>(itemBase.PerfilFuncionalidades))
            {
                if (!itemGravar.PerfilFuncionalidades.Where(f => f.Id == itemPerfilFuncionalidade.Id).Any())
                {
                    Context.Entry<PerfilFuncionalidade>(itemPerfilFuncionalidade).State = System.Data.Entity.EntityState.Deleted;

                }
            }
            foreach (PerfilFuncionalidade itemPerfilFuncionalidade in new List<PerfilFuncionalidade>(itemGravar.PerfilFuncionalidades))
            {
                PerfilFuncionalidade itemBasePerfilFuncionalidade = !itemPerfilFuncionalidade.Id.HasValue ? null : itemBase.PerfilFuncionalidades.Where(f => f.Id == itemPerfilFuncionalidade.Id).FirstOrDefault();
                if (itemBasePerfilFuncionalidade == null)
                {
                    itemBasePerfilFuncionalidade = Context.PerfilFuncionalidades.Create();

                    itemBase.PerfilFuncionalidades.Add(itemBasePerfilFuncionalidade);
                }
                AtualizaPropriedades<PerfilFuncionalidade>(itemBasePerfilFuncionalidade, itemPerfilFuncionalidade);
            }

            Context.SaveChanges();
            itemGravar.Id = itemBase.Id;
        }
        public void ExcluiPerfil(Perfil itemGravar)
        {
            Perfil itemExcluir = Context.Perfis.Include("PerfilFuncionalidades").Where(f => f.Id == itemGravar.Id).FirstOrDefault();

            foreach (PerfilFuncionalidade itemPerfilFuncionalidade in new List<PerfilFuncionalidade>(itemExcluir.PerfilFuncionalidades))
            {
                Context.Entry<PerfilFuncionalidade>(itemPerfilFuncionalidade).State = System.Data.Entity.EntityState.Deleted;

            }

            Context.Entry<Perfil>(itemExcluir).State = System.Data.Entity.EntityState.Deleted;
            Context.SaveChanges();
        }
    }
}
