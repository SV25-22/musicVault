using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MusicVault.Backend.Common;
    public  class Email {
        private static Email? _instance;
        private readonly string senderEmail = "sasa2003s@gmail.com";
        private readonly string senderPassword = "hslz vdyj yzpe sauc";
        public static string urednikEmail = "usi379538@gmail.com";
        public static string adminEmail = "uvoduvod1@gmail.com";

        private Email() { }
        public static Email Instance {
            get {
                _instance ??= new Email();
                return _instance;
            }
        }
        public bool SendEmail(string recipient, string subject, string body, string? attachmentPath = null) {
            string smtpAddress = "smtp.gmail.com";
            int portNumber = 587;
            bool enableSSL = true;

            try {
                using (MailMessage mail = new()) {
                    mail.From = new MailAddress(senderEmail);
                    mail.To.Add(recipient);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    if (attachmentPath != null) {
                        Attachment attachment = new(attachmentPath);
                        mail.Attachments.Add(attachment);
                    }


                    using (SmtpClient smtp = new(smtpAddress, portNumber)) {
                        smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

                return true;
            } catch (Exception) {
                return false;
            }
        }
    public bool MejlRegistracije(string ime, string prezime) {
        string title = EmailPatterns.InsertPatternParamaters(EmailPatterns.registrationTitle, ime, prezime);
        string body = EmailPatterns.InsertPatternParamaters(EmailPatterns.registrationBody, ime, prezime);
        return SendEmail(adminEmail, title, body);
    }
    public bool MejlPrijave(string ime, string prezime) {
        string title = EmailPatterns.InsertPatternParamaters(EmailPatterns.loginTitle, ime, prezime);
        string body = EmailPatterns.InsertPatternParamaters(EmailPatterns.loginBody, ime, prezime);
        ;
        body = body.Replace("{vreme}", DateTime.Now.ToString());
        return SendEmail(adminEmail, title, body);
    }
    public bool MejlDodeleRecenzije(string ime, string prezime, string id) {
        string title = EmailPatterns.InsertPatternParamaters(EmailPatterns.newReviewTitle, ime, prezime);
        string body = EmailPatterns.InsertPatternParamaters(EmailPatterns.newReviewBody, ime, prezime);
        body = body.Replace("{id}", id);
        return SendEmail(urednikEmail, title, body);
    }
    public bool MejlOduzimanjaRecenzije(string ime, string prezime, string id) {
        string title = EmailPatterns.InsertPatternParamaters(EmailPatterns.takenReviewTitle, ime, prezime);
        string body = EmailPatterns.InsertPatternParamaters(EmailPatterns.takenReviewBody, ime, prezime);
        body = body.Replace("{id}", id);
        return SendEmail(urednikEmail, title, body);
    }
    public bool MejlObjaveRecenzije(string ime, string prezime, string id) {
        string title = EmailPatterns.InsertPatternParamaters(EmailPatterns.publishedReviewTitle, ime, prezime);
        string body = EmailPatterns.InsertPatternParamaters(EmailPatterns.publishedReviewBody, ime, prezime);
        body = body.Replace("{id}", id);
        return SendEmail(urednikEmail, title, body);
    }
    public bool MejlOvereRecenzije(string id) {
        string title = EmailPatterns.reviewReviewTitle;
        string body = EmailPatterns.reviewReviewBody;
        body = body.Replace("{id}", id);
        return SendEmail(adminEmail, title, body);
    }
    public bool MejlOdbijanjaRecenzije(string ime, string prezime, string id, string poruka) {
        string title = EmailPatterns.InsertPatternParamaters(EmailPatterns.refuseReviewTitle, ime, prezime);
        string body = EmailPatterns.InsertPatternParamaters(EmailPatterns.refuseReviewBody, ime, prezime);
        body = body.Replace("{id}", id);
        body = body.Replace("{poruka}", poruka);
        return SendEmail(urednikEmail, title, body);
    }
}