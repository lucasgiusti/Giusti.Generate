using System;
using System.Collections.Generic;

namespace [NOMEPROJETO].Model
{
    public class Funcionalidade
    {
        public Funcionalidade()
        {
        }

        public int? Id { get; set; }
        public string Nome { get; set; }
        public int? FuncionalidadeIdPai { get; set; }
        public Funcionalidade FuncionalidadePai { get; set; }
        public IList<Funcionalidade> FuncionalidadesFilho { get; set; }
        public bool UtilizaMenu { get; set; }
        public string LinkMenu { get; set; }
    }
}
