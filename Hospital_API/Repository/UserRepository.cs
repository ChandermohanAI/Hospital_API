using Hospital_API.Data;
using Hospital_API.Model;
using Hospital_API.Model.DTO.LoginRegister;
using Hospital_API.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hospital_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;
        public UserRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey= configuration.GetValue<string>("ApiSettings:Secret");
        }

        // To check if the user exists or not
        public bool IsUniqueUser(string username)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        // This method will check if the user exists or not
        // If the user exists it will create a JWT token
        // Else it will return Null as a Token
        public async Task<LoginResponse_DTO> Login(LoginRequest_DTO loginReq)
        {
            var user = _db.Users.FirstOrDefault(u=>u.UserName.ToLower()==loginReq.UserName.ToLower() 
            && u.Password == loginReq.Password);
            if (user == null)
            {
                return new LoginResponse_DTO()
                {

                    Users = null,
                    Token = ""
                };
            }
            // JWT Key Logic
            // if user Exists
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponse_DTO loginResponseDTO = new LoginResponse_DTO()
            {
                
                Users = user,
                Token = tokenHandler.WriteToken(token)
            };

            return loginResponseDTO;
        }

        // This method will create a new user entry in the table 
        public async Task<User> Register(RegisterationRequestDTO registerationRequestDTO)
        {
 
            User user = new()
            {
                UserName = registerationRequestDTO.UserName,
                Password = registerationRequestDTO.Password,
                Role = registerationRequestDTO.Role

            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}
