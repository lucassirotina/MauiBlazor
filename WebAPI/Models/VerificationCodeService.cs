namespace WebAPI.Models;

public class VerificationCodeService
{
    // Dictionary zum Speichern der Zeitpunkte der Codeerstellung für jeden Benutzer
    private static Dictionary<int, (string code, DateTime creationTime)> _verificationCodes = new Dictionary<int, (string, DateTime)>();

    // Methode zum Generieren und Speichern des Bestätigungscodes
    public static string GenerateVerificationCode(int userId)
    {
        string code = RandomPasswordGenerator.GenerateRandomPassword(6);
        _verificationCodes[userId] = (code, DateTime.Now);
        return code;
    }

    // Methode zum Überprüfen der Gültigkeit des Bestätigungscode
    public static bool IsVerificationCodeValid(int userId, string verificationCode)
    {
        if (_verificationCodes.ContainsKey(userId))
        {
            var (code, creationTime) = _verificationCodes[userId];
            var validityPeriod = TimeSpan.FromMinutes(10); // Gültigkeitsdauer: 10 Minuten
            if (DateTime.Now - creationTime <= validityPeriod && code == verificationCode)
            {
                return true; 
            }
            else
            {
                // Entfernen des abgelaufenen Codes aus dem Dictionary
                _verificationCodes.Remove(userId); 
            }
        }
        return false;
    }
}