using System.Collections.Generic;
using System.Linq;
using System.Data;
using [NOMEPROJETO].Model;
using [NOMEPROJETO].Data.Library;

namespace [NOMEPROJETO].Data
{
    public class PerfilFuncionalidadeData : DataBase
    {
        public PerfilFuncionalidade RetornaPerfilFuncionalidade_Id(int? id)
        {
            IQueryable<PerfilFuncionalidade> query = Context.PerfilFuncionalidades;
            if (id.HasValue)
                query = query.Where(d => d.Id == id);
            return query.FirstOrDefault();
        }
        public IList<PerfilFuncionalidade> RetornaPerfilFuncionalidades()
        {
            IQueryable<PerfilFuncionalidade> query = Context.PerfilFuncionalidades;
            return query.ToList();
        }
        public IList<PerfilFuncionalidade> RetornaPerfilFuncionalidades_PerfilId_FuncionalidadeId(int? perfilId, int? funcionalidadeId)
        {
            IQueryable<PerfilFuncionalidade> query = Context.PerfilFuncionalidades;
            if (perfilId.HasValue)
                query = query.Where(d => d.PerfilId == perfilId);
            if (funcionalidadeId.HasValue)
                query = query.Where(d => d.FuncionalidadeId == funcionalidadeId);
            return query.ToList();
        }
        public void SalvaPerfilFuncionalidade(PerfilFuncionalidade itemGravar)
        {
            PerfilFuncionalidade itemBase = Context.PerfilFuncionalidades.Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            if (itemBase == null)
            {
                itemBase = Context.PerfilFuncionalidades.Create();

                Context.Entry<PerfilFuncionalidade>(itemBase).State = System.Data.Entity.EntityState.Added;
            }
            AtualizaPropriedades<PerfilFuncionalidade>(itemBase, itemGravar);

            Context.SaveChanges();
            itemGravar.Id = itemBase.Id;

        }
        public void ExcluiPerfilFuncionalidade(PerfilFuncionalidade itemGravar)
        {
            PerfilFuncionalidade itemExcluir = Context.PerfilFuncionalidades.Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            Context.Entry<PerfilFuncionalidade>(itemExcluir).State = System.Data.Entity.EntityState.Deleted;

            Context.SaveChanges();
        }
    }
}
