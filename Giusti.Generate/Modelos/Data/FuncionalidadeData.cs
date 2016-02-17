using System.Collections.Generic;
using System.Linq;
using System.Data;
using [NOMEPROJETO].Model;
using [NOMEPROJETO].Data.Library;

namespace [NOMEPROJETO].Data
{
    public class FuncionalidadeData : DataBase
    {
        public Funcionalidade RetornaFuncionalidade_Id(int? id)
        {
            IQueryable<Funcionalidade> query = Context.Funcionalidades;
            if (id.HasValue)
                query = query.Where(d => d.Id == id);
            return query.FirstOrDefault();
        }
        public IList<Funcionalidade> RetornaFuncionalidades()
        {
            IQueryable<Funcionalidade> query = Context.Funcionalidades;
            return query.ToList();
        }
        public IList<Funcionalidade> RetornaFuncionalidades_FuncionalidadeIdPai(int? funcionalidadeIdPai)
        {
            IQueryable<Funcionalidade> query = Context.Funcionalidades;
            if (funcionalidadeIdPai.HasValue)
                query = query.Where(d => d.FuncionalidadeIdPai == funcionalidadeIdPai);
            return query.ToList();
        }
        public void SalvaFuncionalidade(Funcionalidade itemGravar)
        {
            Funcionalidade itemBase = Context.Funcionalidades.Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            if (itemBase == null)
            {
                itemBase = Context.Funcionalidades.Create();

                Context.Entry<Funcionalidade>(itemBase).State = System.Data.Entity.EntityState.Added;
            }
            AtualizaPropriedades<Funcionalidade>(itemBase, itemGravar);

            Context.SaveChanges();
            itemGravar.Id = itemBase.Id;

        }
        public void ExcluiFuncionalidade(Funcionalidade itemGravar)
        {
            Funcionalidade itemExcluir = Context.Funcionalidades.Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            Context.Entry<Funcionalidade>(itemExcluir).State = System.Data.Entity.EntityState.Deleted;

            Context.SaveChanges();
        }
    }
}
