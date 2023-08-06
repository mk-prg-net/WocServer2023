using System.Collections.Concurrent;

namespace TryOut.MySingeltons
{
    /// <summary>
    /// Benutzerverwaltung für Demozwecke
    /// </summary>
    public class MyUserStore
    {
        /// <summary>
        /// Einfaches Benutzerobjekt
        /// </summary>
        public class MyUser 
        { 
            public MyUser(string UserName, string PassWord) 
            {
                this.UserName = UserName;
                this.Password = PassWord;
            }

            public string UserName { get; set; }
            public string Password { get; set; }
        }

        /// <summary>
        /// Konstruktor, der ein paar TestUser anlegt
        /// </summary>
        public MyUserStore() 
        {
            CreateUser("Anton", "A113r131");
            CreateUser("Berta", "Pa$$w0rd");
            CreateUser("Caesar", "Brutus");
        }


        /// <summary>
        /// Ablage aller Benutzer
        /// </summary>
        ConcurrentDictionary<string, MyUser> users = new ConcurrentDictionary<string, MyUser>();

        /// <summary>
        /// Legt ein neues Benutzerkonto an
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        public bool CreateUser(string UserName, string PassWord)
        {
            if(users.ContainsKey(UserName))
            {
                return false;
            }
            else
            {
                users[UserName] = new MyUser(UserName, PassWord);
                return true;
            }
        }

        /// <summary>
        /// Löscht einen bereits existierenden Benutzer
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool DeleteUser(string UserName)
        {
            if(users.ContainsKey(UserName))
            {
                return users.TryRemove(UserName, out _);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Liefert einen existierenden Benutzer aus, falls er existiert
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public async Task<(bool UserFound, MyUser? User)> GetUser(string UserName)
        {
            if (users.ContainsKey(UserName))
            {
                return await Task.FromResult((true, users[UserName]));
            }
            else
            {
                return await Task.FromResult((false, (MyUser?)null));
            }
        }
    }
}
