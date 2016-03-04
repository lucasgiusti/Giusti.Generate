using [NOMEPROJETO].Model;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;

namespace [NOMEPROJETO].Model
{

    [HasSelfValidation()]
    public partial class Email
    {
        public Email()
        {
        }

        public int? Id { get; set; }
        public string Destinatario { get; set; }
        [SelfValidation]
        private void ValidaDestinatario(Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults results)
        {
            if (Destinatario  != null && Destinatario.Length > 100)
            {
                Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult result =
                      new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult(Resource.Mensagem.Email_DestinatarioTamanho, this, "Destinatario", null, null);
                results.AddResult(result);
            }
        }

        public string Assunto { get; set; }
        [SelfValidation]
        private void ValidaAssunto(Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults results)
        {
            if (Assunto != null && Assunto.Length > 50)
            {
                Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult result =
                      new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult(Resource.Mensagem.Email_AssuntoTamanho, this, "Assunto", null, null);
                results.AddResult(result);
            }
        }

        public string Corpo { get; set; }
        [SelfValidation]
        private void ValidaCorpo(Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults results)
        {
            if (Corpo != null && Corpo.Length > 1000)
            {
                Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult result =
                      new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult(Resource.Mensagem.Email_CorpoTamanho, this, "Corpo", null, null);
                results.AddResult(result);
            }
        }

        public int? FuncionalidadeId { get; set; }

        public DateTime? DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public DateTime? DataEnvio { get; set; }

        public Funcionalidade Funcionalidade { get; set; }

        public Email Clone()
        {
            return (Email)this.MemberwiseClone();
        }
    }

}
