namespace WebAPI.Database.Repository
{
    public interface ISessionRepository
    {
        Session? GetSessionByKey(string key);
        void CreateSessionInDB(Session s);
        void DeleteSessionFromDB(Session s);
        IDictionary<string, int> LoadSessionsFromDB();

    }

    public class SessionRepository : ISessionRepository
    {
        private DataContext context;

        public SessionRepository(DataContext context)
        {
            this.context = context;
        }

        public Session? GetSessionByKey(string key)
        {
            return context.Sessions.Find(key);
        }

        public void CreateSessionInDB(Session s)
        {
            context.Sessions.Add(s);
        }

        public void DeleteSessionFromDB(Session s)
        {
            context.Sessions.Remove(s);
        }

        public IDictionary<string, int> LoadSessionsFromDB()
        {
            IDictionary<string, int> Sessions = new Dictionary<string, int>();
            foreach (Session s in context.Sessions)
            {
                Sessions[s.SessionKey] = s.SessionUserID;
            }
            return Sessions;
        }
    }
}
