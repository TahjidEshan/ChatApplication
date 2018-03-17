using System;
using System.Linq;
using ChatServer.Models.Data;

namespace ChatServer.Services
{
    public interface IBaseService
    {
        //User
        void Save(User User);
        void Delete(User User);
        void Update(User User);
        User GetUserById(Guid Id);
        IQueryable<User> GetUsers();
        User Authenticate(string Username, string Password);
    }
}
