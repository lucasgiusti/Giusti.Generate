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
    /// Perfil
    /// </summary>
    public class PerfilController : ApiBase
    {
        PerfilBusiness biz = new PerfilBusiness();

        /// <summary>
        /// Retorna todos os perfis
        /// </summary>
        /// <returns></returns>
        public List<Perfil> Get()
        {
            List<Perfil> ResultadoBusca = new List<Perfil>();
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadePerfilConsulta, Constantes.FuncionalidadeNomePerfilConsulta, biz);
                
                //API
                ResultadoBusca = new List<Perfil>(biz.RetornaPerfis());

                if (!biz.IsValid())
                    throw new InvalidDataException();
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
        /// Retorna o perfil com id solicidado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Perfil Get(int id)
        {
            Perfil ResultadoBusca = new Perfil();
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadePerfilConsulta, Constantes.FuncionalidadeNomePerfilConsulta, biz);

                //API
                ResultadoBusca = biz.RetornaPerfil_Id(id);

                if (!biz.IsValid())
                    throw new InvalidDataException();
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
        /// Inclui um perfil
        /// </summary>
        /// <param name="itemSalvar"></param>
        /// <returns></returns>
        public int? Post([FromBody]Perfil itemSalvar)
        {
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadePerfilInclusao, Constantes.FuncionalidadeNomePerfilInclusao, biz);

                //API
                biz.SalvaPerfil(itemSalvar);
                if (!biz.IsValid())
                    throw new InvalidDataException();

                GravaLog(Constantes.FuncionalidadeNomePerfilInclusao, RetornaEmailAutenticado(), itemSalvar.Id);
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
        /// Altera um determinado perfil
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemSalvar"></param>
        /// <returns></returns>
        public Perfil Put(int id, [FromBody]Perfil itemSalvar)
        {
            try
            {

                VerificaAutenticacao(Constantes.FuncionalidadePerfilEdicao, Constantes.FuncionalidadeNomePerfilEdicao, biz);
                //API
                itemSalvar.Id = id;
                biz.SalvaPerfil(itemSalvar);

                if (!biz.IsValid())
                    throw new InvalidDataException();

                GravaLog(Constantes.FuncionalidadeNomePerfilEdicao, RetornaEmailAutenticado(), itemSalvar.Id);
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
        /// Exclui um determinado perfil
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage retorno = null;
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadePerfilExclusao, Constantes.FuncionalidadeNomePerfilExclusao, biz);

                //API
                Perfil itemExcluir = biz.RetornaPerfil_Id(id);

                biz.ExcluiPerfil(itemExcluir);
                if (!biz.IsValid())
                    throw new InvalidDataException();

                retorno = RetornaMensagemOk(biz.serviceResult);

                GravaLog(Constantes.FuncionalidadeNomePerfilExclusao, RetornaEmailAutenticado(), itemExcluir.Id);
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