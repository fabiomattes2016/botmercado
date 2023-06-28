namespace BotMercadoBitcoin.Responses
{
    public class ResponseOrders
    {
        public string id { get; set; }
        public string fee { get; set; }
        public string side { get; set; }
        public string filledQty { get; set; }
        public decimal avgPrice { get; set; }
        public string status { get; set; }
    }
}
