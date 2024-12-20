using ERP_Genie_Model;
using ERP_Genie_Model.DTO;
using ERP_Genie_Model.Model;
using ERP_Genie_Service.Contract.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ERP_Genie_Service.Contract.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _jwtSecret;
        private readonly int _jwtTokenLifetime;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _jwtSecret = "JWTAuthenticationHIGHsecuredPassword";
            _jwtTokenLifetime = 60;
        }

        public async Task<BaseReponse<string>> Login(LoginModel login)
        {
            if (login == null || string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
                return new BaseReponse<string> { IsSuccess = false, Message = ConstantString.InvalidData };

            // Retrieve the user by email or username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Username || u.Username == login.Username);
            if (user == null)
                return new BaseReponse<string> { IsSuccess = false, Message = ConstantString.InvalidUserPass };

            // Hash the entered password and compare it with the stored hash
            var enteredPasswordHash = HashPassword(login.Password);
            if (enteredPasswordHash != user.Password)
                return new BaseReponse<string> { IsSuccess = false, Message = ConstantString.InvalidUserPass };

            // Generate JWT token
            var token = GenerateJwtToken(user);
            return new BaseReponse<string> { IsSuccess = true, Data = token, Message = ConstantString.LoginSuccess };
        }

        public async Task<BaseReponse<string>> Logout()
        {
            return await Task.FromResult(new BaseReponse<string>
            {
                IsSuccess = true,
                Message = ConstantString.LogOutSuccess
            });
        }

        public async Task<BaseReponse<string>> Register(SignupModel signup)
        {
            if (signup == null || string.IsNullOrEmpty(signup.Username) || string.IsNullOrEmpty(signup.Email))
                return new BaseReponse<string> { IsSuccess = false, Message = ConstantString.InvalidData };

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == signup.Username || u.Email == signup.Email);

            if (existingUser != null)
                return new BaseReponse<string> { IsSuccess = false, Message = ConstantString.DataExist };

            var user = new User
            {
                Password = HashPassword(signup.Password),
                Email = signup.Email,
                Username = signup.Username,
                FullName = signup.FullName,
                DateOfBirth = signup.DateOfBirth,
                LastLogin = DateTime.UtcNow
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new BaseReponse<string> { IsSuccess = true, Message = ConstantString.RegisterSucccess };
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.UserID.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtTokenLifetime),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            var enteredPasswordHash = HashPassword(enteredPassword);
            return enteredPasswordHash == storedPasswordHash;
        }
    }
}
