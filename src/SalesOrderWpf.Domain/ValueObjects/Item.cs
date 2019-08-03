namespace SalesOrderWpf.Domain.ValueObjects
{
    public class Item
    {
        public Item(string itemCode, string itemName, decimal price)
        {
            ItemCode = itemCode;
            ItemName = itemName;
            Price = price;
        }

        public string ItemCode { get; private set; }
        public string ItemName { get; private set; }
        public decimal Price { get; private set; }
    }
}
