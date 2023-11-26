using Academy.Model.Models.IdentityModels;
using System.ComponentModel.DataAnnotations;

namespace Academy.Model.Models.OrderModels
{
    public class UserDiscountCode
    {
        public int UserId { get; set; }
        public int DiscountId { get; set; }


        public User User { get; set; }
        public Discount Discount { get; set; }
    }
}
