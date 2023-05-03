using Microsoft.VisualBasic;
using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace EnviarEmail
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private String meuEmail =  "igoxr2@gmail.com";
        private String minhaSenhaEmail = string.Empty;
        public void SendEmail()
        {
            try
            {
                // Prepara campos da mensagem para o envio do email
                MailMessage mensagem = getPrepararMensagem();

                // Mostra na tela um inputbox para o usuario informar a senha do gmail 
                doInformarSenhaEmail();

                // Mostrar o WaitForm enquanto o sistema inicia e efetica o processo de enviar o email
                splashScreenManager1.ShowWaitForm();

                // Necessario desativar a opçao permitir que aplicativos menos seguros acessem contas 
                // http://www.google.com.br/settings/security/lesssecureapps do gmail
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true; // google usa essa propriedade
                NetworkCredential cred = new NetworkCredential(meuEmail, minhaSenhaEmail); // email e senha do rementente
                smtp.Credentials = cred;

                // Envia o email
                smtp.Send(mensagem);

                // Fecha o Wait form
                splashScreenManager1.CloseWaitForm();
                MessageBox.Show("Email Enviado");
            }
            catch (Exception ex)
            {
                splashScreenManager1.CloseWaitForm();
                if (!String.IsNullOrEmpty(ex.Message))
                    MessageBox.Show(ex.Message);
            }
        }

        private void doInformarSenhaEmail()
        {
            /* usando a função VB.Net para exibir um prompt para o usuário informar a senha */
            string Prompt = "Insira a senha: ";
            string Titulo = "Senha do email " + meuEmail;
            string Resultado = Interaction.InputBox(Prompt, Titulo, "*", 150, 150);

            if (String.IsNullOrEmpty(Resultado))
                throw new Exception("");
            else
                minhaSenhaEmail = Resultado;
        }

        private MailMessage getPrepararMensagem()
        {
            MailMessage mensagem = new MailMessage();

            if (!String.IsNullOrEmpty(tbPara.Text))
            {
                mensagem.To.Add(tbPara.Text);
            }
            else
            {
                throw new Exception("Informe o destinatário");
            }

            mensagem.Subject = String.IsNullOrEmpty(tbAssunto.Text) ? "" : tbAssunto.Text;

            mensagem.From = new MailAddress(meuEmail);

            mensagem.Body = String.IsNullOrEmpty(tbMensagem.Text) ? "" : tbMensagem.Text;

            if (!String.IsNullOrEmpty(tbCC.Text))
                mensagem.CC.Add(tbCC.Text);

            if (!String.IsNullOrEmpty(txtAttachmentPath.Text))
            {
                Attachment anexo = new Attachment(txtAttachmentPath.Text);                
                mensagem.Attachments.Add(anexo);
            }

            return mensagem;
        }

        private void btnAnexo_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            txtAttachmentPath.Text = openFileDialog1.FileName;
        }

        private void btEmail_Click(object sender, EventArgs e)
        {
            SendEmail();
        }
    }
}
