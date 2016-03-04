using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using [NOMEPROJETO].Business;
using [NOMEPROJETO].Business.Library;

namespace [NOMEPROJETO].Service.Mail
{
    partial class Service : ServiceBase
    {
        Timer timer = new Timer();
        public Service()
        {
            InitializeComponent();
            this.ServiceName = "[NOMEPROJETO].Service.Mail";
        }

        protected override void OnStart(string[] args)
        {
            int intervalo = Convert.ToInt32(UtilitarioBusiness.RetornaChaveConfig("intervalo"));
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 60000 * intervalo;
            timer.Start();
        }

        protected override void OnStop()
        {   
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            timer.Stop();

            try
            {
                EmailBusiness biz = new EmailBusiness();
                biz.EnviaEmails();
            }
            catch (Exception ex)
            {
                EventLog eventLog = new EventLog();
                eventLog.Source = this.ServiceName;
                eventLog.WriteEntry("Erro na execução: " + ex.Message, EventLogEntryType.Error);
            }
            finally
            {
                timer.Start();
            }
        }
    }
}
