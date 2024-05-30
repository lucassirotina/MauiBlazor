namespace WebAPI.Database.Repository
{
       public interface ILehrenderRepository
        {
            public User GetUserById(int id);
            public void CreateSupervisor(Supervisor supervisor);
            public void DeleteUser(User user);
            public IDictionary<string, string> LoadUserFromDB();
            public List<User> GetAllUser();
            public User GetUserByName(string userName);
            public bool UserExists(string userName);
        }
        public class LehrenderRepository : ILehrenderRepository
        {


            private DataContext context;

            public LehrenderRepository(DataContext context)
            {
                this.context = context;
            }
            public User GetUserById(int id)
            {
                return context.Users.Find(id);
            }

            public User GetUserByName(string userName)
            {
                return context.Users.FirstOrDefault(x => x.UserName == userName);
            }

            public bool UserExists(string userName)
            {
                return context.Users.Any(x => x.UserName == userName);
            }

            public void CreateSupervisor(Supervisor user)
            {
                context.Supervisors.Add(user);
            }

            public void DeleteUser(User user)
            {
                context.Users.Remove(user);
            }

            public List<User> GetAllUser()
            {
                return context.Users.ToList();
            }

            public IDictionary<string, string> LoadUserFromDB()
            {
                IDictionary<string, string> User = new Dictionary<string, string>();
                foreach (User user in context.Users)
                {
                    User[user.UserName] = user.Password;
                }
                return User;
            }

        }
    }
