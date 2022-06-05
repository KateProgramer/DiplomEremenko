using DiplomEremenko.Models;

namespace DiplomEremenko.Data.Repository.IRepository
{
    public interface IUserLoyaltyRepository
    {
        public UserLoyalty Get(Guid Id);
        public Loyalty GetLoylaty(Guid UserId);
        public string UpdateLoylaty(Guid UserId, Loyalty loyalty);
        public string offBonus(Loyalty loyalty);
        public string Insert(UserLoyalty userLoyalty);
        public string Update(Guid Id, UserLoyalty userLoyalty);
        public string Delete(Guid Id);
        public IEnumerable<UserLoyalty> GetAll();

        public IEnumerable<UserLoyalty> FindUsers(string data);
    }
}
