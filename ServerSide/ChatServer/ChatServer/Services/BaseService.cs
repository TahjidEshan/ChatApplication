using System;
using System.Linq;
using ChatServer.DAL;
using ChatServer.Helpers;
using ChatServer.Models.Data;


namespace ChatServer.Services
{
    public class BaseService: IBaseService
    {
        protected IGenericRepository BaseRepository { get; set; }

        public BaseService(IGenericRepository BaseRepository)
        {
            this.BaseRepository = BaseRepository;
        }


        //User
        public void Save(User User)
        {
            BaseRepository.Insert<User>(User);
        }
        public void Delete(User User)
        {
            BaseRepository.Delete<User>(User);
        }
        public void Update(User User)
        {
            BaseRepository.Update<User>(User);
        }
        public User GetUserById(Guid Id)
        {
            return BaseRepository.GetByID<User>(Id);
        }
        public IQueryable<User> GetUsers()
        {
            return BaseRepository.GetQuery<User>();
        }
        public User Authenticate(string Username, string Password)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)) return null;

            User User = BaseRepository.GetQuery<User>().SingleOrDefault(x => x.UserName.Equals(Username));
            
            if (User == null) return null;

            if (!ServiceHelpers.VerifyPasswordHash(Password, User.PasswordHash, User.PasswordSalt))
                return null;

            return User;
        }
    }
}
