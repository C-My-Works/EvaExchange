using EvaExchange.Dto;
using EvaExchange.Entities;
using EvaExchange.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EvaExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IShareRepository _shareRepository;
        

        public TradeController(ITradeRepository tradeRepository, IPortfolioRepository portfolioRepository, IShareRepository shareRepository)
        {
            _tradeRepository = tradeRepository;
            _portfolioRepository = portfolioRepository;
            _shareRepository = shareRepository;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trade>>> GetAll()
        {
            return _tradeRepository.GetAll();
        }

        [HttpPost("Buy")]
        public async Task<ActionResult> Buy(TradeDto tradeDto)
        {
            var share = _shareRepository.GetById(tradeDto.ShareId);
            var portfolio = _portfolioRepository.GetById(tradeDto.PortfolioId);

            if (share == null)
            {
                return BadRequest("Share not found");
            }
            if (portfolio == null)
            {
                return BadRequest("Portfolio not found");
            }
            if ((tradeDto.TotalQuantity * share.Price) > portfolio.TotalAmount)
            {
                return BadRequest("Portfolio amount not enough for this trade");
            }
            if (share.Quantity < tradeDto.TotalQuantity || share.Quantity == 0)
            {
                return BadRequest("Share quantity not enough for this trade for BUY");
            }


            //var existingTrade = _tradeRepository.GetById(tradeDto.ShareId);
            //if (existingTrade != null)
            //{
            //    existingTrade.TotalQuantity += tradeDto.TotalQuantity;
            //    existingTrade.TotalPrice += tradeDto.TotalQuantity * share.Price;
            //    _tradeRepository.Update(existingTrade);
            //}
            //else
            //{
                var trade = new Trade
                {
                    PortfolioId = tradeDto.PortfolioId,
                    ShareId = share.Id,
                    TotalQuantity = tradeDto.TotalQuantity,
                    TradeType = TradeType.BUY,
                    TradeTime = DateTime.UtcNow,
                    TotalPrice = (tradeDto.TotalQuantity * share.Price),
                };
                _tradeRepository.Add(trade);
            //}




            portfolio.TotalAmount -= tradeDto.TotalQuantity * share.Price;
            share.Quantity -= tradeDto.TotalQuantity;


            _portfolioRepository.Update(portfolio);
            _shareRepository.Update(share);
            

            return Ok();



        }
        [HttpPost("Sell")]
        public async Task<ActionResult> Sell(TradeDto tradeDto)
        {
            var share = _shareRepository.GetById(tradeDto.ShareId);
            var portfolio = _portfolioRepository.GetById(tradeDto.PortfolioId);
            var totalOwnedShares = _tradeRepository.GetTotalShares(tradeDto.PortfolioId, tradeDto.ShareId);

            if (share == null)
            {
                return BadRequest("Share not found");
            }
            if (portfolio == null)
            {
                return BadRequest("Portfolio not found");
            }
            if (totalOwnedShares < tradeDto.TotalQuantity)
            {
                return BadRequest("Share quantity not enough for this trade");
            }


            var existingTrade = _tradeRepository.GetById(tradeDto.ShareId);
            if (existingTrade != null)
            {
                existingTrade.TotalQuantity -= tradeDto.TotalQuantity;
                existingTrade.TotalPrice -= tradeDto.TotalQuantity * share.Price;
                _tradeRepository.Update(existingTrade);

            }
            else
            {
                //var trade = new Trade
                //{
                //    PortfolioId = tradeDto.PortfolioId,
                //    ShareId = share.Id,
                //    TotalQuantity = tradeDto.TotalQuantity,
                //    TradeTime = DateTime.UtcNow,
                //    TradeType = tradeDto.TradeType,
                //    TotalPrice = (tradeDto.TotalQuantity * share.Price),
                //};
                //_tradeRepository.Add(trade);
                return BadRequest("Share quantity not enough for this trade");
            }




            portfolio.TotalAmount += tradeDto.TotalQuantity * share.Price;
            share.Quantity += tradeDto.TotalQuantity;


            _portfolioRepository.Update(portfolio);
            _shareRepository.Update(share);

            //if (tradeDto.TotalQuantity == 0)
            //{
            //    _tradeRepository.Delete(tradeDto);
            //}
            return Ok();



        }
        [HttpGet("TradeHistory")]
        public ActionResult<Trade> GetTradeHistoryById(int? portfolioId, int? shareId, TradeType? tradeType)
        {
            var tradeHistory = _tradeRepository.GetTradeHistory(portfolioId, shareId, tradeType);
            return Ok(tradeHistory);
        }
    }
}