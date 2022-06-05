using DiplomEremenko.Data.Repository.IRepository;
using DiplomEremenko.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiplomEremenko.Data.Repository
{
    public class UserLoayltyRepository:IUserLoyaltyRepository
    {
        ApplicationDbContext _Db;

        public UserLoayltyRepository(ApplicationDbContext dbContext) 
        {
            _Db=dbContext;
        }

        public string Delete(Guid Id)
        {
            if (Id == null || Id == Guid.Empty)
            {
                throw new Exception("Пустой гуид");
            }

            UserLoyalty currentUser = _Db.UsersLoyaltie.FirstOrDefault(x=>x.Id==Id);// select top 1 * from  _Db.UsersLoyaltie Where Id="полученный id"
            if (currentUser == null)
            {
                throw new Exception("Невозможно удалить, так как пользователь с таким гуинд отсутствует");
            }
            _Db.UsersLoyaltie.Remove(currentUser);
            _Db.SaveChanges();
            return "200";
        }


        public UserLoyalty Get(Guid Id)
        {
            if (Id == Guid.Empty) 
            {
                throw new Exception("Пустой гуид");
            }

            var user = _Db.UsersLoyaltie.Include(l => l.Loyalty).FirstOrDefault(x => x.Id == Id);
            if (user == null) 
            {
                throw new Exception("Пользователь неинайден");
            }
            return user;
        }

        public IEnumerable<UserLoyalty> FindUsers(string data)
        {
            if (data == null || data == "")
            {
               return GetAll();
            }
            else 
            {
                string currentData = data.ToLower();

                List<UserLoyalty> userList = _Db.UsersLoyaltie.Where(x =>
                                                                        x.Name.ToLower().Contains(currentData) ||
                                                                        x.Loyalty.CardNumber.ToString().Contains(currentData)||
                                                                        x.Surname.ToLower().Contains(currentData) ||
                                                                        x.Phone.ToLower().Contains(currentData)).Include(x=>x.Loyalty).ToList();

                if (userList == null)
                {
                    throw new Exception("данные не найдены");
                }

                return userList;
            }
        }
        public IEnumerable<UserLoyalty> GetAll()
        {
            List<UserLoyalty> userList = _Db.UsersLoyaltie.Include(x=>x.Loyalty).ToList();
           
            if (userList == null) 
            {
                throw new Exception("данные не найдены");
            }
            
            return userList;
        }

        public Loyalty GetLoylaty(Guid UserId)
        {
            if (UserId == Guid.Empty)
            {
                throw new Exception("Пустой гуид");
            }

            var currentLoyalty = _Db.Loyalties.FirstOrDefault(x => x.UserLoyaltyId == UserId);
            if (currentLoyalty == null)
                throw new Exception();

            return currentLoyalty;
        }

        public string Insert(UserLoyalty userLoyalty)
        {
            if (userLoyalty == null) 
            {
                throw new Exception("Невозможно вставить пустой объект");
            }
            _Db.UsersLoyaltie.Add(userLoyalty);
            _Db.SaveChanges();

            
            return "200";
        }

        public string offBonus(Loyalty loyalty)
        {
            if (loyalty == null)
                throw new Exception();

            loyalty.Balance = 0;
            _Db.Loyalties.Update(loyalty);
            _Db.SaveChanges();

            return "200";
        }

        public string Update(Guid Id, UserLoyalty userLoyalty)
        {
            if (userLoyalty == null || Id == null || Id == Guid.Empty)
            {
                throw new Exception("Недостаточно данных для обновления");            
            }
            var currentUser = _Db.UsersLoyaltie.FirstOrDefault(x => x.Id == Id);
            if (currentUser == null) 
            {
                throw new Exception("Пользователь с таким Id не найден");            
            }

            currentUser.Surname = userLoyalty.Surname;
            currentUser.Name = userLoyalty.Name;    
            currentUser.Birthday = userLoyalty.Birthday;
            currentUser.Phone = userLoyalty.Phone;
            currentUser.ModifiedOn = DateTime.Now;

            _Db.UsersLoyaltie.Update(currentUser);
            _Db.SaveChanges();
            return "200";
        }

        public string UpdateLoylaty(Guid UserId, Loyalty loyalty)
        {
            if (UserId == Guid.Empty || loyalty == null)
                throw new Exception();

            var currentLoyalty = _Db.Loyalties.FirstOrDefault(x => x.UserLoyaltyId == UserId);

            currentLoyalty.Balance += loyalty.Balance; 
            _Db.Loyalties.Update(currentLoyalty);
            _Db.SaveChanges();

            return "200";
        }
    }
}
