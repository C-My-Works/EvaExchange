using EvaExchange.Entities;

namespace EvaExchange.Interfaces
{
    public interface IShareRepository
    {
        public List<Share> GetAll();
        public Share GetById(int id);
       
        public void Add(Share share);
        public void Update(Share share);
        public void UpdatePrice(Share share);
        public void Delete(int id);
    }
}
