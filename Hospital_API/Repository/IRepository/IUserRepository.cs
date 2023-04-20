using Hospital_API.Model;
using Hospital_API.Model.DTO.LoginRegister;

namespace Hospital_API.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponse_DTO> Login(LoginRequest_DTO loginRequestDTO);
        Task<User> Register(RegisterationRequestDTO registrationRequestDTO);
    }
}
