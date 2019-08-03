using SalesOrderWpf.Domain.Models.Orders;
using System.Collections.Generic;

namespace SalesOrderWpf.Infra.Contexts
{
    public interface ISalesOrderDataContext
    {
        IList<Order> Orders { get; set; }
        IList<Line> Lines { get; set; }
    }
}
