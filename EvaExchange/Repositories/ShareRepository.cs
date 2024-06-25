using EvaExchange.Data;
using EvaExchange.Entities;
using EvaExchange.Interfaces;

namespace EvaExchange.Repositories
{
    public class ShareRepository : IShareRepository
    {
        private readonly EvaDbContext _context;

        public ShareRepository(EvaDbContext context)
        {
            _context = context;
        }

        public void Add(Share share)
        {
            _context.Add(share);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Remove(id);
            _context.SaveChanges();
        }

        public List<Share> GetAll()
        {
            var shares = _context.Shares.ToList();
            return shares;
        }

        public Share GetById(int id)
        {
            var share = _context.Shares.Find(id);
            return share;
        }

        public void Update(Share share)
        {
            _context.Shares.Update(share);
            _context.SaveChanges();
        }

        public void UpdatePrice(Share share)
        {
            _context.Shares.Update(share);
            _context.SaveChanges();
        }
    }
}
