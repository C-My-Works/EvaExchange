using EvaExchange.Data;
using EvaExchange.Entities;
using EvaExchange.Interfaces;

namespace EvaExchange.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EvaDbContext _context;

        public UserRepository(EvaDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Remove(id);
            _context.SaveChanges();
        }

        public List<User> GetAll()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public User GetById(int id)
        {
            var user = _context.Users.Find(id);
            return user;
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
