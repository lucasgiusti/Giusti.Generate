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
    /// Log
    /// </summary>
    public class LogController : ApiBase
    {
        LogBusiness biz = new LogBusiness();

        /// <summary>
        /// Retorna todos os logs
        /// </summary>
        /// <returns></returns>
        public List<Log> Get()
        {
            List<Log> ResultadoBusca = new List<Log>();
            try
            {
                VerificaAutenticacao(Constantes.FuncionalidadeLogConsulta, Constantes.FuncionalidadeNomeLogConsulta, biz);

                //API
                ResultadoBusca = new List<Log>(biz.RetornaLogs());

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
    }
}