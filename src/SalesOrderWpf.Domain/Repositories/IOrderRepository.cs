using SalesOrderWpf.Domain.Models.Orders;
using System;
using System.Collections.Generic;

namespace SalesOrderWpf.Domain.Repositories
{
    public interface IOrderRepository
    {
        Order Get(string id);
        IList<Order> GetAll();
        void Add(Order order);
        void Update(Order order);
        void Delete(Order order);
    }
}
