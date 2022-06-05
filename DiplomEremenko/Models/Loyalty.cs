namespace DiplomEremenko.Models
{
    public class Loyalty
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime ModifiedOn { get; set; }
        public int CardNumber { get; set; }
        public int Balance { get; set; } = 0;

        public Guid UserLoyaltyId { get; set; }
        public UserLoyalty UserLoyalty { get; set; }

    }
}
