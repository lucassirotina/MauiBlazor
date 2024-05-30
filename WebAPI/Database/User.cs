using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace WebAPI.Database
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool AdminRights { get; set; }
        public string Email { get; set; }
        public string Rolle {  get; set; }
        public string Salt { get; set; }
        public User() { }
        public User(string userName, int userId, string password, string email, string rolle)
        {
            UserName = userName;
            UserId = userId;
            Salt = GenerateSalt();
            Password = Hash(password, Salt);
            AdminRights = false;
            UserList[UserName] = Password;
            Email = email;
            Rolle = rolle;
            
        }

        [NotMapped]
        private readonly static IDictionary<string, string> UserList = new Dictionary<string, string>();
        private static string GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        // PBKDF2 Hash-Algorithmus
        public static string Hash(string password, string salt)
        {
            using (var algorithm = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 500000))
            {
                var hash = algorithm.GetBytes(256 / 8);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
