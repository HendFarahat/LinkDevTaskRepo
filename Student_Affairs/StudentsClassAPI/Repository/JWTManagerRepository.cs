using Microsoft.IdentityModel.Tokens;
using Students.DataAccess.Data;
using StudentsClassAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentsClassAPI.Repository
{
    public class JWTManagerRepository: IJWTManagerRepository
    {

        private ApplicationDbContext _db;
        private readonly IConfiguration iconfiguration;
        public JWTManagerRepository(IConfiguration iconfiguration, ApplicationDbContext db)
        {
            this.iconfiguration = iconfiguration;
            _db = db;
        }
        public Tokens Authenticate(Users users)
        {
            var User = _db.Users.FirstOrDefault(a => a.UserName == users.Name && a.PasswordHash == users.Password);
            if (User == null)
            {
                return null;
            }

            // Else  generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();//create token
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
             new Claim(ClaimTypes.Name, users.Name)
              }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };

        }












    }




}
