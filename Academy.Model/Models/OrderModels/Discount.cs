namespace Academy.Model.Models.OrderModels
{
   public class Discount
    {
        public int DiscountId { get; set; }
        public string DiscountCode { get; set; }
        public int DiscountPercent { get; set; }
        public int? UsableCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<UserDiscountCode> UserDiscountCodes { get; set; }
    }
}
