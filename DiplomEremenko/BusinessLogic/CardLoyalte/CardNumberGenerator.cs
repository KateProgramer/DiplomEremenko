using DiplomEremenko.Data;

namespace DiplomEremenko.BusinessLogic.CardLoyalte
{
    public class CardNumberGenerator
    {
        const int _lengCard = 6;
        Random _Random;
        ApplicationDbContext _Db;

        public  CardNumberGenerator(ApplicationDbContext dbContext) 
         {
            _Random = new Random();
            _Db = dbContext;
         }

        public int CreateCardNUmber()
        {
           int card = Generator(_lengCard);
            while (_Db.Loyalties.Any(x => x.CardNumber == card)) 
            {
                card = Generator(_lengCard);
            }
            
            return card;
        }

       private int Generator(int lenght) 
        {
            string number = "";
            for (int i = 0; i < lenght; i++)
            {
                number += _Random.Next(0, 9).ToString();
            }
            int result = int.Parse(number);
            return result;
        }
    }
}

