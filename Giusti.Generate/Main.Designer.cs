namespace Giusti.Generate
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtNomeProjeto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDiretorio = new System.Windows.Forms.Button();
            this.txtDiretorio = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnGerar = new System.Windows.Forms.Button();
            this.txtDataSource = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtInitialCatalog = new System.Windows.Forms.TextBox();
            this.txtEmailRemetente = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDiretorioLogServiceMail = new System.Windows.Forms.TextBox();
            this.btnDiretorioArquivoLogServiceMail = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nome do Projeto";
            // 
            // txtNomeProjeto
            // 
            this.txtNomeProjeto.Location = new System.Drawing.Point(126, 32);
            this.txtNomeProjeto.Name = "txtNomeProjeto";
            this.txtNomeProjeto.Size = new System.Drawing.Size(252, 20);
            this.txtNomeProjeto.TabIndex = 1;
            this.txtNomeProjeto.Text = "Giusti.Template";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Criar em";
            // 
            // btnDiretorio
            // 
            this.btnDiretorio.Location = new System.Drawing.Point(474, 65);
            this.btnDiretorio.Name = "btnDiretorio";
            this.btnDiretorio.Size = new System.Drawing.Size(48, 23);
            this.btnDiretorio.TabIndex = 3;
            this.btnDiretorio.Text = "...";
            this.btnDiretorio.UseVisualStyleBackColor = true;
            this.btnDiretorio.Click += new System.EventHandler(this.btnDiretorio_Click);
            // 
            // txtDiretorio
            // 
            this.txtDiretorio.Location = new System.Drawing.Point(126, 67);
            this.txtDiretorio.Name = "txtDiretorio";
            this.txtDiretorio.Size = new System.Drawing.Size(342, 20);
            this.txtDiretorio.TabIndex = 2;
            this.txtDiretorio.Text = "C:\\Users\\giust\\Documents\\Visual Studio 2015\\Projects";
            // 
            // btnGerar
            // 
            this.btnGerar.Location = new System.Drawing.Point(12, 252);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(510, 64);
            this.btnGerar.TabIndex = 11;
            this.btnGerar.Text = "Gerar";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // txtDataSource
            // 
            this.txtDataSource.Location = new System.Drawing.Point(126, 100);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.Size = new System.Drawing.Size(151, 20);
            this.txtDataSource.TabIndex = 4;
            this.txtDataSource.Text = ".\\SQLEXPRESS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Data Source";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(295, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "User Id";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Password";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(295, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Initial Catalog";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(371, 100);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(151, 20);
            this.txtUserId.TabIndex = 5;
            this.txtUserId.Text = "sa";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(126, 135);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(151, 20);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.Text = "123456";
            // 
            // txtInitialCatalog
            // 
            this.txtInitialCatalog.Location = new System.Drawing.Point(371, 135);
            this.txtInitialCatalog.Name = "txtInitialCatalog";
            this.txtInitialCatalog.Size = new System.Drawing.Size(151, 20);
            this.txtInitialCatalog.TabIndex = 7;
            this.txtInitialCatalog.Text = "GiustiTemplate";
            // 
            // txtEmailRemetente
            // 
            this.txtEmailRemetente.Location = new System.Drawing.Point(126, 173);
            this.txtEmailRemetente.Name = "txtEmailRemetente";
            this.txtEmailRemetente.Size = new System.Drawing.Size(298, 20);
            this.txtEmailRemetente.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 180);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Email Remetente";
            // 
            // txtDiretorioLogServiceMail
            // 
            this.txtDiretorioLogServiceMail.Location = new System.Drawing.Point(126, 211);
            this.txtDiretorioLogServiceMail.Name = "txtDiretorioLogServiceMail";
            this.txtDiretorioLogServiceMail.Size = new System.Drawing.Size(239, 20);
            this.txtDiretorioLogServiceMail.TabIndex = 9;
            // 
            // btnDiretorioArquivoLogServiceMail
            // 
            this.btnDiretorioArquivoLogServiceMail.Location = new System.Drawing.Point(376, 208);
            this.btnDiretorioArquivoLogServiceMail.Name = "btnDiretorioArquivoLogServiceMail";
            this.btnDiretorioArquivoLogServiceMail.Size = new System.Drawing.Size(48, 23);
            this.btnDiretorioArquivoLogServiceMail.TabIndex = 10;
            this.btnDiretorioArquivoLogServiceMail.Text = "...";
            this.btnDiretorioArquivoLogServiceMail.UseVisualStyleBackColor = true;
            this.btnDiretorioArquivoLogServiceMail.Click += new System.EventHandler(this.btnDiretorioArquivoLogServiceMail_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(430, 176);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "(Serviço de Email)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 214);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(108, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Caminho Arquivo Log";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(430, 213);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(92, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "(Serviço de Email)";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 341);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnDiretorioArquivoLogServiceMail);
            this.Controls.Add(this.txtDiretorioLogServiceMail);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtEmailRemetente);
            this.Controls.Add(this.txtInitialCatalog);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDataSource);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.txtDiretorio);
            this.Controls.Add(this.btnDiretorio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNomeProjeto);
            this.Controls.Add(this.label1);
            this.Name = "Main";
            this.Text = "Giusti.Generate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNomeProjeto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDiretorio;
        private System.Windows.Forms.TextBox txtDiretorio;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.TextBox txtDataSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtInitialCatalog;
        private System.Windows.Forms.TextBox txtEmailRemetente;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDiretorioLogServiceMail;
        private System.Windows.Forms.Button btnDiretorioArquivoLogServiceMail;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}

