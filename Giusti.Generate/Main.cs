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

        private void btnGerar_Click(object sender, EventArgs e)
        {
            try
            {
                string nomeProjeto = txtNomeProjeto.Text.Trim();
                string diretorio = txtDiretorio.Text.Trim();
                string diretorioProjeto = string.Format("{0}\\{1}", diretorio, nomeProjeto);
                string dataSource = txtDataSource.Text.Trim();
                string userId = txtUserId.Text.Trim();
                string password = txtPassword.Text.Trim();
                string initialCatalog = txtInitialCatalog.Text.Trim();

                if (nomeProjeto == string.Empty)
                {
                    MessageBox.Show("Informe o nome do projeto");
                    return;
                }
                if (diretorio == string.Empty)
                {
                    MessageBox.Show("Informe o diretório em que o projeto será gerado");
                    return;
                }
                if (dataSource == string.Empty)
                {
                    MessageBox.Show("Informe o dataSource");
                    return;
                }
                if (userId == string.Empty)
                {
                    MessageBox.Show("Informe o userId");
                    return;
                }
                if (password == string.Empty)
                {
                    MessageBox.Show("Informe o password");
                    return;
                }
                if (initialCatalog == string.Empty)
                {
                    MessageBox.Show("Informe o initialCatalog");
                    return;
                }

                bool existeDiretorio = Directory.Exists(diretorioProjeto);
                if (existeDiretorio)
                {
                    MessageBox.Show("Projeto existente");
                    return;
                }

                Directory.CreateDirectory(diretorioProjeto);
                string guidProjetoModel = Guid.NewGuid().ToString();
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}", diretorioProjeto, nomeProjeto, "Model"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Model", "Dominio"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Model", "Properties"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Model", "Resource"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Model", "Results"));

                string guidProjetoData = Guid.NewGuid().ToString();
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}", diretorioProjeto, nomeProjeto, "Data"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Data", "Configuration"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Data", "Library"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Data", "Properties"));

                string guidProjetoBusiness = Guid.NewGuid().ToString();
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}", diretorioProjeto, nomeProjeto, "Business"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Business", "Library"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Business", "Properties"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Business", "Resource"));

                string guidProjetoWeb = Guid.NewGuid().ToString();
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}", diretorioProjeto, nomeProjeto, "Web"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "app"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "app", "controllers"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "app", "library"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "app", "templates"));

                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "App_Data"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "App_Start"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "Content"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "Controllers"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "Controllers", "Api"));

                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "fonts"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "Library"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "Properties"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "Scripts"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "Scripts", "angular-ui"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "Scripts", "i18n"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "Views"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "Views", "Home"));
                Directory.CreateDirectory(string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "Views", "Shared"));

                Directory.CreateDirectory(string.Format("{0}\\{1}", diretorioProjeto, "Componentes"));

                Directory.CreateDirectory(string.Format("{0}\\{1}", diretorioProjeto, "BaseDeDados"));
                Directory.CreateDirectory(string.Format("{0}\\{1}\\{2}", diretorioProjeto, "BaseDeDados", "01 - DDL"));
                Directory.CreateDirectory(string.Format("{0}\\{1}\\{2}", diretorioProjeto, "BaseDeDados", "02 - DML"));
                Directory.CreateDirectory(string.Format("{0}\\{1}", diretorioProjeto, "Documentos"));

                string[] diretoriosArquivosFixosOrigem = new string[] {
                    "..\\..\\ArquivosFixos\\Componentes",
                    "..\\..\\ArquivosFixos\\Web",
                    "..\\..\\ArquivosFixos\\Data",
                    "..\\..\\ArquivosFixos\\Web\\app\\controllers",
                    "..\\..\\ArquivosFixos\\Web\\app\\library",
                    "..\\..\\ArquivosFixos\\Web\\app\\templates",
                    "..\\..\\ArquivosFixos\\Web\\Content",
                    "..\\..\\ArquivosFixos\\Web\\fonts",
                    "..\\..\\ArquivosFixos\\Web\\Views"};


                string[] diretoriosArquivosFixosDestino = new string[] {
                    string.Format("{0}\\{1}", diretorioProjeto, "Componentes"),
                    string.Format("{0}\\{1}.{2}", diretorioProjeto, nomeProjeto, "Data"),
                    string.Format("{0}\\{1}.{2}", diretorioProjeto, nomeProjeto, "Web"),
                    string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "app", "controllers"),
                    string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "app", "library"),
                    string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "app", "templates"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "Content"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "fonts"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "Views")};

                for (int i = 0; i < diretoriosArquivosFixosOrigem.Length; i++)
                {

                    string[] files = Directory.GetFiles(diretoriosArquivosFixosOrigem[i]);
                    foreach (string s in files)
                    {
                        string fileName = Path.GetFileName(s);
                        string destFile = Path.Combine(diretoriosArquivosFixosDestino[i], fileName);
                        File.Copy(s, destFile, true);
                    }
                }


                string[] diretoriosModelosOrigem = new string[] {
                    "..\\..\\Modelos\\Model\\Results",
                    "..\\..\\Modelos\\Model\\Dominio",
                    "..\\..\\Modelos\\Model\\Resource",
                    "..\\..\\Modelos\\Model\\Properties",
                    "..\\..\\Modelos\\Data\\Library",
                    "..\\..\\Modelos\\Data\\Configuration",
                    "..\\..\\Modelos\\Data\\Properties",
                    "..\\..\\Modelos\\Business\\Library",
                    "..\\..\\Modelos\\Business\\Resource",
                    "..\\..\\Modelos\\Business\\Properties",
                    "..\\..\\Modelos\\Web\\App_Start",
                    "..\\..\\Modelos\\Web\\Library",
                    "..\\..\\Modelos\\Web\\Views",
                    "..\\..\\Modelos\\Web\\Views\\Home",
                    "..\\..\\Modelos\\Web\\Views\\Shared",
                    "..\\..\\Modelos\\Web\\Controllers",
                    "..\\..\\Modelos\\Web\\Controllers\\Api",
                    "..\\..\\Modelos\\Web\\app\\controllers",
                    "..\\..\\Modelos\\Web\\app\\templates",
                    "..\\..\\Modelos\\Web\\Properties",
                    "..\\..\\Modelos\\Web\\Scripts",
                    "..\\..\\Modelos\\Web\\Scripts\\angular-ui",
                    "..\\..\\Modelos\\Web\\Scripts\\i18n",
                    "..\\..\\Modelos\\Model",
                    "..\\..\\Modelos\\Data",
                    "..\\..\\Modelos\\Business",
                    "..\\..\\Modelos\\Web",
                    "..\\..\\Modelos"};


                string[] diretoriosModelosDestino = new string[] {
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Model", "Results"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Model", "Dominio"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Model", "Resource"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Model", "Properties"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Data", "Library"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Data", "Configuration"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Data", "Properties"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Business", "Library"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Business", "Resource"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Business", "Properties"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "App_Start"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "Library"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "Views"),
                    string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "Views", "Home"),
                    string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "Views", "Shared"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "Controllers"),
                    string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "Controllers", "Api"),
                    string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "app", "controllers"),
                    string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "app", "templates"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "Properties"),
                    string.Format("{0}\\{1}.{2}\\{3}", diretorioProjeto, nomeProjeto, "Web", "Scripts"),
                    string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "Scripts", "angular-ui"),
                    string.Format("{0}\\{1}.{2}\\{3}\\{4}", diretorioProjeto, nomeProjeto, "Web", "Scripts", "i18n"),
                    string.Format("{0}\\{1}.{2}", diretorioProjeto, nomeProjeto, "Model"),
                    string.Format("{0}\\{1}.{2}", diretorioProjeto, nomeProjeto, "Data"),
                    string.Format("{0}\\{1}.{2}", diretorioProjeto, nomeProjeto, "Business"),
                    string.Format("{0}\\{1}.{2}", diretorioProjeto, nomeProjeto, "Web"),
                    string.Format("{0}", diretorioProjeto)};

                for (int i = 0; i < diretoriosModelosOrigem.Length; i++)
                {

                    string[] files = Directory.GetFiles(diretoriosModelosOrigem[i]);
                    foreach (string s in files)
                    {
                        string fileName = Path.GetFileName(s);
                        string destFile = Path.Combine(diretoriosModelosDestino[i], fileName);
                        destFile = destFile.Replace("[NOMEPROJETO]", nomeProjeto);

                        File.Copy(s, destFile, true);

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

                        content = content.Replace("[NOMEPROJETO]", nomeProjeto);
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




                MessageBox.Show("Projeto gerado com sucesso");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar projeto: " + ex.Message);
            }
        }
    }
}
