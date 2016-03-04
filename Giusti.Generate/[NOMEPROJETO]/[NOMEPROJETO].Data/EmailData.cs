using System.Collections.Generic;
using System.Linq;
using System.Data;
using [NOMEPROJETO].Model;
using [NOMEPROJETO].Data.Library;

namespace [NOMEPROJETO].Data
{
    public class EmailData : DataBase
    {
        public IList<Email> RetornaEmails(bool Enviado)
        {
            IQueryable<Email> query = Context.Emails;

            if (Enviado)
                query = query.Where(d => d.DataEnvio != null);
            else
                query = query.Where(d => d.DataEnvio == null);

            return query.ToList();
        }
        public void SalvaEmail(Email itemGravar)
        {
            Email itemBase = Context.Emails
            .Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            if (itemBase == null)
            {
                itemBase = Context.Emails.Create();
                Context.Entry<Email>(itemBase).State = System.Data.Entity.EntityState.Added;
            }
            AtualizaPropriedades<Email>(itemBase, itemGravar);
            Context.SaveChanges();
            itemGravar.Id = itemBase.Id;
        }
    }
}
