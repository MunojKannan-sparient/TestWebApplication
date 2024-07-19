using Microsoft.AspNetCore.Identity;

namespace TestWebApplication.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
