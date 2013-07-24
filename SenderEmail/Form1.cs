using System;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace SenderEmail
{
    public partial class Form1 : Form
    {
        private bool adj = false;
        private string File;
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnSendClick(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();
            
            mail.From = new MailAddress(txtFrom.Text);

            mail.To.Add(txtFor.Text);
            mail.Subject = txtSubject.Text;
            mail.Body = txtMessage.Text;
            mail.IsBodyHtml = false;
            mail.Priority = MailPriority.Normal;

            if (adj == true)
            {
                Attachment attach = new Attachment(@File);
                mail.Attachments.Add(attach);
                adj = false;
            }

            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential(txtFrom.Text, txtPassword.Text);

                smtp.Host = "smtp.live.com";
                smtp.Port = 25;

                //smtp.Port = 995;  pop3.live.com --- Hotmail
                //smtp.Port = 587; pop.gmail.com --- Gmail
                smtp.EnableSsl = true;

                smtp.Send(mail);
                MessageBox.Show("Mail Sent");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send mail", ex.Message);
            }
                mail.Dispose();
                Clean();
                txtFrom.Focus();
        }
        
        private void BtnInsertClick(object sender, EventArgs e)
        {
            adj = true;
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Choose File";
            file.InitialDirectory = @"c:\";
            file.Filter = "All files(*.*)|*.*";
            file.FilterIndex = 1;
            file.RestoreDirectory = true;
            file.ShowDialog();
            File = file.FileName;
        }

        public void Clean()
        {
            txtFor.Text = string.Empty;
            txtFrom.Text = string.Empty;
            txtMessage.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtSubject.Text = string.Empty;
        }
    }
}
