using System.Text;

namespace WebAPI.Models;
public class RandomPasswordGenerator
{
    private static Random random = new Random();

    public static string GenerateRandomPassword(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        StringBuilder password = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            int index = random.Next(chars.Length);
            password.Append(chars[index]);
        }

        return password.ToString();
    }
}
/*
so ruft ihr es auf:
string randomPassword = RandomPasswordGenerator.GenerateRandomPassword(6); // 6 ist für die Anzahl der Zeichen.
Console.WriteLine("Generated Password: " + randomPassword);
*/