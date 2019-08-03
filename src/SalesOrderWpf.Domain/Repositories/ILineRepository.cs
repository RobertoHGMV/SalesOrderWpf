using SalesOrderWpf.Domain.Models.Orders;
using System.Collections.Generic;

namespace SalesOrderWpf.Domain.Repositories
{
    public interface ILineRepository
    {
        Line Get(string id);
        IList<Line> GetByOrder(string orderId);
        void Add(Line line);
        void Add(IList<Line> lines);
        void Update(Line line);
        void Update(IList<Line> lines);
        void Delete(Line line);
        void Delete(IList<Line> lines);
    }
}
