using OngProject.Entities;

namespace OngProject.Core.Helper.Interface
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}

