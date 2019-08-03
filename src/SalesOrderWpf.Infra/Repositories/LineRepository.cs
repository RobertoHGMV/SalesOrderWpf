using SalesOrderWpf.Domain.Models.Orders;
using SalesOrderWpf.Domain.Repositories;
using SalesOrderWpf.Infra.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace SalesOrderWpf.Infra.Repositories
{
    public class LineRepository : ILineRepository
    {
        ISalesOrderDataContext _context;

        public LineRepository(ISalesOrderDataContext context)
        {
            _context = context;
        }

        public Line Get(string id)
        {
            return _context.Lines.FirstOrDefault(l => id.Equals(l.Id.ToString()));
        }

        public IList<Line> GetByOrder(string orderId)
        {
            return _context.Lines.Where(l => orderId.Equals(l.OrderId.ToString())).ToList();
        }

        public void Add(Line line)
        {
            _context.Lines.Add(line);
        }

        public void Add(IList<Line> lines)
        {
            foreach (var line in lines)
                Add(line);
        }

        public void Update(Line line)
        {
            //_context.Lines.FirstOrDefault(l => line.Id.Equals(l.Id.ToString()))?.Update(line);
        }

        public void Update(IList<Line> lines)
        {
            foreach (var line in lines)
                Update(line);
        }

        public void Delete(Line line)
        {
            _context.Lines.Remove(line);
        }

        public void Delete(IList<Line> lines)
        {
            foreach (var line in lines)
                Delete(line);
        }
    }
}
