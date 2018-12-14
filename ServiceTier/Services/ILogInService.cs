using Common.Model;

namespace ServiceTier.Services
{
    public interface ILogInService
    {
        string CreateToken(LoginModel model);
    }
}
