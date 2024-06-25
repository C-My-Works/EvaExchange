namespace EvaExchange.Entities
{
    public class Share
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime PriceChangeDate { get; set; }
        


        //Relations
        public List<Trade> Trades {  get; set; }


    }
}
