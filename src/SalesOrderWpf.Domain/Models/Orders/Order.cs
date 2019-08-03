using SalesOrderWpf.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesOrderWpf.Domain.Models.Orders
{
    public class Order
    {
        IList<Line> _lines;

        public Order(BusinessPartner businessPartner)
        {
            Id = new Guid();
            BusinessPartner = businessPartner;
            _lines = new List<Line>();
        }

        public Guid Id { get; private set; }
        public BusinessPartner BusinessPartner { get; private set; }
        public decimal Total { get; private set; }
        public IList<Line> Lines => _lines;

        #region Lines

        public void AddLine(Line line)
        {
            line.SetOrderId(this);
            _lines.Add(line);
            SetTotal();
        }

        public void AddLine(IList<Line> lines)
        {
            foreach (var line in lines)
                AddLine(line);
        }

        public void RemoveLine(Line line)
        {
            _lines.Remove(line);
            SetTotal();
        }

        public void RemoveLine(IList<Line> lines)
        {
            foreach (var line in lines)
                RemoveLine(line);
        }

        #endregion

        void SetTotal() => Total = _lines.Sum(l => l.Total);

        public void Update(BusinessPartner businessPartner)
        {
            BusinessPartner = businessPartner;
            SetTotal();
        }
    }
}
