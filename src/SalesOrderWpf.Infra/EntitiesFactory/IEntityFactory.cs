using SalesOrderWpf.Domain.Models.Orders;
using System.Collections.Generic;

namespace SalesOrderWpf.Infra.EntitiesFactory
{
    public interface IEntityFactory
    {
        IList<Order> CreateOrders();
        IList<Line> CreateLines();
    }
}
