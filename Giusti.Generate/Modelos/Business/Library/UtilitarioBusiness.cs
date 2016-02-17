using System;
using System.IO;
using System.Configuration;

namespace [NOMEPROJETO].Business.Library
{
    public class UtilitarioBusiness
    {
        public static void GravaArquivoTexto(string path, bool append, string conteudo)
        {
            StreamWriter writer = new StreamWriter(path, append, System.Text.Encoding.UTF8);
            try
            {
                writer.Write(conteudo);
                writer.Flush();
            }
            finally
            {
                writer.Close();
            }
        }
        public static void GravaLog(string path, string mensagem)
        {
            path += "LOG" + " " + DateTime.Today.Date.ToShortDateString().Replace("/", "-").Replace(" ", "").Replace("00:00:00", "") + ".txt";
            GravaArquivoTexto(path, true, mensagem);
        }
        public static string RetornaChaveConfig(string nome)
        {
            if (ConfigurationManager.AppSettings[nome] != null)
                return ConfigurationManager.AppSettings[nome].ToString();
            else
                return null;
        }
        public static string RetornaExceptionMessages(Exception e, string msgs = "")
        {
            if (e == null) return string.Empty;
            if (msgs == "") msgs = e.Message;
            if (e.InnerException != null)
                msgs += "\r\nInnerException: " + RetornaExceptionMessages(e.InnerException);
            return msgs;
        }
    }
}
