using EvaExchange.Data;
using EvaExchange.Dto;
using EvaExchange.Entities;
using EvaExchange.Interfaces;
using System.Diagnostics;

namespace EvaExchange.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly EvaDbContext _context;

        public TradeRepository(EvaDbContext context)
        {
            _context = context;
        }

        public void Add(Trade trade)
        {
            _context.Add(trade);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Remove(id);
            _context.SaveChanges();
        }
        public void Delete(TradeDto tradeDto)
        {
            _context.Remove(tradeDto);
            _context.SaveChanges();
        }

        public List<Trade> GetAll()
        {
            var trades = _context.Trades.ToList();
            return trades;
        }

        public Trade GetById(int id)
        {
            //    var trade = _context.Trades.ToList()
            //.Where(t => t.PortfolioId == tradeDto.PortfolioId
            //            && t.ShareId == tradeDto.ShareId
            //            && t.TradeType == TradeType.BUY);//;
            return _context.Trades.Find(id);
        }

        public int GetTotalShares(int portfolioId, int shareId)
        {
            //return _context.Trades
            //.Where(t => t.PortfolioId == portfolioId && t.ShareId == shareId && t.TradeType == TradeType.BUY)
            //.Sum(t => t.TotalQuantity)
            //- _context.Trades
            //.Where(t => t.PortfolioId == portfolioId && t.ShareId == shareId && t.TradeType == TradeType.SELL)
            //.Sum(t => t.TotalQuantity);
            var totalBuyQuantity = _context.Trades
       .Where(t => t.PortfolioId == portfolioId && t.ShareId == shareId && t.TradeType == TradeType.BUY)
       .Sum(t => t.TotalQuantity);

            var totalSellQuantity = _context.Trades
                .Where(t => t.PortfolioId == portfolioId && t.ShareId == shareId && t.TradeType == TradeType.SELL)
                .Sum(t => t.TotalQuantity);

            return totalBuyQuantity - totalSellQuantity;
        }

        public List<Trade> GetTradeHistory(int? portfolioId, int? shareId, TradeType? tradeType)
        {
            var query = _context.Trades.AsQueryable();

            if (portfolioId.HasValue)
            {
                query = query.Where(t => t.PortfolioId == portfolioId.Value);
            }
            if (shareId.HasValue)
            {
                query = query.Where(t => t.ShareId == shareId.Value);
            }
            
            return query.OrderBy(t => t.TradeTime).ToList();
        }

        public void Update(Trade trade)
        {
            _context.Trades.Update(trade);
            _context.SaveChanges();
        }
    }
}
