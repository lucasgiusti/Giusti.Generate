using System;
using System.Collections.Generic;
using System.Web.Http;
using System.IO;
using [NOMEPROJETO].Web.Library;
using System.Net.Http;
using System.Net;
using [NOMEPROJETO].Model;
using [NOMEPROJETO].Model.Dominio;
using System.Web;
using [NOMEPROJETO].Business;

namespace [NOMEPROJETO].Web.Controllers.Api
{
    /// <summary>
    /// Signin
    /// </summary>
    public class SigninController : ApiBase
    {
        UsuarioBusiness biz = new UsuarioBusiness();

        public UsuarioLogado Post([FromBody]Usuario usuario)
        {
            UsuarioLogado usuarioLogado = new UsuarioLogado();
            try
            {
                string ipMaquina = string.Empty;
                string nomeMaquina = string.Empty;
                string email = null;
                string senha = null;

                if (usuario != null)
                {
                    if (!string.IsNullOrEmpty(usuario.Email))
                        email = usuario.Email;
                    if (!string.IsNullOrEmpty(usuario.Senha))
                        senha = usuario.Senha;
                }

                nomeMaquina = Dns.GetHostName();
                //IPAddress[] ip = Dns.GetHostAddresses(nomeMaquina);
                //ipMaquina = ip[1].ToString();
                ipMaquina = "127.0.0.1";

                usuarioLogado = biz.EfetuaLoginSistema(email, senha, ipMaquina, nomeMaquina);

                if (!biz.IsValid())
                    throw new InvalidDataException();

                GravaLog(EnumTipoAcao.Login.ToString(), email);
            }
            catch (InvalidDataException)
            {
                GeraErro(HttpStatusCode.InternalServerError, biz.serviceResult);
            }
            catch (UnauthorizedAccessException)
            {
                GeraErro(HttpStatusCode.Forbidden, biz.serviceResult);
            }
            catch (Exception ex)
            {
                GeraErro(HttpStatusCode.BadRequest, ex);
            }

            return usuarioLogado;
        }
    }
}