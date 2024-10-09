using System.Net;
using System.Net.Mail;
using MVC.G02.DAL.Models;

namespace MVC.G02.PL.Helper
{
    public static class EmailSetting
    {
        public static void Sendemail(Email email)
        {
            //Mail server :gmail.com
            //SMTP
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = 
                new NetworkCredential("mohamedelbordiny33@gmail.com", "jbomsfwlfuqauvaq");
            client.Send("mohamedelbordiny33@gmail.com", email.To, email.Subject, email.Body);
		}
	}
}
