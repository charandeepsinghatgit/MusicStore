namespace MusicStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
