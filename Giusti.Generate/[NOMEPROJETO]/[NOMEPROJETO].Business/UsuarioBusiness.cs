using System.Collections.Generic;
using System.Linq;
using [NOMEPROJETO].Model.Results;
using [NOMEPROJETO].Business.Library;
using [NOMEPROJETO].Data;
using [NOMEPROJETO].Model;
using System;
using [NOMEPROJETO].Model.Dominio;
using System.Web.Security;

namespace [NOMEPROJETO].Business
{
    public class UsuarioBusiness : BusinessBase
    {
        public Usuario RetornaUsuario_Id(int id)
        {
            LimpaValidacao();
            Usuario RetornoAcao = null;
            if (IsValid())
            {
                using (UsuarioData data = new UsuarioData())
                {
                    RetornoAcao = data.RetornaUsuario_Id(id);
                }
            }

            return RetornoAcao;
        }
        public Usuario RetornaUsuario_Email(string email)
        {
            LimpaValidacao();
            Usuario RetornoAcao = null;
            if (IsValid())
            {
                using (UsuarioData data = new UsuarioData())
                {
                    RetornoAcao = data.RetornaUsuario_Email(email);
                }
            }

            return RetornoAcao;
        }
        public IList<Usuario> RetornaUsuarios()
        {
            LimpaValidacao();
            IList<Usuario> RetornoAcao = new List<Usuario>();
            if (IsValid())
            {
                using (UsuarioData data = new UsuarioData())
                {
                    RetornoAcao = data.RetornaUsuarios();
                }
            }

            return RetornoAcao;
        }

        public UsuarioLogado EfetuaLoginSistema(string email, string senha, string ip, string nomeMaquina)
        {
            LimpaValidacao();
            if (string.IsNullOrEmpty(email))
                IncluiErroBusiness("Usuario_Email");

            if (string.IsNullOrEmpty(senha))
                IncluiErroBusiness("Usuario_Senha");

            UsuarioLogado retorno = null;
            if (IsValid())
            {
                UsuarioBusiness bizUsuario = new UsuarioBusiness();
                Usuario itemBase = bizUsuario.RetornaUsuario_Email(email);

                if (itemBase == null)
                    IncluiErroBusiness("Usuario_EmailInvalido");

                if (IsValid() && !PasswordHash.ValidatePassword(senha, itemBase.Senha))
                    IncluiErroBusiness("Usuario_SenhaInvalida");

                if (IsValid())
                {
                    retorno = new UsuarioLogado();
                    retorno.Id = itemBase.Id.Value;
                    retorno.DataHoraAcesso = DateTime.Now;
                    retorno.Email = itemBase.Email;
                    retorno.Nome = itemBase.Nome;
                    retorno.WorkstationId = nomeMaquina;

                    PerfilUsuarioBusiness bizPerfilUsuario = new PerfilUsuarioBusiness();
                    IList<string> listFuncionalidade = bizPerfilUsuario.RetornaFuncionalidades_UsuarioId((int)itemBase.Id);

                    retorno.Token = GeraToken(email, string.Join(",", listFuncionalidade));
                }

            }
            return retorno;
        }
        public void SalvaUsuario(Usuario itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasSalvar(itemGravar);
            if (IsValid())
            {
                using (UsuarioData data = new UsuarioData())
                {
                    data.SalvaUsuario(itemGravar);
                    IncluiSucessoBusiness("Usuario_SalvaUsuarioOK");
                }
            }
        }
        public void ExcluiUsuario(Usuario itemGravar)
        {
            LimpaValidacao();
            ValidateService(itemGravar);
            ValidaRegrasExcluir(itemGravar);
            if (IsValid())
            {
                using (UsuarioData data = new UsuarioData())
                {
                    data.ExcluiUsuario(itemGravar);
                    IncluiSucessoBusiness("Usuario_ExcluiUsuarioOK");
                }
            }
        }
        public void AlteraSenha(Usuario itemGravar)
        {
            LimpaValidacao();
            ValidaRegrasAlterarSenha(ref itemGravar);
            if (IsValid())
            {
                ValidateService(itemGravar);
                ValidaRegrasSalvar(itemGravar);
                if (IsValid())
                {
                    using (UsuarioData data = new UsuarioData())
                    {
                        data.SalvaUsuario(itemGravar);
                        IncluiSucessoBusiness("Usuario_SenhaAlteradaOK");
                    }
                }
            }
        }
        public void GeraNovaSenha(Usuario itemGravar)
        {
            LimpaValidacao();
            ValidaRegrasGerarNovaSenha(ref itemGravar);
            if (IsValid())
            {
                string novaSenha = string.Empty;
                novaSenha = PasswordHash.GenerateRandomPassword();
                itemGravar.Senha = novaSenha;
                itemGravar.SenhaConfirmacao = novaSenha;

                ValidateService(itemGravar);
                ValidaRegrasSalvar(itemGravar);
                if (IsValid())
                {
                    using (UsuarioData data = new UsuarioData())
                    {
                        data.SalvaUsuario(itemGravar);
                        IncluiSucessoBusiness("Usuario_NovaSenhaGeradaOK");

                        GeraEmailEsqueciSenha(itemGravar, novaSenha);
                    }
                }
            }
        }

        public void ValidaRegrasSalvar(Usuario itemGravar)
        {
            if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.Nome))
                IncluiErroBusiness("Usuario_Nome");

            if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.Email))
                IncluiErroBusiness("Usuario_Email");

            if (IsValid())
            {
                Usuario itemBase = RetornaUsuario_Email(itemGravar.Email);
                if (itemBase != null && itemGravar.Id != itemBase.Id)
                    IncluiErroBusiness("Usuario_CadastroDuplicado");
            }
            if (IsValid())
            {
                if (itemGravar.Id.HasValue)
                {
                    Usuario itemBase = RetornaUsuario_Id((int)itemGravar.Id);
                    ValidaExistencia(itemBase);
                    if (IsValid())
                    {
                        itemGravar.DataInclusao = itemBase.DataInclusao;
                        itemGravar.DataAlteracao = DateTime.Now;

                        if (string.IsNullOrWhiteSpace(itemGravar.Senha) && string.IsNullOrWhiteSpace(itemGravar.SenhaConfirmacao))
                            itemGravar.Senha = itemBase.Senha;
                        else
                        {
                            if (string.IsNullOrWhiteSpace(itemGravar.Senha))
                                IncluiErroBusiness("Usuario_Senha");

                            if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.SenhaConfirmacao))
                                IncluiErroBusiness("Usuario_SenhaConfirmacao");

                            if (IsValid() && !itemGravar.Senha.Equals(itemGravar.SenhaConfirmacao))
                                IncluiErroBusiness("Usuario_SenhaConfirmacao_Incorreta");

                            if (IsValid())
                                itemGravar.Senha = PasswordHash.HashPassword(itemGravar.Senha);
                        }
                    }
                }
                else
                {
                    itemGravar.DataInclusao = DateTime.Now;
                    itemGravar.Ativo = true;

                    if (string.IsNullOrWhiteSpace(itemGravar.Senha))
                        IncluiErroBusiness("Usuario_Senha");

                    if (IsValid() && string.IsNullOrWhiteSpace(itemGravar.SenhaConfirmacao))
                        IncluiErroBusiness("Usuario_SenhaConfirmacao");

                    if (IsValid() && !itemGravar.Senha.Equals(itemGravar.SenhaConfirmacao))
                        IncluiErroBusiness("Usuario_SenhaConfirmacao_Incorreta");

                    if (IsValid())
                        itemGravar.Senha = PasswordHash.HashPassword(itemGravar.Senha);
                }
            }


        }
        public void ValidaRegrasExcluir(Usuario itemGravar)
        {
            if (IsValid())
                ValidaExistencia(itemGravar);

            if (IsValid())
            {
                PerfilUsuarioBusiness biz = new PerfilUsuarioBusiness();
                var PerfisAssociados = biz.RetornaPerfilUsuarios_PerfilId_UsuarioId(null, itemGravar.Id);

                if (PerfisAssociados.Count > 0)
                    IncluiErroBusiness("Usuario_CadastroUtilizado");
            }

            if (IsValid())
            {
                LogBusiness biz = new LogBusiness();
                if (biz.ExisteLog_UsuarioId((int)itemGravar.Id))
                    IncluiErroBusiness("Usuario_CadastroUtilizado");
            }

        }
        public void ValidaRegrasAlterarSenha(ref Usuario itemGravar)
        {
            LimpaValidacao();
            if (string.IsNullOrEmpty(itemGravar.Email))
                IncluiErroBusiness("Usuario_Email");

            if (string.IsNullOrEmpty(itemGravar.Senha))
                IncluiErroBusiness("Usuario_Senha");

            if (string.IsNullOrEmpty(itemGravar.NovaSenha))
                IncluiErroBusiness("Usuario_NovaSenha");

            if (string.IsNullOrEmpty(itemGravar.SenhaConfirmacao))
                IncluiErroBusiness("Usuario_NovaSenhaConfirmacao");

            if (IsValid())
            {
                UsuarioBusiness bizUsuario = new UsuarioBusiness();
                Usuario itemBase = bizUsuario.RetornaUsuario_Email(itemGravar.Email);

                if (itemBase == null)
                    IncluiErroBusiness("Usuario_EmailInvalido");

                if (IsValid() && !PasswordHash.ValidatePassword(itemGravar.Senha, itemBase.Senha))
                    IncluiErroBusiness("Usuario_SenhaInvalida");

                if(IsValid())
                {
                    itemBase.Senha = itemGravar.NovaSenha;
                    itemBase.SenhaConfirmacao = itemGravar.SenhaConfirmacao;
                    itemGravar = itemBase;
                }
            }
        }
        public void ValidaRegrasGerarNovaSenha(ref Usuario itemGravar)
        {
            LimpaValidacao();
            if (string.IsNullOrEmpty(itemGravar.Email))
                IncluiErroBusiness("Usuario_Email");

            if (IsValid())
            {
                UsuarioBusiness bizUsuario = new UsuarioBusiness();
                Usuario itemBase = bizUsuario.RetornaUsuario_Email(itemGravar.Email);

                if (itemBase == null)
                    IncluiErroBusiness("Usuario_EmailInvalido");

                if (IsValid())
                    itemGravar = itemBase;
            }
        }
        public void ValidaExistencia(Usuario itemGravar)
        {
            if (itemGravar == null)
                IncluiErroBusiness("Usuario_NaoEncontrado");
        }

        private string GeraToken(string email, string funcionalidades)
        {
            try
            {
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, email, DateTime.Now, DateTime.Now.AddMinutes(60), false, funcionalidades);

                string ticketCriptografado = FormsAuthentication.Encrypt(authTicket);
                return ticketCriptografado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public void GeraEmailEsqueciSenha(Usuario itemGravar, string novaSenha)
        {
            EmailBusiness biz = new EmailBusiness();
            biz.GeraEmailEsqueciSenha(itemGravar, novaSenha);
        }

    }
}
