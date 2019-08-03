using SalesOrderWpf.Domain.Models.Orders;
using SalesOrderWpf.Domain.Repositories;
using SalesOrderWpf.Domain.Services;
using SalesOrderWpf.Domain.ValueObjects;
using SalesOrderWpf.Domain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SalesOrderWpf.Business.Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        ILineRepository _lineRepository;

        public OrderService(IOrderRepository orderRepository, ILineRepository lineRepository)
        {
            _orderRepository = orderRepository;
            _lineRepository = lineRepository;
        }

        public OrderInput Get(string id)
        {
            var order = _orderRepository.Get(id);
            var lines = _lineRepository.GetByOrder(id);
            var linesInput = GetLineInput(lines);

            return GetOrderInput(order, linesInput);
        }

        public IList<OrderInput> GetAll()
        {
            var orders = _orderRepository.GetAll();
            return GetOrderInput(orders);
        }

        public void Add(OrderInput orderInput)
        {
            var order = CreateOrder(orderInput);
            var lines = CreateLine(orderInput.Lines);

            order.AddLine(lines);

            _orderRepository.Add(order);
            _lineRepository.Add(lines);
        }

        public void Update(OrderInput orderInput)
        {
            var order = _orderRepository.Get(orderInput.Id);
            var lines = _lineRepository.GetByOrder(orderInput.Id);
            var businessPartner = new BusinessPartner(orderInput.CardCode, orderInput.CardName);

            var linesInputToAdd = orderInput.Lines.Where(c => !lines.Any(x => x.Id.ToString().Equals(c.Id))).ToList();
            var linesInputToUpdate = orderInput.Lines.Where(c => lines.Any(x => x.Id.ToString().Equals(c.Id))).ToList();
            var linesToRemove = lines.Where(c => !orderInput.Lines.Any(x => x.Id.Equals(x.Id.ToString()))).ToList();

            var linesToAdd = CreateLine(linesInputToAdd);
            var linesToUpdate = CreateLine(linesInputToUpdate);

            linesToUpdate.ForEach(l => l.Update(l));
            order.AddLine(linesToAdd);
            order.RemoveLine(linesToRemove);

            order.Update(businessPartner);
            _lineRepository.Update(linesToUpdate);
            _lineRepository.Add(linesToAdd);
            _lineRepository.Delete(linesToRemove);
            _orderRepository.Update(order);
        }

        public void Delete(string id)
        {
            var order = _orderRepository.Get(id);
            var lines = _lineRepository.GetByOrder(id);

            _lineRepository.Delete(lines);
            _orderRepository.Delete(order);
        }

        #region CreateOrdersAndLines

        private Order CreateOrder(OrderInput orderInput)
        {
            var businessPartner = new BusinessPartner(orderInput.CardCode, orderInput.CardName);
            return new Order(businessPartner);
        }

        private List<Line> CreateLine(IList<LineInput> linesInput)
        {
            var lines = new List<Line>();

            foreach (var lineInput in linesInput)
                lines.Add(CreateLine(lineInput));

            return lines;
        }

        private Line CreateLine(LineInput lineInput)
        {
            var item = new Item(lineInput.ItemCode, lineInput.ItemName, lineInput.Price);
            return new Line(item, lineInput.Quantity);
        }

        #endregion

        #region CreateInputs

        private OrderInput GetOrderInput(Order order, List<LineInput> linesInput)
        {
            return new OrderInput
            {
                Id = order.Id.ToString(),
                CardCode = order.BusinessPartner.CardCode,
                CardName = order.BusinessPartner.CardName,
                Total = order.Total,
                Lines = linesInput
            };
        }

        private OrderInput GetOrderInput(Order order)
        {
            return new OrderInput
            {
                Id = order.Id.ToString(),
                CardCode = order.BusinessPartner.CardCode,
                CardName = order.BusinessPartner.CardName,
                Total = order.Total
            };
        }

        private List<OrderInput> GetOrderInput(IList<Order> orders)
        {
            var ordersInput = new List<OrderInput>();

            foreach (var order in orders)
                ordersInput.Add(GetOrderInput(order));

            return ordersInput;
        }

        private LineInput GetLineInput(Line line)
        {
            return new LineInput
            {
                Id = line.Id.ToString(),
                OrderId = line.OrderId.ToString(),
                ItemCode = line.Item.ItemCode,
                ItemName = line.Item.ItemName,
                Price = line.Item.Price,
                Quantity = line.Quantity,
                Total = line.Total
            };
        }

        private List<LineInput> GetLineInput(IList<Line> lines)
        {
            var linesInput = new List<LineInput>();

            foreach (var line in lines)
                linesInput.Add(GetLineInput(line));

            return linesInput;
        }

        #endregion
    }
}
