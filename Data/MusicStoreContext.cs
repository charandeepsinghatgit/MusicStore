using Microsoft.EntityFrameworkCore;
using MusicStore.Models;

namespace MusicStore.Data
{
    public class MusicStoreContext : DbContext
    {
        public MusicStoreContext(DbContextOptions<MusicStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Track> Tracks { get; set; }

        // Cart functionality
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}