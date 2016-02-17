using System;
using System.Linq;
using [NOMEPROJETO].Model.Results;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using [NOMEPROJETO].Model;
using [NOMEPROJETO].Data;
using System.Web.Security;

namespace [NOMEPROJETO].Business.Library
{
    public abstract class BusinessBase
    {
        #region Validacao

        public ServiceResult serviceResult = new ServiceResult();
        protected void ValidateService(object entity)
        {
            ValidationFactory.ResetCaches();
            Validator validator = ValidationFactory.CreateValidator(entity.GetType());
            ValidationResults results = validator.Validate(entity);
            AddValidationResults(results);
        }
        protected virtual void AddValidationResults(ValidationResults results)
        {
            foreach (ValidationResult result in results)
            {
                serviceResult.Messages.Add(new ServiceResultMessage() { Message = result.Message });
                serviceResult.Success = false;
            }
        }
        protected void LimpaValidacao()
        {
            serviceResult = new ServiceResult();
        }
        public bool IsValid()
        {
            return serviceResult.Success;
        }
        public void IncluiErro(string codigoMensagemErro)
        {
            IncluiErroBusiness(codigoMensagemErro, false);
        }
        protected void IncluiErroBusiness(string codigoMensagemErro)
        {
            IncluiErroBusiness(codigoMensagemErro, false);
        }
        protected void IncluiErroBusiness(string codigoMensagemErro, bool mensagemPersonalizada)
        {
            if (mensagemPersonalizada)
                IncluiMensagemErroBusiness(codigoMensagemErro);
            else
                IncluiMensagemErroBusiness(MensagemBusiness.RetornaMensagens(codigoMensagemErro));
        }
        protected void IncluiMensagemErroBusiness(string mensagemErro)
        {
            serviceResult = new ServiceResult();
            serviceResult.Success = false;
            serviceResult.Messages.Add(new ServiceResultMessage() { Message = mensagemErro });
        }

        #endregion

        #region Log

        private void ValidaRegrasNegocioLog(Log itemGravar)
        {
        }
        public void SalvaLog(Log itemGravar)
        {
            ValidateService(itemGravar);
            ValidaRegrasNegocioLog(itemGravar);
            if (IsValid())
            {
                using (UsuarioData data = new UsuarioData())
                {
                    data.SalvaLog(itemGravar);
                }
            }
        }

        #endregion

        #region Autenticacao

        private string GeraToken(string email, string acesso)
        {
            try
            {
                FormsAuthenticationTicket authTicket =
                  new FormsAuthenticationTicket(1, email, DateTime.Now, DateTime.Now.AddMinutes(60), false, acesso);

                string ticketCriptografado = FormsAuthentication.Encrypt(authTicket);
                return ticketCriptografado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public void VerificaAutenticacao(string token, string codigoFuncionalidade, string funcionalidade)
        {
            if (string.IsNullOrEmpty(token))
                IncluiErroBusiness("Autenticacao_NecessarioAutenticacao");
            else
            {
                FormsAuthenticationTicket cookie = FormsAuthentication.Decrypt(token);

                if (cookie.Expired)
                    IncluiErroBusiness("Autenticacao_LoginExpirado");

                string userData = cookie.UserData;
                string[] roles = userData.Split(',');

                if (!roles.Any(a => a == codigoFuncionalidade))
                    IncluiErroBusiness(string.Format(MensagemBusiness.RetornaMensagens("Autenticacao_AcessoNegado"), cookie.Name, funcionalidade), true);
            }
        }

        #endregion
    }
}
