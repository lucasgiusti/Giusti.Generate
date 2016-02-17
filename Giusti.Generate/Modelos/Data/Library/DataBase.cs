using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using [NOMEPROJETO].Model;

namespace [NOMEPROJETO].Data.Library
{
    public abstract partial class DataBase : IDisposable
    {
        #region Base

        public DataBase()
        {
            this.Context = new EntityContext("Sistema");

            this.Context.Database.Connection.ConnectionString = RetornaConexao();
        }
        ~DataBase()
        {
            Dispose(false);
        }
        protected EntityContext Context { get; private set; }
        private bool Disposed { get; set; }
        public void CommitChanges()
        {
            if (this.Context != null)
            {
                Context.SaveChanges();
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    this.Context.Dispose();
                }

                this.Disposed = true;
            }
        }
        protected void AtualizaPropriedades<T>(T classeEF, T classePoco) where T : class
        {
            ObjectContext objectContext = ((IObjectContextAdapter)Context).ObjectContext;
            ObjectSet<T> set = objectContext.CreateObjectSet<T>();
            IEnumerable<string> keyNames = set.EntitySet.ElementType
            .KeyMembers
            .Select(k => k.Name);
            Type Tipo = typeof(T);
            PropertyInfo[] propriedades = Tipo.GetProperties();
            foreach (PropertyInfo propriedade in propriedades)
            {
                if (propriedade.CanWrite && !keyNames.Where(d => d == propriedade.Name).Any())
                {
                    Type p = propriedade.PropertyType;
                    if (((!p.Namespace.StartsWith("Giusti.Template.Model") && p.Name != "IList`1") || p.IsEnum))
                    {
                        propriedade.SetValue(classeEF, propriedade.GetValue(classePoco, null), null);
                    }

                }
            }
        }
        protected object VerificaValorNulo<T>(T valor)
        {
            if (valor == null)
                return DBNull.Value;
            else
                return valor;
        }
        protected T AjustaValorObjeto<T>(object valorBanco)
        {
            if (valorBanco == null || valorBanco == DBNull.Value)
                return default(T);
            else if (typeof(T) == typeof(string))
                return (T)Convert.ChangeType(Convert.ToString(valorBanco), typeof(T));
            else if (typeof(T) == typeof(Int32))
                return (T)Convert.ChangeType(Convert.ToInt32(valorBanco), typeof(T));
            else if (typeof(T) == typeof(Double))
                return (T)Convert.ChangeType(Convert.ToDouble(valorBanco), typeof(T));
            else
                return (T)valorBanco;

        }
        protected decimal? AjustaValorDecimal(object valorBanco)
        {
            if (valorBanco == null || valorBanco == DBNull.Value)
                return null;
            else
                return Convert.ToDecimal(valorBanco);
        }
        private string RetornaConexao()
        {
            return System.Configuration.ConfigurationManager.AppSettings["ConexaoBanco"];
        }

        #endregion

        #region Log

        public void SalvaLog(Log itemGravar)
        {
            Log itemBase = Context.Loges
            .Where(f => f.Id == itemGravar.Id).FirstOrDefault();
            if (itemBase == null)
            {
                itemBase = Context.Loges.Create();
                Context.Entry<Log>(itemBase).State = System.Data.Entity.EntityState.Added;
            }
            AtualizaPropriedades<Log>(itemBase, itemGravar);
            Context.SaveChanges();
            itemGravar.Id = itemBase.Id;
        }

        #endregion
    }
}
