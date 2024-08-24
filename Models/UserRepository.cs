using System.Linq;
using WatchitWebApp.Entities;
using WebSite.Common;

namespace WatchitWebApp.Models
{
    public class UserRepository
    {
        private readonly WatchitDBContext _watchitDBContext;

        public UserRepository(WatchitDBContext _watchitDBContext)
        {
            this._watchitDBContext = _watchitDBContext;
        }

        public User GetUserByLogin(string login) => _watchitDBContext.Users.SingleOrDefault(u => u.Login == login);
        public User GetUserByEmail(string email) => _watchitDBContext.Users.SingleOrDefault(u => u.Email == email);
        public User GetUserById(int? id) => _watchitDBContext.Users.SingleOrDefault(u => u.Id == id);
        internal bool AddUser(User user)
        {
            user.Password = SecurePasswordHasher.Hash(user.Password);

            _watchitDBContext.Users.Add(user);
            return _watchitDBContext.SaveChanges() == 1 ? true : false;
        }
    }
}
