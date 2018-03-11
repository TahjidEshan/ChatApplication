using System;
using System.Linq;
using ChatServer.DAL;
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
    }
}
