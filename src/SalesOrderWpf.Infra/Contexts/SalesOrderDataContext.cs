using System.Collections.Generic;
using SalesOrderWpf.Domain.Models.Orders;
using SalesOrderWpf.Infra.EntitiesFactory;

namespace SalesOrderWpf.Infra.Contexts
{
    public class SalesOrderDataContext : ISalesOrderDataContext
    {
        public SalesOrderDataContext(IEntityFactory entityFactory)
        {
            Orders = entityFactory.CreateOrders();
            Lines = entityFactory.CreateLines();
        }

        public IList<Order> Orders { get; set; }
        public IList<Line> Lines { get; set; }
    }
}
