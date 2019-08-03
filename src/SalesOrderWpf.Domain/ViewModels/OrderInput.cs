using System.Collections.Generic;

namespace SalesOrderWpf.Domain.ViewModels
{
    public class OrderInput
    {
        public string Id { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public decimal Total { get; set; }
        public IList<LineInput> Lines { get; set; }
    }
}
