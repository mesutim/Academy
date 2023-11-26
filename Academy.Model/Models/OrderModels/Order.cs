using Academy.Model.Models.IdentityModels;

namespace Academy.Model.Models.OrderModels
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int OrderSum { get; set; }
        public bool IsFinaly { get; set; }
        public DateTime CreateDate { get; set; }
        public User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
