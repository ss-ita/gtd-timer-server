using Common.ModelsDTO;

namespace ServiceTier.Services
{
    public interface ILogInService
    {
        string CreateToken(LoginDTO model);
        string CreateTokenWithGoogle(SocialAuthDTO accessToken);
        string CreateTokenWithFacebook(SocialAuthDTO accessToken);
    }
}
