using StudentsClassAPI.Models;

namespace StudentsClassAPI.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);
    }
}
