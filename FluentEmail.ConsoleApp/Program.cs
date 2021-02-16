using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FluentEmail.ConsoleApp
{
	public class EmailModel
	{
		public string Message { get; set; }
	}

	class Program
	{
		static void Main(string[] args)
		{
			// SendWithFluentEmail();
			SendWithSmtpClient();
		}

		private static void SendWithFluentEmail()
		{
			var smtpClient = new SmtpClient("smtp.croc.ru", 25);

			smtpClient.UseDefaultCredentials = true;

			Email.DefaultSender = new SmtpSender(smtpClient);
			Email.DefaultRenderer = new RazorRenderer();

			var template = @"<html><body><h5>@Model.Message</h5></body></html>";

			var email = Email
				.From("isungurov@croc.ru")
				.To("isungurov@croc.ru")
				.Subject("Testing FluentEmail")
				.UsingTemplate<EmailModel>(template, new EmailModel { Message = "Hello FluentEmail!" }, isHtml: true);

			email.Send();

			Console.WriteLine("Fluent email sent");
			Console.ReadKey();
		}

		private static void SendWithSmtpClient()
		{
			var client = new SmtpClient("smtp.croc.ru", 25);

			client.UseDefaultCredentials = true;

			var mail = new MailMessage("isungurov@croc.ru", "isungurov@croc.ru", "Smtp Test", "Hello FluentEmail");

			// client.SendAsync(mail, null);
			client.Send(mail);

			Console.WriteLine("SmtpClient email sent");
			Console.ReadKey();
		}
	}
}
