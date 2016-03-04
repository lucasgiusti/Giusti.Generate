using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Resources;
using System.Reflection;
using [NOMEPROJETO].Model.Resource;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace [NOMEPROJETO].Model
{
    [HasSelfValidation()]
    public partial class Perfil
    {
        public Perfil()
        {
            PerfilFuncionalidades = new List<PerfilFuncionalidade>();
        }
        public int? Id { get; set; }

        public string Nome { get; set; }
        [SelfValidation]
        private void ValidaNome(Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults results)
        {
            if (Nome != null && Nome.Length > 50)
            {
                Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult result =
                      new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult(Resource.Mensagem.Perfil_NomeTamanho, this, "Nome", null, null);
                results.AddResult(result);
            }
        }
        public IList<PerfilFuncionalidade> PerfilFuncionalidades { get; set; }
    }
}
