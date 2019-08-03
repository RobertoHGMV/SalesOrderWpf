using SalesOrderWpf.Domain.ValueObjects;
using System;

namespace SalesOrderWpf.Domain.Models.Orders
{
    public class Line
    {
        public Line(Item item, int quantity)
        {
            Id = Guid.NewGuid();
            Item = item;
            Quantity = quantity;
        }

        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Item Item { get; private set; }
        public int Quantity { get; private set; }
        public decimal Total => Item.Price * Quantity;

        public void Update(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public void Update(Line line)
        {
            Update(line.Item, line.Quantity);
        }

        public void SetOrderId(Order order)
        {
            OrderId = order.Id;
        }
    }
}
