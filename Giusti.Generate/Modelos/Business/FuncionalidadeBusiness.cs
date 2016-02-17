using System.Collections.Generic;
using System.Linq;
using [NOMEPROJETO].Model.Results;
using [NOMEPROJETO].Business.Library;
using [NOMEPROJETO].Data;
using [NOMEPROJETO].Model;

namespace [NOMEPROJETO].Business
{
    public class FuncionalidadeBusiness : BusinessBase
    {
        public Funcionalidade RetornaFuncionalidade_Id(int? id)
        {
            LimpaValidacao();
            Funcionalidade RetornoAcao = null;
            if (IsValid())
            {
                using (FuncionalidadeData data = new FuncionalidadeData())
                {
                    RetornoAcao = data.RetornaFuncionalidade_Id(id);
                }
            }
            return RetornoAcao;
        }
        public IList<Funcionalidade> RetornaFuncionalidades()
        {
            LimpaValidacao();
            IList<Funcionalidade> RetornoAcao = new List<Funcionalidade>();
            if (IsValid())
            {
                using (FuncionalidadeData data = new FuncionalidadeData())
                {
                    RetornoAcao = data.RetornaFuncionalidades();
                }
            }
            return RetornoAcao;
        }
        public void SalvaFuncionalidade(Funcionalidade itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            if (IsValid())
            {
                using (FuncionalidadeData data = new FuncionalidadeData())
                {

                    data.SalvaFuncionalidade(itemGravar);
                    serviceResult = new ServiceResult();
                    serviceResult.Success = true;
                    serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Funcionalidade_SalvaFuncionalidadeOK") });
                }
            }
        }
        public void ExcluiFuncionalidade(Funcionalidade itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasExcluir(itemGravar);
            if (IsValid())
            {
                using (FuncionalidadeData data = new FuncionalidadeData())
                {

                    data.ExcluiFuncionalidade(itemGravar);
                    serviceResult = new ServiceResult();
                    serviceResult.Success = true;
                    serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Funcionalidade_ExcluiFuncionalidadeOK") });
                }
            }
        }

        private void ValidaRegrasExcluir(Funcionalidade itemGravar)
        {
            IList<PerfilFuncionalidade> PerfilFuncionalidadesAssociadas = new List<PerfilFuncionalidade>();
            PerfilFuncionalidadeBusiness bizPerfilFuncionalidade = new PerfilFuncionalidadeBusiness();
            PerfilFuncionalidadesAssociadas = bizPerfilFuncionalidade.RetornaPerfilFuncionalidades_PerfilId_FuncionalidadeId(null, itemGravar.Id);

            List<Funcionalidade> FuncionalidadeAssociadas = new List<Funcionalidade>();
            using (FuncionalidadeData data = new FuncionalidadeData())
            {
                FuncionalidadeAssociadas = new List<Funcionalidade>(data.RetornaFuncionalidades_FuncionalidadeIdPai(itemGravar.Id));
            }
            if (PerfilFuncionalidadesAssociadas.Count > 0 || FuncionalidadeAssociadas.Count > 0)
            {
                serviceResult = new ServiceResult();
                serviceResult.Success = false;
                serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Funcionalidade_FuncionalidadeUtilizada") });
            }
        }

    }
}
