using FundooNotes.API.Data;
using FundooNotes.API.ModelLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace FundooNotes.API.DataBaseLayer
{
    public class UserRepository
    {
        private readonly FundooContext _context;
        public UserRepository(FundooContext context)
        {
            _context = context;
        }

        public void Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
