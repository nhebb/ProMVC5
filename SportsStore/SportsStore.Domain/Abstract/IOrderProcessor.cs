using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrders(Cart cart, ShippingDetails shippingDetails);
    }
}
