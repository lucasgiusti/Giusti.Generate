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
                    IncluiSucessoBusiness("Perfil_SalvaPerfilOK");
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
                    IncluiSucessoBusiness("Perfil_ExcluiPerfilOK");
                }
            }
        }

        public void ValidaRegrasSalvar(Perfil itemGravar)
        {
            if (IsValid() && itemGravar.Id.HasValue)
            {
                Perfil itemBase = RetornaPerfil_Id((int)itemGravar.Id);
                ValidaExistencia(itemBase);
            }

            if (IsValid() && itemGravar.Id == (int)Constantes.PerfilMasterId)
                IncluiErroBusiness("Perfil_SemPermissaoEdicaoExclusao");

            if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.Nome))
                IncluiErroBusiness("Perfil_Nome");
        }
        public void ValidaRegrasExcluir(Perfil itemGravar)
        {
            if (IsValid())
                ValidaExistencia(itemGravar);

            if (IsValid() && itemGravar.Id == (int)Constantes.PerfilMasterId)
                IncluiErroBusiness("Perfil_SemPermissaoEdicaoExclusao");

            if (IsValid())
            {
                PerfilUsuarioBusiness biz = new PerfilUsuarioBusiness();
                var UsuariosAssociados = biz.RetornaPerfilUsuarios_PerfilId_UsuarioId(itemGravar.Id, null);

                if (UsuariosAssociados.Count > 0)
                    IncluiErroBusiness("Perfil_CadastroUtilizado");
            }
        }
        public void ValidaExistencia(Perfil itemGravar)
        {
            if (itemGravar == null)
                IncluiErroBusiness("Perfil_NaoEncontrado");
        }
    }
}
