using eindwerk.DB;
using eindwerk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eindwerk.Models;
using eindwerk.Encryption;
using System.Windows;
using eindwerk.Services;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;

namespace eindwerk.Commands
{
    public class AddAccountCommand : CommandBase
    {
        public readonly AddAccountViewModel _viewModal;
        public readonly INavigationService _navigationService;
        private static string password; 

        public AddAccountCommand(AddAccountViewModel viewModal, INavigationService navigationService)
        {
            _viewModal = viewModal;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            if (_viewModal.Name == "" || _viewModal.Email == "" || _viewModal.ChosenClass == null || _viewModal.Chosenpermission == null)
            {
                MessageBox.Show("Wrong inputs, please check if everything is filled in correctly", "Wrong Inputs", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Regex regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                        + "@"
                        + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z");
            if (!regex.IsMatch(_viewModal.Email))
            {
                MessageBox.Show("Non Valid Email has been used please check if the email has the corect format", "Wrong Inputs", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var DB = new Database())
            {
                Account? account = DB.Accounts.AsQueryable().Where(e => e.Email == _viewModal.Email).FirstOrDefault();
                if (account != null)
                {
                    MessageBox.Show("Email already in the sytem please check if its correct or use another one", "Wrong Inputs", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                password = Hashing.PasswordGenerator();

                Class? chosenClass = DB.Class.AsQueryable().Where(i => i.Id == _viewModal.ChosenClass.Id).FirstOrDefault();
                Permission? Chosenpermission = DB.Permissions.AsQueryable().Where(i => i.Id == _viewModal.Chosenpermission.Id).FirstOrDefault();
                var Account = new Account()
                {
                    UserName = _viewModal.Name,
                    Email = _viewModal.Email,
                    Class = chosenClass,
                    Permission = Chosenpermission,
                    Password = Hashing.hash(password)
                };
                DB.Add(Account);
                DB.SaveChanges();
            }


            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("guldensporenstudyapp@outlook.com");
                message.To.Add(new MailAddress(_viewModal.Email));
                message.Subject = "Account Info";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString(_viewModal.Email, password);

                Attachment item = Attachment.CreateAttachmentFromString("https://www.guldensporencollege.be/themes/custom/barriocustom/logo.png", "logo.png");
                message.Attachments.Add(item);
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp-mail.outlook.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("guldensporenstudyapp@outlook.com", "A123B456C");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Email failed to send");
            }
            _navigationService.Navigate();
        }
        //ctrl k + u to uncomment
        private string htmlString(string email, string password)
        {
          string html = @"<!DOCTYPE HTML PUBLIC \"" -//W3C//DTD HTML 4.0 Transitional//EN\""

< html lang=""en"" xmlns:o=""urn:schemas-microsoft-com:office:office"" xmlns:v=""urn:schemas-microsoft-com:vml"">
<head>
<title></title>
<meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type""/>
<meta content=""width=device-width, initial-scale=1.0"" name=""viewport""/>
<!--[if mso]><xml><o:OfficeDocumentSettings><o:PixelsPerInch>96</o:PixelsPerInch><o:AllowPNG/></o:OfficeDocumentSettings></xml><![endif]-->
<!--[if !mso]><!-->
<link href=""https://fonts.googleapis.com/css?family=Abril+Fatface"" rel=""stylesheet"" type=""text/css""/>
<link href=""https://fonts.googleapis.com/css?family=Alegreya"" rel=""stylesheet"" type=""text/css""/>
<link href=""https://fonts.googleapis.com/css?family=Arvo"" rel=""stylesheet"" type=""text/css""/>
<link href=""https://fonts.googleapis.com/css?family=Bitter"" rel=""stylesheet"" type=""text/css""/>
<link href=""https://fonts.googleapis.com/css?family=Cabin"" rel=""stylesheet"" type=""text/css""/>
<link href=""https://fonts.googleapis.com/css?family=Ubuntu"" rel=""stylesheet"" type=""text/css""/>
<!--<![endif]-->
<style>
		* {
			box-sizing: border-box;
		}

		body {
			margin: 0;
			padding: 0;
		}

		a[x-apple-data-detectors] {
			color: inherit !important;
			text-decoration: inherit !important;
		}

		#MessageViewBody a {
			color: inherit;
			text-decoration: none;
		}

		p {
			line-height: inherit
		}

		.desktop_hide,
		.desktop_hide table {
			mso-hide: all;
			display: none;
			max-height: 0px;
			overflow: hidden;
		}

		@media (max-width:520px) {
			.desktop_hide table.icons-inner {
				display: inline-block !important;
			}

			.icons-inner {
				text-align: center;
			}

			.icons-inner td {
				margin: 0 auto;
			}

			.row-content {
				width: 100% !important;
			}

			.column .border,
			.mobile_hide {
				display: none;
			}

			table {
				table-layout: fixed !important;
			}

			.stack .column {
				width: 100%;
				display: block;
			}

			.mobile_hide {
				min-height: 0;
				max-height: 0;
				max-width: 0;
				overflow: hidden;
				font-size: 0px;
			}

			.desktop_hide,
			.desktop_hide table {
				display: table !important;
				max-height: none !important;
			}
		}
	</style>
</head>
<body style=""background-color: #FFFFFF; margin: 0; padding: 0; -webkit-text-size-adjust: none; text-size-adjust: none;"">
<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""nl-container"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #FFFFFF;"" width=""100%"">
<tbody>
<tr>
<td>
<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-1"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #f5f5f5;"" width=""100%"">
<tbody>
<tr>
<td>
<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row-content stack"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; color: #000000; width: 500px;"" width=""500"">
<tbody>
<tr>
<td class=""column column-1"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 0px; padding-bottom: 5px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;"" width=""100%"">
<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""image_block"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
<tr>
<td style=""padding-bottom:10px;width:100%;padding-right:0px;padding-left:0px;"">
<div align=""center"" style=""line-height:10px""><img alt=""logo"" src=""https://www.guldensporencollege.be/themes/custom/barriocustom/logo.png"" style=""display: block; height: auto; border: 0; width: 125px; max-width: 100%;"" title=""your-logo"" width=""125""/></div>
</td>
</tr>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-2"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #f5f5f5;"" width=""100%"">
<tbody>
<tr>
<td>
<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row-content stack"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #ffffff; color: #000000; width: 500px;"" width=""500"">
<tbody>
<tr>
<td class=""column column-1"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 15px; padding-bottom: 20px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;"" width=""100%"">
<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""heading_block"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
<tr>
<td style=""text-align:center;width:100%;"">
<h1 style=""margin: 0; color: #393d47; direction: ltr; font-family: Tahoma, Verdana, Segoe, sans-serif; font-size: 25px; font-weight: 400; letter-spacing: normal; line-height: 120%; text-align: center; margin-top: 0; margin-bottom: 0;""><span class=""tinyMce-placeholder"">New Account info</span></h1>
</td>
</tr>
</table>
<table border=""0"" cellpadding=""10"" cellspacing=""0"" class=""text_block"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;"" width=""100%"">
<tr>
<td>
<div style=""font-family: Tahoma, Verdana, sans-serif"">
<div class=""txtTinyMce-wrapper"" style=""font-size: 12px; font-family: Tahoma, Verdana, Segoe, sans-serif; mso-line-height-alt: 18px; color: #393d47; line-height: 1.5;"">
<p style=""margin: 0; font-size: 14px; text-align: center; mso-line-height-alt: 21px;""><span style=""font-size:14px;""><span style="""">Email  ="+$" {email}"+ @"</span></span></p>
<p style=""margin: 0; font-size: 14px; text-align: center; mso-line-height-alt: 21px;""><span style=""font-size:14px;""><span style="""">Password = " + $"{password}" + @"</span></span></p>
</div>
</div>
</td>
</tr>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-3"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #fff;"" width=""100%"">
<tbody>
<tr>
<td>
<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row-content stack"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; color: #000000; width: 500px;"" width=""500"">
<tbody>
<tr>
<td class=""column column-1"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;"" width=""100%"">
<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""html_block"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word; word-wrap: break-word;"" width=""100%"">
<tr>
<td>
<div align=""center"" style=""font-family:Arial, Helvetica Neue, Helvetica, sans-serif;text-align:center;""><div style=""height:30px;""> </div></div>
</td>
</tr>
</table>
<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""html_block"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word; word-wrap: break-word;"" width=""100%"">
<tr>
<td>
<div align=""center"" style=""font-family:Arial, Helvetica Neue, Helvetica, sans-serif;text-align:center;""><div style=""margin-top: 25px;border-top:1px dashed #D6D6D6;margin-bottom: 20px;""></div></div>
</td>
</tr>
</table>
<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""html_block"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word; word-wrap: break-word;"" width=""100%"">
<tr>
<td>
<div align=""center"" style=""font-family:Arial, Helvetica Neue, Helvetica, sans-serif;text-align:center;""><div style=""height-top: 20px;""> </div></div>
</td>
</tr>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row row-4"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
<tbody>
<tr>
<td>
<table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""row-content stack"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; color: #000000; width: 500px;"" width=""500"">
<tbody>
<tr>
<td class=""column column-1"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;"" width=""100%"">
<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""icons_block"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
<tr>
<td style=""vertical-align: middle; color: #9d9d9d; font-family: inherit; font-size: 15px; padding-bottom: 5px; padding-top: 5px; text-align: center;"">
<table cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" width=""100%"">
<tr>
<td style=""vertical-align: middle; text-align: center;"">
<!--[if vml]><table align=""left"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""display:inline-block;padding-left:0px;padding-right:0px;mso-table-lspace: 0pt;mso-table-rspace: 0pt;""><![endif]-->
<!--[if !vml]><!-->
</td>
</tr>
</table>
</td>
</tr>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table><!-- End -->
</body>
</html>";
            return html.ToString();
           
        }
    }
}

