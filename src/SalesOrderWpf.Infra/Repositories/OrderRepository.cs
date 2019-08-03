using SalesOrderWpf.Domain.Models.Orders;
using SalesOrderWpf.Domain.Repositories;
using SalesOrderWpf.Infra.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace SalesOrderWpf.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        ISalesOrderDataContext _context;

        public OrderRepository(ISalesOrderDataContext context)
        {
            _context = context;
        }

        public Order Get(string id)
        {
            return _context.Orders.FirstOrDefault(o => id.Equals(o.Id.ToString()));
        }

        public IList<Order> GetAll()
        {
            return _context.Orders;
        }

        public void Add(Order order)
        {
            order.Lines.Clear();
            _context.Orders.Add(order);
        }

        public void Update(Order order)
        {
            //_context.Orders.FirstOrDefault(o => order.Id.Equals(o.Id))?.Update(order.BusinessPartner);
        }

        public void Delete(Order order)
        {
            _context.Orders.Remove(order);
        }
    }
}
