using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarRentalSystem.Models;
using CarRentalSystem.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CarRentalSystem.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtSecretKey;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository, string jwtSecretKey)
        {
            _userRepository = userRepository;
            _jwtSecretKey = jwtSecretKey;
            _passwordHasher = new PasswordHasher<User>();
        }

        public bool RegisterUser(User user)
        {
            var existingUser = _userRepository.GetUserByEmail(user.Email);
            if (existingUser != null)
                return false;

            user.Password = _passwordHasher.HashPassword(user, user.Password);
            _userRepository.AddUser(user);
            return true;
        }

        public string AuthenticateUser(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null) return null;

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (verificationResult != PasswordVerificationResult.Success) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
