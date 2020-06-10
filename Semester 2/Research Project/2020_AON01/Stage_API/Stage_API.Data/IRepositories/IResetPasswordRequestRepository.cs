using Stage_API.Domain;
using Stage_API.Domain.Classes;

namespace Stage_API.Data.IRepositories
{
    public interface IResetPasswordRequestRepository : IGenericRepository<ResetPasswordRequest>
    {
        void Add(User user);
    }
}
