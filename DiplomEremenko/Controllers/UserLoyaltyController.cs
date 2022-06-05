using DiplomEremenko.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DiplomEremenko.Models;
using DiplomEremenko.Models.ViewModel;
using DiplomEremenko.Data;
using DiplomEremenko.BusinessLogic.CardLoyalte;

namespace DiplomEremenko.Controllers
{
    public class UserLoyaltyController : Controller
    {
        IUserLoyaltyRepository _Repository;
        ApplicationDbContext _Db;



        public UserLoyaltyController(IUserLoyaltyRepository repository, ApplicationDbContext dbContext)
        {
            _Repository = repository;
            _Db = dbContext;
        }

        [HttpPost]
        [Authorize]
        public IActionResult FilterView(string data) 
        {
            return View("UsersLoyalty", _Repository.FindUsers(data));
        }

        //GET
        [Authorize]
        public IActionResult UsersLoyalty()
        {
            return View(_Repository.GetAll());
        }
        
        //GET
        [Authorize]
        public IActionResult ShowAddView()
        {
            return View();
        }

        [Authorize]
        public IActionResult CreateUser(UserLoyaltyVM obj) 
        {
            if (!ModelState.IsValid) 
            {
                ViewBag.ModalError = "Проверьте корректность вводимых данных";
                return View("ShowAddView");
            }

            UserLoyalty user = new UserLoyalty();

            user.Name=obj.Name;
            user.Surname=obj.Surname;
            user.Phone=obj.Phone;
            user.Birthday=obj.Birthday;

            CardNumberGenerator cardNumberGenerator = new CardNumberGenerator(_Db);
            int cardNuber = cardNumberGenerator.CreateCardNUmber();

            Loyalty loyalty = new Loyalty();
            loyalty.CardNumber=cardNuber;
            
            user.Loyalty=loyalty;

            _Repository.Insert(user);

            return RedirectToAction("UsersLoyalty");
        }
        [Authorize]
        public IActionResult DeleteUser(Guid id) 
        {
            _Repository.Delete(id);

            return RedirectToAction("UsersLoyalty");
        }
    }
}
