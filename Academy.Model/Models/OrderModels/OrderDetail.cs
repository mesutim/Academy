using Academy.Model.Models.CourseModels;

namespace Academy.Model.Models.OrderModels
{
    public class OrderDetail
    {
        public int DetailId { get; set; }
        public int OrderId { get; set; }
        public int CourseId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public Order Order { get; set; }
        public Course Course { get; set; }
    }
}
