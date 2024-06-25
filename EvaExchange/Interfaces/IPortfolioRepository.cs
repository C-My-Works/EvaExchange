using EvaExchange.Entities;

namespace EvaExchange.Interfaces
{
    public interface IPortfolioRepository
    {
        public List<Portfolio> GetAll();
        public Portfolio GetById(int id);

        public void Add(Portfolio portfolio);
        public void Update(Portfolio portfolio);
        public void Delete(int id);
    }
}
