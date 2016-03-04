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
    /// Usuário
    /// </summary>
    public class UsuarioController : ApiBase
    {
        UsuarioBusiness biz = new UsuarioBusiness();

        /// <summary>
        /// Retorna todos os usuários
        /// </summary>
        /// <returns></returns>
        public List<Usuario> Get()
        {
            List<Usuario> ResultadoBusca = new List<Usuario>();
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeUsuarioConsulta, Constantes.FuncionalidadeNomeUsuarioConsulta, biz);

                //API
                ResultadoBusca = new List<Usuario>(biz.RetornaUsuarios());

                if (!biz.IsValid())
                    throw new InvalidDataException();

                ResultadoBusca.ForEach(a => a.Senha = null);
            }
            catch (InvalidDataException)
            {
                GeraErro(HttpStatusCode.InternalServerError, biz.serviceResult);
            }
            catch (UnauthorizedAccessException)
            {
                GeraErro(HttpStatusCode.Unauthorized, biz.serviceResult);
            }
            catch (Exception ex)
            {
                GeraErro(HttpStatusCode.BadRequest, ex);
            }

            return ResultadoBusca;
        }

        /// <summary>
        /// Retorna o usuário com id solicidado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Usuario Get(int id)
        {
            Usuario ResultadoBusca = new Usuario();
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeUsuarioConsulta, Constantes.FuncionalidadeNomeUsuarioConsulta, biz);
                
                //API
                ResultadoBusca = biz.RetornaUsuario_Id(id);

                if (!biz.IsValid())
                    throw new InvalidDataException();

                if (ResultadoBusca != null)
                    ResultadoBusca.Senha = null;
            }
            catch (InvalidDataException)
            {
                GeraErro(HttpStatusCode.InternalServerError, biz.serviceResult);
            }
            catch (UnauthorizedAccessException)
            {
                GeraErro(HttpStatusCode.Unauthorized, biz.serviceResult);
            }
            catch (Exception ex)
            {
                GeraErro(HttpStatusCode.BadRequest, ex);
            }

            return ResultadoBusca;
        }

        /// <summary>
        /// Inclui um usuário
        /// </summary>
        /// <param name="itemSalvar"></param>
        /// <returns></returns>
        public int? Post([FromBody]Usuario itemSalvar)
        {
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeUsuarioInclusao, Constantes.FuncionalidadeNomeUsuarioInclusao, biz);

                //API
                biz.SalvaUsuario(itemSalvar);
                if (!biz.IsValid())
                    throw new InvalidDataException();

                if (itemSalvar != null)
                {
                    itemSalvar.Senha = null;
                    itemSalvar.SenhaConfirmacao = null;
                }

                GravaLog(Constantes.FuncionalidadeNomeUsuarioInclusao, RetornaEmailAutenticado(), itemSalvar.Id);
            }
            catch (InvalidDataException)
            {
                GeraErro(HttpStatusCode.InternalServerError, biz.serviceResult);
            }
            catch (UnauthorizedAccessException)
            {
                GeraErro(HttpStatusCode.Unauthorized, biz.serviceResult);
            }
            catch (Exception ex)
            {
                GeraErro(HttpStatusCode.BadRequest, ex);
            }

            return itemSalvar.Id;
        }

        /// <summary>
        /// Altera um determinado usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemSalvar"></param>
        /// <returns></returns>
        public Usuario Put(int id, [FromBody]Usuario itemSalvar)
        {
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeUsuarioEdicao, Constantes.FuncionalidadeNomeUsuarioEdicao, biz);

                //API
                itemSalvar.Id = id;
                biz.SalvaUsuario(itemSalvar);

                if (!biz.IsValid())
                    throw new InvalidDataException();

                if (itemSalvar != null)
                {
                    itemSalvar.Senha = null;
                    itemSalvar.SenhaConfirmacao = null;
                }

                GravaLog(Constantes.FuncionalidadeNomeUsuarioEdicao, RetornaEmailAutenticado(), itemSalvar.Id);
            }
            catch (InvalidDataException)
            {
                GeraErro(HttpStatusCode.InternalServerError, biz.serviceResult);
            }
            catch (UnauthorizedAccessException)
            {
                GeraErro(HttpStatusCode.Unauthorized, biz.serviceResult);
            }
            catch (Exception ex)
            {
                GeraErro(HttpStatusCode.BadRequest, ex);
            }

            return itemSalvar;
        }

        /// <summary>
        /// Exclui um determinado usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage retorno = null;
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeUsuarioExclusao, Constantes.FuncionalidadeNomeUsuarioExclusao, biz);

                //API
                Usuario itemExcluir = biz.RetornaUsuario_Id(id);

                biz.ExcluiUsuario(itemExcluir);
                if (!biz.IsValid())
                    throw new InvalidDataException();

                retorno = RetornaMensagemOk(biz.serviceResult);

                GravaLog(Constantes.FuncionalidadeNomeUsuarioExclusao, RetornaEmailAutenticado(), itemExcluir.Id);
            }
            catch (InvalidDataException)
            {
                retorno = RetornaMensagemErro(HttpStatusCode.InternalServerError, biz.serviceResult);
            }
            catch (UnauthorizedAccessException)
            {
                retorno = RetornaMensagemErro(HttpStatusCode.Unauthorized, biz.serviceResult);
            }
            catch (Exception ex)
            {
                retorno = RetornaMensagemErro(HttpStatusCode.BadRequest, ex);
            }

            return retorno;
        }


    }
}