using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Giusti.Generate
{
    public partial class Main : Form
    {
        string nomeProjeto = string.Empty;
        string guidProjetoModel = string.Empty;
        string guidProjetoData = string.Empty;
        string guidProjetoBusiness = string.Empty;
        string guidProjetoWeb = string.Empty;
        string guidProjetoMail = string.Empty;
        string diretorio = string.Empty;
        string diretorioProjeto = string.Empty;
        string dataSource = string.Empty;
        string userId = string.Empty;
        string password = string.Empty;
        string initialCatalog = string.Empty;
        string emailRemetente = string.Empty;
        string diretorioLogServiceMail = string.Empty;

        public Main()
        {
            InitializeComponent();
        }

        private void btnDiretorio_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
                txtDiretorio.Text = folderBrowserDialog1.SelectedPath;
            else
                txtDiretorio.Text = string.Empty;
        }

        private void btnDiretorioArquivoLogServiceMail_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
                txtDiretorioLogServiceMail.Text = folderBrowserDialog1.SelectedPath;
            else
                txtDiretorioLogServiceMail.Text = string.Empty;
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            try
            {
                nomeProjeto = txtNomeProjeto.Text.Trim();
                diretorio = txtDiretorio.Text.Trim();
                diretorioProjeto = string.Format("{0}\\{1}", diretorio, nomeProjeto);
                dataSource = txtDataSource.Text.Trim();
                userId = txtUserId.Text.Trim();
                password = txtPassword.Text.Trim();
                initialCatalog = txtInitialCatalog.Text.Trim();
                emailRemetente = txtEmailRemetente.Text.Trim();
                diretorioLogServiceMail = txtDiretorioLogServiceMail.Text.Trim();

                if (DadosValidos())
                {
                    guidProjetoModel = Guid.NewGuid().ToString();
                    guidProjetoData = Guid.NewGuid().ToString();
                    guidProjetoBusiness = Guid.NewGuid().ToString();
                    guidProjetoWeb = Guid.NewGuid().ToString();
                    guidProjetoMail = Guid.NewGuid().ToString();
                    CopiaArquivos("..\\..\\[NOMEPROJETO]", string.Format("{0}", diretorioProjeto));

                    MessageBox.Show("Projeto gerado com sucesso");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar projeto: " + ex.Message);
            }
        }

        private void CopiaArquivos(string diretorio, string diretorioDestino)
        {
            string[] diretorioSplit = diretorio.Split('\\');
            string diretorioSplitUltimo = diretorioSplit[diretorioSplit.Length - 1];

            if (!diretorioSplitUltimo.Equals("[NOMEPROJETO]"))
                diretorioDestino += "\\" + diretorioSplitUltimo;

            diretorioDestino = diretorioDestino.Replace("[NOMEPROJETO]", nomeProjeto);

            if (!Directory.Exists(diretorioDestino))
                Directory.CreateDirectory(diretorioDestino);

            string[] diretorios = Directory.GetDirectories(diretorio);

            if (diretorios.Length > 0)
            {
                diretorios.ToList().ForEach(a => {
                    CopiaArquivos(a, diretorioDestino);
                });
            }

            string[] files = Directory.GetFiles(diretorio);
            foreach (string s in files)
            {
                string fileName = Path.GetFileName(s);
                string destFile = Path.Combine(diretorioDestino, fileName);
                destFile = destFile.Replace("[NOMEPROJETO]", nomeProjeto);

                File.Copy(s, destFile, true);

                if (!s.EndsWith(".dll"))
                {

                    var content = string.Empty;
                    using (StreamReader reader = new StreamReader(destFile))
                    {
                        content = reader.ReadToEnd();
                        reader.Close();
                    }

                    content = content.Replace("[GUIDPROJETOMODEL]", guidProjetoModel);
                    content = content.Replace("[GUIDPROJETODATA]", guidProjetoData);
                    content = content.Replace("[GUIDPROJETOBUSINESS]", guidProjetoBusiness);
                    content = content.Replace("[GUIDPROJETOWEB]", guidProjetoWeb);
                    content = content.Replace("[GUIDPROJETOMAIL]", guidProjetoMail);

                    content = content.Replace("[NOMEPROJETO]", nomeProjeto);
                    content = content.Replace("[URLBASEIIS]", nomeProjeto.ToLower());
                    content = content.Replace("[EMAILREMETENTESERVICEMAIL]", emailRemetente);
                    content = content.Replace("[DIRETORIOLOGSERVICEMAIL]", diretorioLogServiceMail);
                    
                    content = content.Replace("[DATASOURCE]", dataSource);
                    content = content.Replace("[USERID]", userId);
                    content = content.Replace("[PASSWORD]", password);
                    content = content.Replace("[INITIALCATALOG]", initialCatalog);

                    using (StreamWriter writer = new StreamWriter(destFile))
                    {
                        writer.Write(content);
                        writer.Close();
                    }
                }
            }
        }

        private bool DadosValidos()
        {
            if (nomeProjeto == string.Empty)
            {
                MessageBox.Show("Informe o nome do projeto");
                return false;
            }
            if (diretorio == string.Empty)
            {
                MessageBox.Show("Informe o diretório em que o projeto será gerado");
                return false;
            }
            if (dataSource == string.Empty)
            {
                MessageBox.Show("Informe o dataSource");
                return false;
            }
            if (userId == string.Empty)
            {
                MessageBox.Show("Informe o userId");
                return false;
            }
            if (password == string.Empty)
            {
                MessageBox.Show("Informe o password");
                return false;
            }
            if (initialCatalog == string.Empty)
            {
                MessageBox.Show("Informe o initialCatalog");
                return false;
            }

            bool existeDiretorio = Directory.Exists(diretorioProjeto);
            if (existeDiretorio)
            {
                string[] diretorios = Directory.GetDirectories(diretorioProjeto);

                if (diretorios.Length > 1)
                {
                    MessageBox.Show("Projeto existente");
                    return false;
                }
            }
            return true;
        }
    }
}
