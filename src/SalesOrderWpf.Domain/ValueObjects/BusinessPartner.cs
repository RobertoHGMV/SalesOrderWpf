namespace SalesOrderWpf.Domain.ValueObjects
{
    public class BusinessPartner
    {
        public BusinessPartner(string cardCode, string cardName)
        {
            CardCode = cardCode;
            CardName = cardName;
        }

        public string CardCode { get; private set; }
        public string CardName { get; private set; }
    }
}
