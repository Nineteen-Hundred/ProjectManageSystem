using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

/// <summary>
///sendmail 的摘要说明
/// </summary>
///
namespace sendmail
{
    /* public class sendmailclass
    {
		
        public bool sendmailfunction(string address,string content,string title)
        {	
            SmtpClient client = new SmtpClient();  
			client.Host = "smtp.163.com";  
			//client.UseDefaultCredentials = false;
			client.Port = 465;
			client.Credentials = new System.Net.NetworkCredential("yicloudpro@163.com", "xxxx");  
			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.EnableSsl = true;
			System.Net.Mail.MailMessage message = new MailMessage("yicloudpro@163.com", "xxxx");  
			message.Subject = title;  
			message.Body = content;  
			message.SubjectEncoding = System.Text.Encoding.UTF8;  
			message.BodyEncoding = System.Text.Encoding.UTF8;  
			message.IsBodyHtml = true;  
			message.Priority = MailPriority.High;  
			message.IsBodyHtml = true;  

			client.Send(message);
			
			return true;
		}
    } */
	public class sendmailclass
	{
		public int sendmailfunction(string address,string content,string title)
		{
			System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
			mail.To = address;
			mail.From = "yicloudpro@163.com";
			mail.Subject = title;
			mail.BodyFormat = System.Web.Mail.MailFormat.Html;
			mail.Body = content;
			mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication
			mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "xxxx"); //set your username here
			mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "xxxx"); //set your password here
			mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 465);//set port
			mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");//set is ssl               
			System.Web.Mail.SmtpMail.SmtpServer = "smtp.163.com";
			System.Web.Mail.SmtpMail.Send(mail);

			return 0;
		}
	}
}