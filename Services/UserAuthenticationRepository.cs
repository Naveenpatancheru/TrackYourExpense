using System;
using System.Collections.Generic;
using System.Linq;
using TrackYourExpenseApi.DbContexts;
using TrackYourExpenseApi.Entities;
using TrackYourExpenseApi.Helpers;

namespace TrackYourExpenseApi.Services
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository, IDisposable
    {
        private ExpensesContext _context;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
        public UserAuthenticationRepository(ExpensesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public User Authenticate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            var user = _context.Users.SingleOrDefault(x => x.UserName == userName);

            if(user == null)
            {
                return null;
            }
            if (!PasswordHashing.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }
        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is Required");

            if (_context.Users.Any(x => x.UserName == user.UserName))
                throw new AppException("UserName \"" + user.UserName + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            Helpers.PasswordHashing.CreatePasswordHash(password, out passwordHash,out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
                 
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
      

        public void Update(User userParam, string password)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new AppException("User Not Found");

            if(!string.IsNullOrWhiteSpace(userParam.UserName) && userParam.UserName != user.UserName)
            {
                if (_context.Users.Any(x => x.UserName == userParam.UserName))
                    throw new AppException("UserName" + userParam.UserName + "is already taken");
                user.UserName = userParam.UserName;
            }

            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
                user.FirstName = userParam.FirstName;

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
                user.LastName = userParam.LastName;

            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                PasswordHashing.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
