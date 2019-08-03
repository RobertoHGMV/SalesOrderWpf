namespace SalesOrderWpf.Domain.ViewModels
{
    public class LineInput
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
