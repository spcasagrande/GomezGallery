using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GomezGallery.Domain.Abstract;
using GomezGallery.Domain.Entities;

namespace GomezGallery.Domain.Concrete
{
	// email settings
	public class EmailSettings
	{
		public string MailToAddress = "orders@example.com";
		public string MailFromAddress = "gomezgallery@example.com";
		public bool UseSsl = true;
		public string Username = "MySmtpUsername";
		public string Password = "MySmtpPassword";
		public string ServerName = "smtp.example.com";
		public int ServerPort = 587;
		public bool WriteAsFile = true;
		public string FileLocation = @"c:\gomez_gallery_email";
	}

	public class EmailOrderProcessor : IOrderProcessor
	{
		private EmailSettings emailSettings;

		public EmailOrderProcessor(EmailSettings settings)
		{
			emailSettings = settings;
		}
		public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
		{
			using (var smtpClient = new SmtpClient())
			{
				// set smtp configs
				smtpClient.EnableSsl = emailSettings.UseSsl;
				smtpClient.Host = emailSettings.ServerName;
				smtpClient.Port = emailSettings.ServerPort;
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

				if (emailSettings.WriteAsFile)
				{
					smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
					smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
					smtpClient.EnableSsl = false;
				}

				// create email body
				StringBuilder body = new StringBuilder()
					.AppendLine("A new order has been submitted")
					.AppendLine("==============================")
					.AppendLine("Items: ");

				foreach (var line in cart.Lines)
				{
					var subtotal = line.Picture.Price * line.Quantity;
					body.AppendFormat("{0} x {1} (subtotal: {2:c})",
										line.Quantity,
										line.Picture.Name,
										subtotal);
				}

				body.AppendFormat("\nTotal order value: {0:c}", cart.ComputeTotalValue())
					.AppendLine("\n========================")
					.AppendLine("\nShip to: ")
					.AppendLine(shippingInfo.Name)
					.AppendLine(shippingInfo.Line1)
					.AppendFormat(shippingInfo.Line2 ?? "")
					.AppendFormat(shippingInfo.Line3 ?? "")
					.AppendLine(shippingInfo.City)
					.AppendLine(shippingInfo.State)
					.AppendLine(shippingInfo.Zip ?? "")
					.AppendLine(shippingInfo.Country)
					.AppendLine("========================")
					.AppendFormat("Gift Wrap: {0}", shippingInfo.GiftWrap ? "Yes" : "No");

				// create mailMessage
				MailMessage mailMessage = new MailMessage(
					emailSettings.MailFromAddress,
					emailSettings.MailToAddress,
					"New order submitted!",
					body.ToString());

				if (emailSettings.WriteAsFile)
				{
					mailMessage.BodyEncoding = Encoding.ASCII;
				}

				// send the email
				smtpClient.Send(mailMessage);
			}
		}
	}
}
