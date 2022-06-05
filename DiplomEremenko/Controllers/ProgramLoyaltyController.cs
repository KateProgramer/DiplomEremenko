using DiplomEremenko.Data.Repository.IRepository;
using DiplomEremenko.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomEremenko.Controllers
{
    public class ProgramLoyaltyController : Controller
    {
        IUserLoyaltyRepository _Repository;
        public ProgramLoyaltyController(IUserLoyaltyRepository repositor) 
        {
            _Repository=repositor;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult UserLoyalty(Guid id)
        {
            var currentUser = _Repository.Get(id);
            
            return View(currentUser);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddBonus(int countBonus, Guid id) 
        {
            if (id == Guid.Empty)
                throw new Exception();
            if (countBonus <= 0) 
            {
                ViewBag.CountBonus = countBonus;
                var currentUser = _Repository.Get(id);

                return View("UserLoyalty", currentUser);
            }
            
            var currentLoylty = _Repository.GetLoylaty(id);
            if (currentLoylty == null)
                throw new Exception();

            Loyalty addBonus = new Loyalty();
            addBonus.Balance = countBonus;

            _Repository.UpdateLoylaty(id, addBonus);

            return RedirectToAction("UserLoyalty", new { id = id });
        }
        [Authorize]
        public IActionResult offBonus(Guid id) 
        {
            if (id == Guid.Empty)
                RedirectToAction("Index","Home");

            var currentLoylty = _Repository.GetLoylaty(id);
            if (currentLoylty == null)
                throw new Exception();
            ViewBag.offBonus = currentLoylty.Balance;

            _Repository.offBonus(currentLoylty);

            var currentUser = _Repository.Get(id);

            return View("UserLoyalty", currentUser);
        }
    }
}
