using System.Collections.Generic;
using TrackYourExpenseApi.Entities;

namespace TrackYourExpenseApi.Services
{
    public interface IUserAuthenticationRepository
    {
        public User Authenticate(string userName, string password);
        public IEnumerable<User> GetAll();
        public User GetById(int id);
        public User Create(User user, string password);
        public void Update(User user, string password=null);
        public void Delete(int id);

    }
}