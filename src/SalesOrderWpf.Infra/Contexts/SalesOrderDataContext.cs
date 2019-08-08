using System.Collections.Generic;
using SalesOrderWpf.Domain.Models.Orders;

namespace SalesOrderWpf.Infra.Contexts
{
    public class SalesOrderDataContext : ISalesOrderDataContext
    {
        public SalesOrderDataContext()
        {
            Orders = new List<Order>();
            Lines = new List<Line>();
        }

        public IList<Order> Orders { get; set; }
        public IList<Line> Lines { get; set; }
    }
}
