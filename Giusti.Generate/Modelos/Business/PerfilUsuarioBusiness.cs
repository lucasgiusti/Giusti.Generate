using System.Collections.Generic;
using System.Linq;
using [NOMEPROJETO].Model.Results;
using [NOMEPROJETO].Business.Library;
using [NOMEPROJETO].Data;
using [NOMEPROJETO].Model;

namespace [NOMEPROJETO].Business
{
    public class PerfilUsuarioBusiness : BusinessBase
    {
        public PerfilUsuario RetornaPerfilUsuario_Id(int id)
        {
            LimpaValidacao();
            PerfilUsuario RetornoAcao = null;
            if (IsValid())
            {
                using (PerfilUsuarioData data = new PerfilUsuarioData())
                {
                    RetornoAcao = data.RetornaPerfilUsuario_Id(id);
                }
            }

            return RetornoAcao;
        }
        public IList<PerfilUsuario> RetornaPerfilUsuarios()
        {
            LimpaValidacao();
            IList<PerfilUsuario> RetornoAcao = new List<PerfilUsuario>();
            if (IsValid())
            {
                using (PerfilUsuarioData data = new PerfilUsuarioData())
                {
                    RetornoAcao = data.RetornaPerfilUsuarios();
                }
            }

            return RetornoAcao;
        }
        public IList<PerfilUsuario> RetornaPerfilUsuarios_PerfilId_UsuarioId(int? perfilId, int? usuarioId)
        {
            LimpaValidacao();
            IList<PerfilUsuario> RetornoAcao = new List<PerfilUsuario>();
            if (IsValid())
            {
                using (PerfilUsuarioData data = new PerfilUsuarioData())
                {
                    RetornoAcao = data.RetornaPerfilUsuarios_PerfilId_UsuarioId(perfilId, usuarioId);
                }
            }

            return RetornoAcao;
        }
        public void SalvaPerfilUsuario(PerfilUsuario itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasSalvar(itemGravar);
            if (IsValid())
            {
                using (PerfilUsuarioData data = new PerfilUsuarioData())
                {
                    data.SalvaPerfilUsuario(itemGravar);
                    serviceResult = new ServiceResult();
                    serviceResult.Success = true;
                    serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("PerfilUsuario_SalvaPerfilUsuarioOK") });
                }
            }
        }
        public void ExcluiPerfilUsuario(PerfilUsuario itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasExcluir(itemGravar);
            if (IsValid())
            {
                using (PerfilUsuarioData data = new PerfilUsuarioData())
                {
                    data.ExcluiPerfilUsuario(itemGravar);
                    serviceResult = new ServiceResult();
                    serviceResult.Success = true;
                    serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("PerfilUsuario_ExcluiPerfilUsuarioOK") });
                }
            }
        }

        public void ValidaRegrasSalvar(PerfilUsuario itemGravar)
        {

        }
        public void ValidaRegrasExcluir(PerfilUsuario itemGravar)
        {
            ValidaExistencia(itemGravar);
        }
        public void ValidaExistencia(PerfilUsuario itemGravar)
        {
            if (itemGravar == null)
            {
                serviceResult.Success = false;
                serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("PerfilUsuario_NaoEncontrado") });
            }
        }
    }
}
