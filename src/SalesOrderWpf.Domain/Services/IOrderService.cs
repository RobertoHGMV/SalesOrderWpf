using SalesOrderWpf.Domain.Models.Orders;
using SalesOrderWpf.Domain.ViewModels;
using System.Collections.Generic;

namespace SalesOrderWpf.Domain.Services
{
    public interface IOrderService
    {
        OrderInput Get(string id);
        IList<OrderInput> GetAll();
        void Add(OrderInput orderInput);
        void Update(OrderInput orderInput);
        void Delete(string id);
    }
}
