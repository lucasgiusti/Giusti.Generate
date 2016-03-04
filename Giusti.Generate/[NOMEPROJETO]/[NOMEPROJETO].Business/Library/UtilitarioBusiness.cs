using System;
using System.IO;
using System.Configuration;
using System.Net.Mail;

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
        public static void EnviaEmail(string nomeRemetente, string emailRemetente, string emailDestinatario, string assuntoMensagem, string conteudoMensagem)
        {
            MailMessage objEmail = new MailMessage();

            //Define o Campo From e ReplyTo do e-mail.
            objEmail.From = new System.Net.Mail.MailAddress(nomeRemetente + "<" + emailRemetente + ">");

            //Define os destinatários do e-mail.
            objEmail.To.Add(emailDestinatario);

            //Define a prioridade do e-mail.
            objEmail.Priority = System.Net.Mail.MailPriority.Normal;

            //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
            objEmail.IsBodyHtml = true;

            //Define título do e-mail.
            objEmail.Subject = assuntoMensagem;

            //Define o corpo do e-mail.
            objEmail.Body = conteudoMensagem;

            //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");




            // Caso queira enviar um arquivo anexo
            //Caminho do arquivo a ser enviado como anexo
            //string arquivo = Server.MapPath("arquivo.jpg");

            // Ou especifique o caminho manualmente
            //string arquivo = @"e:\home\LoginFTP\Web\arquivo.jpg";

            // Cria o anexo para o e-mail
            //Attachment anexo = new Attachment(arquivo, System.Net.Mime.MediaTypeNames.Application.Octet);

            // Anexa o arquivo a mensagemn
            //objEmail.Attachments.Add(anexo);

            //Cria objeto com os dados do SMTP
            System.Net.Mail.SmtpClient objSmtp = new System.Net.Mail.SmtpClient();

            //Alocamos o endereço do host para enviar os e-mails, localhost(recomendado) 
            objSmtp.Host = RetornaChaveConfig("smtpServer");
            objSmtp.Port = Convert.ToInt32(RetornaChaveConfig("smtpPort"));

            bool utilizarCredencial = Convert.ToBoolean(RetornaChaveConfig("utilizarCredencial"));

            if (utilizarCredencial)
            {
                objSmtp.EnableSsl = true;
                objSmtp.Credentials = new System.Net.NetworkCredential(RetornaChaveConfig("credencialEmail"), RetornaChaveConfig("senhaCredencialEmail"));
            }

            //Enviamos o e-mail através do método .send()
            try
            {
                objSmtp.Send(objEmail);
            }
            catch
            {
                throw;
            }
            finally
            {
                objEmail.Dispose();
            }
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
