using System;

namespace [NOMEPROJETO].Model
{
    public class UsuarioLogado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string WorkstationId { get; set; }
        public DateTime DataHoraAcesso { get; set; }
        public string Token { get; set; }
    }
}
