using System.Collections.Generic;
using System.Linq;
using [NOMEPROJETO].Model.Results;
using [NOMEPROJETO].Business.Library;
using [NOMEPROJETO].Data;
using [NOMEPROJETO].Model;
using [NOMEPROJETO].Model.Dominio;

namespace [NOMEPROJETO].Business
{
    public class PerfilBusiness : BusinessBase
    {
        public Perfil RetornaPerfil_Id(int id)
        {
            LimpaValidacao();
            Perfil RetornoAcao = null;
            if (IsValid())
            {
                using (PerfilData data = new PerfilData())
                {
                    RetornoAcao = data.RetornaPerfil_Id(id);
                }
            }

            return RetornoAcao;
        }
        public IList<Perfil> RetornaPerfis()
        {
            LimpaValidacao();
            IList<Perfil> RetornoAcao = new List<Perfil>();
            if (IsValid())
            {
                using (PerfilData data = new PerfilData())
                {
                    RetornoAcao = data.RetornaPerfis();
                }
            }

            return RetornoAcao;
        }
        public void SalvaPerfil(Perfil itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasSalvar(itemGravar);
            if (IsValid())
            {
                using (PerfilData data = new PerfilData())
                {
                    data.SalvaPerfil(itemGravar);
                    serviceResult = new ServiceResult();
                    serviceResult.Success = true;
                    serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Perfil_SalvaPerfilOK") });
                }
            }
        }
        public void ExcluiPerfil(Perfil itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasExcluir(itemGravar);
            if (IsValid())
            {
                using (PerfilData data = new PerfilData())
                {
                    data.ExcluiPerfil(itemGravar);
                    serviceResult = new ServiceResult();
                    serviceResult.Success = true;
                    serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Perfil_ExcluiPerfilOK") });
                }
            }
        }

        public void ValidaRegrasSalvar(Perfil itemGravar)
        {
            if (IsValid())
            {
                if (itemGravar.Id == (int)Constantes.PerfilMasterId)
                {
                    serviceResult.Success = false;
                    serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Perfil_SemPermissaoEdicaoExclusao") });
                }
            }

            if (IsValid() && itemGravar.Id.HasValue)
            {
                Perfil itemAlterar = RetornaPerfil_Id((int)itemGravar.Id);
                ValidaExistencia(itemAlterar);
            }
        }
        public void ValidaRegrasExcluir(Perfil itemGravar)
        {
            if (IsValid())
                ValidaExistencia(itemGravar);

            if (IsValid())
            {
                if (itemGravar.Id == (int)Constantes.PerfilMasterId)
                {
                    serviceResult.Success = false;
                    serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Perfil_SemPermissaoEdicaoExclusao") });
                }
            }

            if (IsValid())
            {
                PerfilUsuarioBusiness biz = new PerfilUsuarioBusiness();
                var UsuariosAssociados = biz.RetornaPerfilUsuarios_PerfilId_UsuarioId(itemGravar.Id, null);

                if (UsuariosAssociados.Count > 0)
                {
                    serviceResult.Success = false;
                    serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Perfil_CadastroUtilizado") });
                }
            }
        }
        public void ValidaExistencia(Perfil itemGravar)
        {
            if (itemGravar == null)
            {
                serviceResult.Success = false;
                serviceResult.Messages.Add(new ServiceResultMessage() { Message = MensagemBusiness.RetornaMensagens("Perfil_NaoEncontrado") });
            }
        }
    }
}
