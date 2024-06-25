using EvaExchange.Entities;

namespace EvaExchange.Dto
{
    public class TradeDto
    {
        
        public TradeType TradeType { get; set; }
        public int PortfolioId { get; set; }
        public int ShareId { get; set; }
        public int TotalQuantity { get; set; }

    }
}
