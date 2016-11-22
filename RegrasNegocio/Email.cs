using System.IO;
using System.Net.Mail;
using System.Web;

namespace Artebit.Restaurante.Global.RegrasNegocio
{
    public class Email
    {
        public static void EnviarEmail(MailMessage email)
        {
            //CRIA OBJETO SMTP
            var smtp = new SmtpClient();

            //ENVIA EMAIL
            smtp.Send(email);
        }

        public static void CriarEmail(string msg, string assunto, string email)
        {
            var mail = new MailMessage();
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            mail.Subject = assunto;
            mail.To.Add(new MailAddress(email));

            using (var str = new StreamReader(HttpContext.Current.Server.MapPath("~/LayoutEmails/SolicitaSenha.htm")))
            {
                string corpo = str.ReadToEnd();

                mail.Body = corpo.Replace("{Resposta}", msg);
            }

            EnviarEmail(mail);
        }
    }
}