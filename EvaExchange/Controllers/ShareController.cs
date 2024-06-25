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
    public class ShareController : ControllerBase
    {
        
        private readonly IShareRepository _shareRepository;
        

        public ShareController(IShareRepository shareRepository)
        {
            _shareRepository = shareRepository;
            
        }
        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Share>>> GetAll()
        {
            return _shareRepository.GetAll();
        }
        [HttpGet("{id:int}")]
        public Share GetShareToId(int id)
        {
            return _shareRepository.GetById(id);
        }

        [HttpPost("UpdatePrice")]
        public async Task<ActionResult<Share>> UpdatePrice(int id, decimal price)
        {
            var share = _shareRepository.GetById(id);
            
            
            
            if(share == null)
            {
                return NotFound("Share not found");
            }
            var time = ((DateTime.UtcNow.Hour) - (share.PriceChangeDate.Hour));
            if (time <= 1)
            {
                return NotFound("Share change time can't be less to 1 hour");
            }

            share.PriceChangeDate = DateTime.UtcNow;
            share.Price = price;
            _shareRepository.Update(share);
            return Ok(share);
            
        }
        [HttpPost("AddShare")]
        public async Task<ActionResult<Share>> Add(ShareDto shareDto) {

            var share = new Share
            {
                Symbol = shareDto.Symbol.ToUpper(),
                Price = shareDto.Price,
                Quantity = shareDto.Quantity,
                PriceChangeDate = DateTime.UtcNow,
            };
            _shareRepository.Add(share);
            return CreatedAtAction(nameof(GetShareToId),new {id = share.Id},share);
        }
    }
}
