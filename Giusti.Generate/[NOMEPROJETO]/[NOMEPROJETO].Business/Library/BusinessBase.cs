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
        protected void IncluiErroBusiness(string codigoMensagem)
        {
            IncluiErroBusiness(codigoMensagem, false);
        }
        protected void IncluiErroBusiness(string codigoMensagem, bool mensagemPersonalizada)
        {
            if (mensagemPersonalizada)
                IncluiMensagemErroBusiness(codigoMensagem);
            else
                IncluiMensagemErroBusiness(MensagemBusiness.RetornaMensagens(codigoMensagem));
        }
        protected void IncluiMensagemErroBusiness(string mensagem)
        {
            serviceResult.Success = false;
            serviceResult.Messages.Add(new ServiceResultMessage() { Message = mensagem });
        }
        protected void IncluiSucessoBusiness(string codigoMensagem)
        {
            IncluiSucessoBusiness(codigoMensagem, false);
        }
        protected void IncluiSucessoBusiness(string codigoMensagem, bool mensagemPersonalizada)
        {
            if (mensagemPersonalizada)
                IncluiMensagemSucessoBusiness(codigoMensagem);
            else
                IncluiMensagemSucessoBusiness(MensagemBusiness.RetornaMensagens(codigoMensagem));
        }
        protected void IncluiMensagemSucessoBusiness(string mensagem)
        {
            serviceResult = new ServiceResult();
            serviceResult.Success = true;
            serviceResult.Messages.Add(new ServiceResultMessage() { Message = mensagem });
        }

        #endregion

        #region Autenticação

        public void VerificaAutenticacao(string token, string codigoFuncionalidade, string funcionalidade)
        {
            if (string.IsNullOrEmpty(token))
                IncluiErroBusiness("Usuario_NecessarioAutenticacao");
            else
            {
                FormsAuthenticationTicket cookie = FormsAuthentication.Decrypt(token);

                if (cookie.Expired)
                    IncluiErroBusiness("Usuario_LoginExpirado");

                string userData = cookie.UserData;
                string[] roles = userData.Split(',');

                if (!roles.Any(a => a == codigoFuncionalidade))
                    IncluiErroBusiness(string.Format(MensagemBusiness.RetornaMensagens("Usuario_AcessoNegado"), cookie.Name, funcionalidade), true);
            }
        }
        
        #endregion
    }
}
