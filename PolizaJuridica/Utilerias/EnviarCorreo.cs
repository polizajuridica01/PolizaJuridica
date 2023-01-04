using PolizaJuridica.Data;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PolizaJuridica.Utilerias
{
    public class EnviarCorreo
    {
        private readonly PolizaJuridicaDbContext _context;

        public EnviarCorreo(PolizaJuridicaDbContext context)
        {
            _context = context;
        }

        public void EnviarCorreo1()
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress("sistemas@polizajuridica.com.mx", "poliza", System.Text.Encoding.UTF8)
                };
                //mail.To.Add(new MailAddress("jhdzsant@gmail.com"));
                mail.CC.Add(new MailAddress("hdzjulio@hotmail.com"));

                mail.Subject = "Sistema de poliza juridica ";
                var V = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8'/> <title>Sistema</title> <style type='text/css'> body{padding-top: 0 !important; padding-bottom: 0 !important; padding-top: 0 !important; padding-bottom: 0 !important; margin: 0 !important; width: 100% !important; -webkit-text-size-adjust: 100% !important; -ms-text-size-adjust: 100% !important; -webkit-font-smoothing: antialiased !important;}.tableContent img{border: 0 !important; display: block !important; outline: none !important;}a{color: #382F2E;}p, h1{color: #382F2E; margin: 0;}p{text-align: left; color: #999999; font-size: 14px; font-weight: normal; line-height: 19px;}a.link1{color: #382F2E;}a.link2{font-size: 16px; text-decoration: none; color: #ffffff;}h2{text-align: left; color: #222222; font-size: 19px; font-weight: normal;}div, p, ul, h1{margin: 0;}.bgBody{background: #ffffff;}.bgItem{background: #ffffff;}@media only screen and (max-width:480px){table[class='MainContainer'], td[class='cell']{width: 100% !important; height: auto !important;}td[class='specbundle']{width: 100% !important; float: left !important; font-size: 13px !important; line-height: 17px !important; display: block !important; padding-bottom: 15px !important;}td[class='spechide']{display: none !important;}img[class='banner']{width: 100% !important; height: auto !important;}td[class='left_pad']{padding-left: 15px !important; padding-right: 15px !important;}}@media only screen and (max-width:540px){table[class='MainContainer'], td[class='cell']{width: 100% !important; height: auto !important;}td[class='specbundle']{width: 100% !important; float: left !important; font-size: 13px !important; line-height: 17px !important; display: block !important; padding-bottom: 15px !important;}td[class='spechide']{display: none !important;}img[class='banner']{width: 100% !important; height: auto !important;}.font{font-size: 18px !important; line-height: 22px !important;}.font1{font-size: 18px !important; line-height: 22px !important;}}</style> <script type='colorScheme' class='swatch active'>{'name':'Default', 'bgBody':'ffffff', 'link':'382F2E', 'color':'999999', 'bgItem':'ffffff', 'title':'222222'}</script></head><body paddingwidth='0' paddingheight='0' style='padding-top: 0; padding-bottom: 0; padding-top: 0; padding-bottom: 0; background-repeat: repeat; width: 100% !important; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; -webkit-font-smoothing: antialiased;' offset='0' toppadding='0' leftpadding='0'> <table bgcolor='#ffffff' width='100%' border='0' cellspacing='0' cellpadding='0' class='tableContent' align='center' style='font-family:Helvetica, Arial,serif;'> <tbody> <tr> <td> <table width='600' border='0' cellspacing='0' cellpadding='0' align='center' bgcolor='#ffffff' class='MainContainer'> <tbody> <tr> <td> <table width='100%' border='0' cellspacing='0' cellpadding='0'> <tbody> <tr> <td valign='top' width='40'>&nbsp;</td><td> <table width='100%' border='0' cellspacing='0' cellpadding='0'> <tbody> <tr> <td height='75' class='spechide'></td></tr><tr> <td class='movableContentContainer ' valign='top'> <div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'> <table width='100%' border='0' cellspacing='0' cellpadding='0' align='center'> <tr> <td valign='top' align='center'> <div class='contentEditableContainer contentImageEditable'> <div class='contentEditable'> <img src='http://polizajuridica.com.mx/assets/img/Polizalogo.png' width='500' height='150' alt='' data-default='placeholder' data-max-width='1060'> </div></div></td></tr></table> </div><div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'> <table width='100%' border='0' cellspacing='0' cellpadding='0'> <tbody> <tr> <td height='35'></td></tr><tr> <td> <table width='100%' border='0' cellspacing='0' cellpadding='0'> <tbody> <tr> <td valign='top' align='center' class='specbundle'> <div class='contentEditableContainer contentTextEditable'> <div class='contentEditable'> <p style='text-align:center;margin:0;font-family:Georgia,Time,sans-serif;font-size:26px;color:#222222;'><span class='specbundle2'><span class='font1'>Estimado(a)&nbsp;</span></span></p></div></div></td><td valign='top' class='specbundle'> <div class='contentEditableContainer contentTextEditable'> <div class='contentEditable'> <p style='text-align:center;margin:0;font-family:Georgia,Time,sans-serif;font-size:26px;color:#DC2828;'><span class='font'>Gilberto Hernandez</span> </p></div></div></td></tr></tbody> </table> </td></tr></tbody> </table> </div><div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'> <table width='100%' border='0' cellspacing='0' cellpadding='0' align='center'> <tr> <td height='55'></td></tr><tr> <td align='left'> <div class='contentEditableContainer contentTextEditable'> <div class='contentEditable' align='center'> <h2>Le comentamos que su solicitud de otorgamiento ha sido recibida</h2> </div></div></td></tr><tr> <td height='15'> </td></tr><tr> <td align='left'> <div class='contentEditableContainer contentTextEditable'> <div class='contentEditable' align='center'> <p> Por medio del presente correo le comentamos que se ha iniciado el proceso. Recaudar información e investigación, le estaremos enviando un serie de correos para informarle cual es el estatus de su solicitud. <br><br>Cualquier duda no dude en contactarnos y con mucho gusto lo atenderemos. <br><br>Saludos, <br><span style='color:#222222;'>Atentamente: Póliza Jurídica</span> </p></div></div></td></tr><tr> <td height='55'></td></tr><tr> <td height='20'></td></tr></table> </div><div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'> <table width='100%' border='0' cellspacing='0' cellpadding='0'> <tbody> <tr> <td height='65'> </tr><tr> <td style='border-bottom:1px solid #DDDDDD;'></td></tr><tr> <td height='25'></td></tr><tr> <td> <table width='100%' border='0' cellspacing='0' cellpadding='0'> <tbody> <tr> <td valign='top' class='specbundle'> <div class='contentEditableContainer contentTextEditable'> <div class='contentEditable' align='center'> <p style='text-align:left;color:#CCCCCC;font-size:12px;font-weight:normal;line-height:20px;'> <span style='font-weight:bold;'>Póliza Jurídica</span> <br>Convento de Acolman No. 70 Col. Jardines de Santa Monica Atizapán. Edo. de México <br></p></div></div></td><td valign='top' width='30' class='specbundle'>&nbsp;</td><td valign='top' class='specbundle'> <table width='100%' border='0' cellspacing='0' cellpadding='0'> <tbody> <tr> <td valign='top' width='52'> <div class='contentEditableContainer contentFacebookEditable'> <div class='contentEditable'> <a target='_blank' href='#'><img src='http://polizajuridica.com.mx/assets/img/logo_poliza.png' width='52' height='53' alt='facebook icon' data-default='placeholder' data-max-width='52' data-customIcon='true'></a> </div></div></td><td valign='top' width='16'>&nbsp;</td></tr></tbody> </table> </td></tr></tbody> </table> </td></tr><tr> <td height='88'></td></tr></tbody> </table> </div></td></tr></tbody> </table> </td><td valign='top' width='40'>&nbsp;</td></tr></tbody> </table> </td></tr></tbody> </table> </td></tr></tbody> </table></body></html>";
                mail.Body = V;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient("69.49.115.72", 1025))
                {
                    smtp.Credentials = new NetworkCredential("sistemas@polizajuridica.com.mx", "sistemaspoliza1");
                    smtp.EnableSsl = false;
                    smtp.SendMailAsync(mail).GetAwaiter().GetResult();
                }
            }catch(Exception e)
            {
                Log l = new Log
                {
                    LogFecha = DateTime.Now,
                    LogObjetoIn = e.ToString(),
                    LogProceso = "WS Evento Usuarios"
                };

                _context.Add(l);
                _context.SaveChanges();
            }
            
        }

        public void EnviarCorreoSendGrid(EventoUsuarios eu, Eventos e)
        {
            string BODY = "<table class='tableContent' style='font-family: Helvetica, Arial,serif;' border='0' width='100%' cellspacing='0' cellpadding='0' align='center' bgcolor='#ffffff'><tbody><tr><td><table class='MainContainer' border='0' width='600' cellspacing='0' cellpadding='0' align='center' bgcolor='#ffffff'><tbody><tr><td><table border='0' width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td valign='top' width='40'>&nbsp;</td><td><table border='0' width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td class='spechide' height='75'>&nbsp;</td></tr><tr><td class='movableContentContainer ' valign='top'><div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'><tbody><tr><td align='center' valign='top'><div class='contentEditableContainer contentImageEditable'><div class='contentEditable'><img src='http://polizajuridica.com.mx/assets/img/Polizalogo.png' alt='' width='500' height='150' data-default='placeholder' data-max-width='1060'/></div></div></td></tr></tbody></table></div><div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'><table border='0' width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td height='35'>&nbsp;</td></tr><tr><td><table border='0' width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td class='specbundle' align='center' valign='top'><div class='contentEditableContainer contentTextEditable'><div class='contentEditable'><p style='text-align: center; margin: 0; font-family: Georgia,Time,sans-serif; font-size: 26px; color: #222222;'><span class='specbundle2'><span class='font1'>Estimado(a)&nbsp;</span></span></p></div></div></td><td class='specbundle' valign='top'><div class='contentEditableContainer contentTextEditable'><div class='contentEditable'><p style='text-align: center; margin: 0; font-family: Georgia,Time,sans-serif; font-size: 26px; color: #dc2828;'><span class='font'>" + eu.Nombre.Trim() + ' ' + eu.ApellidoPaterno.Trim() + "</span></p></div></div></td></tr></tbody></table></td></tr></tbody></table></div><div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'><table border='0' width='100%' cellspacing='0' cellpadding='0' align='center'><tbody><tr><td height='55'><p>&nbsp;</p><h4><strong>&nbsp;No te pierdas nuestro evento:</strong></h4><p><strong>T&iacute;tulo: </strong>"+e.Titulo.Trim()+"</p><p>&nbsp;</p><p><strong>Descripci&oacute;n: </strong>"+e.Descripcion.Trim()+"</p><p>&nbsp;</p><p><strong>Horario: </strong>"+e.FechaHoraInicio.ToString("dd/mm/aa hh:mm")+ " A " + e.FechaHoraFin.ToString("dd/mm/aa hh:mm") +" </p><p>&nbsp;</p><p>&nbsp;</p></td></tr><tr><td align='left'><div class='contentEditableContainer contentTextEditable'><div class='contentEditable' align='center'><h2>Le compartimos los datos para conectarse a nuestra pl&aacute;tica : " + "Id de reunión: " + e.ReunionIde.Trim() + " Contraseña: " + e.ReunionPass.Trim() + " Url: " + e.ReunionUr.Trim() +" </h2></div></div></td></tr><tr><td height='15'>&nbsp;</td></tr><tr><td align='left'><div class='contentEditableContainer contentTextEditable'><div class='contentEditable' align='center'><br/><br/>Cualquier duda no dude en contactarnos y con mucho gusto lo atenderemos. <br/><br/>Saludos, <br/><span style='color: #222222;'>Atentamente: P&oacute;liza Jur&iacute;dica</span></div></div></td></tr><tr><td height='55'>&nbsp;</td></tr><tr><td height='20'>&nbsp;</td></tr></tbody></table></div><div class='movableContent' style='border: 0px; padding-top: 0px; position: relative;'><table border='0' width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td height='65'>&nbsp;</td></tr><tr><td style='border-bottom: 1px solid #DDDDDD;'>&nbsp;</td></tr><tr><td height='25'>&nbsp;</td></tr><tr><td><table border='0' width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td class='specbundle' valign='top'><div class='contentEditableContainer contentTextEditable'><div class='contentEditable' align='center'><p style='text-align: left; color: #cccccc; font-size: 12px; font-weight: normal; line-height: 20px;'><span style='font-weight: bold;'>P&oacute;liza Jur&iacute;dica</span> <br/>Convento de Acolman No. 70 Col. Jardines de Santa Monica Atizap&aacute;n. Edo. de M&eacute;xico </p></div></div></td><td class='specbundle' valign='top' width='30'>&nbsp;</td><td class='specbundle' valign='top'><table border='0' width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td valign='top' width='52'><div class='contentEditableContainer contentFacebookEditable'><div class='contentEditable'><a href='#' target='_blank' rel='noopener'><img src='http://polizajuridica.com.mx/assets/img/logo_poliza.png' alt='facebook icon' width='52' height='53' data-default='placeholder' data-max-width='52' data-customicon='true'/></a></div></div></td><td valign='top' width='16'>&nbsp;</td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td height='88'>&nbsp;</td></tr></tbody></table></div></td></tr></tbody></table></td><td valign='top' width='40'>&nbsp;</td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>";
            try
            {
                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("sistemas@polizajuridica.com.mx", "Póliza Jurídica");
                var subject = "Datos Plática";
                var to = new EmailAddress(eu.Correo.Trim(), eu.Nombre.Trim() + " " + eu.ApellidoPaterno.Trim());
                var plainTextContent = " ";
                var htmlContent = BODY;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = client.SendEmailAsync(msg);
                Log l = new Log
                {
                    LogFecha = DateTime.Now,
                    LogObjetoIn = response.Result.ToString()
                };

                _context.Log.Add(l);
                _context.SaveChanges();

            }
            catch (Exception xe)
            {
                Log l = new Log
                {
                    LogFecha = DateTime.Now,
                    LogObjetoIn = xe.ToString()
                };

                _context.Log.Add(l);
                _context.SaveChanges();
            }
        }

        public void EnviarCorreoSendGridMasivo(EventoUsuarios eu,string mensaje)
        {
            string BODY = mensaje;
            try
            {
                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("sistemas@polizajuridica.com.mx", "Póliza Jurídica");
                var subject = "Datos Plática";
                var to = new EmailAddress(eu.Correo.Trim(), eu.Nombre.Trim() + " " + eu.ApellidoPaterno.Trim());
                var plainTextContent = " ";
                var htmlContent = BODY;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = client.SendEmailAsync(msg);
                Log l = new Log
                {
                    LogFecha = DateTime.Now,
                    LogObjetoIn = response.Result.ToString()
                };

                _context.Log.Add(l);
                _context.SaveChanges();

            }
            catch (Exception xe)
            {
                Log l = new Log
                {
                    LogFecha = DateTime.Now,
                    LogObjetoIn = xe.ToString()
                };

                _context.Log.Add(l);
                _context.SaveChanges();
            }
        }
    }
}
