using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BinaryWarfare.Data;
using BinaryWarfare.Model;

namespace BinaryWarfare.Repository
{
    public class UsersRepository : BaseRepository, IUsersRepository
    {
        private const string SessionKeyChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const int SessionKeyLen = 50;

        private const string ValidUsernameChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM_1234567890";
        private const string ValidNicknameChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM_1234567890 -";
        private const int MinUsernameNicknameChars = 3;
        private const int MaxUsernameNicknameChars = 30;

        private DbSet<User> entitySet;

        public UsersRepository(DbContext dbContent)
            : base(dbContent)
        {
            this.entitySet = this.context.Set<User>();
        }

        private void ValidateSessionKey(string sessionKey)
        {
            if (sessionKey.Length != SessionKeyLen || sessionKey.Any(ch => !SessionKeyChars.Contains(ch)))
            {
                throw new ServerErrorException("Invalid Password", "ERR_INV_AUTH");
            }
        }

        private string GenerateSessionKey(int userId)
        {
            StringBuilder keyChars = new StringBuilder(50);
            keyChars.Append(userId.ToString());
            while (keyChars.Length < SessionKeyLen)
            {
                int randomCharNum;
                lock (rand)
                {
                    randomCharNum = rand.Next(SessionKeyChars.Length);
                }
                char randomKeyChar = SessionKeyChars[randomCharNum];
                keyChars.Append(randomKeyChar);
            }
            string sessionKey = keyChars.ToString();
            return sessionKey;
        }

        private void ValidateUsername(string username)
        {
            if (username == null || username.Length < MinUsernameNicknameChars || username.Length > MaxUsernameNicknameChars)
            {
                throw new ServerErrorException("Username should be between 4 and 30 symbols long", "INV_USRNAME_LEN");
            }
            else if (username.Any(ch => !ValidUsernameChars.Contains(ch)))
            {
                throw new ServerErrorException("Username contains invalid characters", "INV_USRNAME_CHARS");
            }
        }

        private void ValidateAuthCode(string authCode)
        {
            if (authCode.Length != Sha1CodeLength)
            {
                throw new ServerErrorException("Invalid authentication code length", "INV_USR_AUTH_LEN");
            }
        }

        /* public members */

        public User Add(User user)
        {
            ValidateUsername(user.Username);
            ValidateAuthCode(user.AuthCode);
            var usernameToLower = user.Username.ToLower();

            var dbUser = this.entitySet.FirstOrDefault(u => u.Username.ToLower() == usernameToLower);

            if (dbUser != null)
            {
                if (dbUser.Username.ToLower() == usernameToLower)
                {
                    throw new ServerErrorException("Username already exists", "ERR_DUP_USR");
                }
                else
                {
                    throw new ServerErrorException("Nickname already exists", "ERR_DUP_NICK");
                }
            }

            this.entitySet.Add(user);
            this.context.SaveChanges();
            return user;
        }

        public string Login(User user)
        {
            ValidateUsername(user.Username);
            ValidateAuthCode(user.AuthCode);

            var dbUser = this.entitySet.FirstOrDefault(u => u.Username.ToLower() == user.Username.ToLower() && u.AuthCode == user.AuthCode);
            if (dbUser == null)
            {
                throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
            }

            var sessionKey = GenerateSessionKey((int)user.Id);
            dbUser.SessionKey = sessionKey;
            context.SaveChanges();
            return sessionKey;
        }

        public void Logout(string sessionKey)
        {
            ValidateSessionKey(sessionKey);
            var user = this.entitySet.FirstOrDefault(u => u.SessionKey == sessionKey);
            if (user == null)
            {
                throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
            }

            user.SessionKey = null;
            context.SaveChanges();
        }

        public User Get(string sessionKey)
        {
            var user = this.entitySet.FirstOrDefault(u => u.SessionKey == sessionKey);
            if (user == null)
            {
                throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
            }

            return user;
        }

        public User Get(int id)
        {
            var user = this.entitySet.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
            }

            return user;
        }

        public IQueryable<User> All()
        {
            return this.entitySet;
        }

        /* methods not implemented */

        public User Update(int id, User item)
        {
            throw new NotImplementedException("Method not supported for Users");
        }

        public void Delete(int id)
        {
            throw new NotImplementedException("Method not supported for Users");
        }

        public void Delete(User item)
        {
            throw new NotImplementedException("Method not supported for Users");
        }

        public IQueryable<User> Find(Expression<Func<User, int, bool>> predicate)
        {
            throw new NotImplementedException("Method not supported for Users");
        }
    }
}