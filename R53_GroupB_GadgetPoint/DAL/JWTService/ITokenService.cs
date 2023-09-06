using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DAL.JWTService
{
    public interface ITokenService
    {
         string CreateToken(AppUser user);
    }
}
