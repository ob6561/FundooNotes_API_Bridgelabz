using FundooNotes.API.DataBaseLayer;
using FundooNotes.API.ModelLayer.Entities;
using FundooNotes.API.ModelLayer.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;


namespace FundooNotes.API.BusinessLayer
{
    public class UserService
    {
        private readonly UserRepository _repository;
        private readonly IConfiguration _configuration;

        public UserService(UserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        
        public bool Register(RegisterDto dto)
        {
            var existingUser = _repository.GetUserByEmail(dto.Email);
            if (existingUser != null)
                return false;

            User user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _repository.Register(user);
            return true;
        }

       
        public string Login(LoginDto dto)
        {
            var user = _repository.GetUserByEmail(dto.Email);
            if (user == null)
                return null;

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            if (!isValidPassword)
                return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"])
                ),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
