using MusicStore.Models;

namespace MusicStore.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public int PendingOrders { get; set; }
        public int TotalCustomers { get; set; }

        public IEnumerable<Order> RecentOrders { get; set; } = new List<Order>();
    }
}