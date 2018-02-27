using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Windows;

namespace Property_Management_System
{
    class Email
    {
        /// <summary>
        /// Creates and sends an email.
        /// </summary>
        /// <param name="Recipient">The email of the receiver of the email.</param>
        /// <param name="Subject">The subject of the email.</param>
        /// <param name="Body">The main content of the email.</param>
        public static void SendMail(string Recipient, string Subject, string Body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                //Setup variables for the email as we are using ssl we need a password as well as an email. This is defined in the user settings.
                SmtpClient Client = new SmtpClient(Properties.Settings.Default.Email_Host);
                mail.From = new MailAddress(Properties.Settings.Default.Email_Sender);
                mail.To.Add(Recipient);
                mail.Subject = Subject;
                mail.IsBodyHtml = false;
                mail.Body = Body;
                Client.Port = int.Parse(Properties.Settings.Default.Email_Port);
                Client.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.Email_Sender, Properties.Settings.Default.Email_Password);
                Client.EnableSsl = true;
                Client.Send(mail);
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[Email:SendMail] Mail sent to " + Recipient + " from " + Properties.Settings.Default.Email_Sender);
                }
                MessageBox.Show("Mail sent!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                //The email failed to send so we handle the error and alert the user.
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[Email:SendMail] Mail sending to " + Recipient + " from " + Properties.Settings.Default.Email_Sender + " failed.");
                    Log.Commit("[Email:SendMail] Mail sending failed: " + e.Message);
                }
                MessageBox.Show("Mail failed to send.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
