namespace MusicStore.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public string SessionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}