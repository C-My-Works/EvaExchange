namespace EvaExchange.Entities
{
    public enum TradeType
    {
        BUY,SELL
    }
    public class Trade
    {
        public int Id { get; set; }
        public TradeType TradeType { get; set; }
        public int PortfolioId { get; set; }
        public int ShareId { get; set; }
        public int TotalQuantity { get; set; } //TotalQuantity
        public decimal TotalPrice { get; set; }
        public DateTime TradeTime { get; set; }

        //Relations
        
        public Portfolio Portfolio { get; set; }
        
    }
}
