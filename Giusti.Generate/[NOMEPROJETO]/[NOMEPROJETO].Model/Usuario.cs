using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;

namespace [NOMEPROJETO].Model
{
    [HasSelfValidation()]
    public class Usuario
    {
        public Usuario()
        {
            Perfis = new List<PerfilUsuario>();
        }

        public int? Id { get; set; }
        public string Nome { get; set; }
        [SelfValidation]
        private void ValidaNome(Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults results)
        {
            if (Nome != null && Nome.Length > 100)
            {
                Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult result =
                      new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult(Resource.Mensagem.Usuario_NomeTamanho, this, "Nome", null, null);
                results.AddResult(result);
            }
        }
        public string Email { get; set; }
        [SelfValidation]
        private void ValidaEmail(Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults results)
        {
            if (Email != null && Email.Length > 100)
            {
                Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult result =
                      new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult(Resource.Mensagem.Usuario_EmailTamanho, this, "Email", null, null);
                results.AddResult(result);
            }
        }
        public string Senha { get; set; }
        [SelfValidation]
        private void ValidaSenha(Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults results)
        {
            if (Senha != null && Senha.Length > 300)
            {
                Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult result =
                      new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult(Resource.Mensagem.Usuario_SenhaTamanho, this, "Senha", null, null);
                results.AddResult(result);
            }
        }
        public string NovaSenha { get; set; }
        public string SenhaConfirmacao { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public IList<PerfilUsuario> Perfis { get; set; }
        public Usuario Clone()
        {
            return (Usuario)this.MemberwiseClone();
        }
    }
}
