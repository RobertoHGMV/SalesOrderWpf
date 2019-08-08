using System.Collections.Generic;
using System.Linq;

namespace SalesOrderWpf.Domain.ViewModels
{
    public class OrderInput
    {
        public OrderInput()
        {
            Lines = new List<LineInput>();
        }

        public string Id { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public decimal Total { get { return CalcTotal(); } }
        public IList<LineInput> Lines { get; set; }

        public decimal CalcTotal()
        {
            return Lines.Sum(c => c.Total);
        }
    }
}
