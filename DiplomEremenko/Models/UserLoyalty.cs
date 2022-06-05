namespace DiplomEremenko.Models
{
    public class UserLoyalty
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime ModifiedOn { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        public Loyalty Loyalty { get; set; }
    }
}
