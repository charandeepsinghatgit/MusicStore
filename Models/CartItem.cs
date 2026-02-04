namespace MusicStore.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int AlbumId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedDate { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Album Album { get; set; }
    }
}