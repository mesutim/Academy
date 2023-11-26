using Academy.Model.Models.IdentityModels;

namespace Academy.Model.Models.TransactionModels
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int TypeId { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public bool IsPay { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual User User { get; set; }
        public virtual TransactionType TransactionType { get; set; }
    }
}
