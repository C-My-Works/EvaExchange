using EvaExchange.Data;
using EvaExchange.Dto;
using EvaExchange.Entities;
using EvaExchange.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvaExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        
        private readonly IPortfolioRepository _portfolioRepository;
        

        public PortfolioController(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
            
        }
        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Portfolio>>> GetAll()
        {
            return _portfolioRepository.GetAll();
        }
        [HttpGet("{id:int}")]
        public Portfolio GetShareToId(int id)
        {
            return _portfolioRepository.GetById(id);
        }

        
            
        //[HttpPost("AddShare")]
        //public async Task<ActionResult<Portfolio>> Add(Portfolio portfolio) {

        //    var share = new Share
        //    {
        //        Symbol = shareDto.Symbol.ToUpper(),
        //        Price = shareDto.Price,
        //        Quantity = shareDto.Quantity,
        //        PriceChangeDate = DateTime.UtcNow,
        //    };
        //    _portfolioRepository.Add(portfolio);
        //    return CreatedAtAction(nameof(GetShareToId),new {id = share.Id},share);
        //}
    }
}
