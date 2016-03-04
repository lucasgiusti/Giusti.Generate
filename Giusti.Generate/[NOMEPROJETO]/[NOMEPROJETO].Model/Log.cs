using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;

namespace [NOMEPROJETO].Model
{
    [HasSelfValidation()]
    public partial class Log
    {
        public Log()
        {
        }

        public int? Id { get; set; }
        public int? UsuarioId { get; set; }
        public int? RegistroId { get; set; }
        public string Acao { get; set; }
        public string OrigemAcesso { get; set; }
        public string IpMaquina { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime? DataInclusao { get; set; }
        public Log Clone()
        {
            return (Log)this.MemberwiseClone();
        }
    }
}
