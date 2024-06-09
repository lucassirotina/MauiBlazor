using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using WebMVC.Database.Repository;

namespace WebMVC.Database
{
    public class Session
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string SessionKey { get; set; }

        [ForeignKey("User")]
        public int SessionUserID { get; set; }

        [NotMapped]
        private static IDictionary<string, int> ActiveSessions = new Dictionary<string, int>();

        /// <summary>Returns the Session-Object with the sessionKey. Make sure the Key is Valid before using the function</summary>
        public static Session FromKey(string sessionKey)
        {
            Session s = new Session
            {
                SessionKey = sessionKey,
                SessionUserID = ActiveSessions[sessionKey]
            };
            return s;
        }

        public Session() { }
        public Session(int userID)
        {
            this.SessionUserID = userID;
            this.SessionKey = GenerateSessionKey();
            ActiveSessions[this.SessionKey] = this.SessionUserID;
            using UnitOfWork unitOfWork = new();
            ISessionRepository sr = unitOfWork.SessionRepository;
            sr.CreateSessionInDB(this);
            unitOfWork.Save();
        }

        private static string GenerateSessionKey()
        {
            RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();
            string key;
            do
            {
                byte[] bytes = new byte[128];
                RandomNumberGenerator.GetBytes(bytes);
                key = Convert.ToBase64String(bytes);
            } while (key == null);

            return key;
        }


        public static bool IsValid(string sessionKey)
        {
            return ActiveSessions.ContainsKey(sessionKey);
        }

        public void Delete()
        {
            ActiveSessions.Remove(this.SessionKey);
            using UnitOfWork unitOfWork = new();
            ISessionRepository sr = unitOfWork.SessionRepository;
            sr.DeleteSessionFromDB(this);
            unitOfWork.Save();
        }

        /// <summary>Call once (when the Server starts)</summary>
        public static void InitializeDictionaryFromDB()
        {
            using UnitOfWork unitOfWork = new();
            ISessionRepository sr = unitOfWork.SessionRepository;
            ActiveSessions = sr.LoadSessionsFromDB();
        }
    }

}
