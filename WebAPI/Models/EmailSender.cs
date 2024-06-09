using System.Net;
using System.Net.Mail;

namespace WebAPI.Models;

public class EmailSender
{
    public static void SendEmail(string to, string subject, string body)
    {
        try
        {
            // E-Mail-Einstellungen konfigurieren
            string smtpServer = "smtp.office365.com"; // SMTP-Server-Adresse
            int port = 587; // Port des SMTP-Servers
            string username = "thesisportalservice@outlook.de"; // Benutzername für den SMTP-Server
            string password = "ImplementierungVonAnwendungssystemen2024?"; // Passwort für den SMTP-Server

            // Erstellen einer neuen SmtpClient-Instanz
            using (SmtpClient client = new SmtpClient(smtpServer, port))
            {
                // Authentifizierung für den SMTP-Server konfigurieren
                client.Credentials = new NetworkCredential(username, password);
                client.EnableSsl = true; // SSL-Verschlüsselung aktivieren, falls erforderlich

                // Erstellen der E-Mail
                MailMessage mail = new MailMessage(username, to, subject, body);
                mail.IsBodyHtml = true; // Falls der E-Mail-Inhalt HTML-Formatierung enthält

                // Senden der E-Mail
                client.Send(mail);

                Console.WriteLine("Die E-Mail wurde erfolgreich gesendet.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fehler beim Senden der E-Mail: " + ex.Message);
        }
    }
}
/*
So ruft man es auf:
string from = "your_email@example.com";
string to = "recipient@example.com";
string subject = "Test-E-Mail";
string body = "Dies ist eine Test-E-Mail.";

EmailSender.SendEmail(from, to, subject, body);

*/