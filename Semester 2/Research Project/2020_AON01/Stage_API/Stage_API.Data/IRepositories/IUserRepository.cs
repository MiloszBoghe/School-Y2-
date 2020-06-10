using Stage_API.Business.Models;
using Stage_API.Domain;

namespace Stage_API.Data.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        bool UpdateUser(int id, ProfileModel entity);
        bool UpdateBedrijf(int id, ProfileModelBedrijf entity);
        bool PatchEmailConfirmed(int id, bool newEmailConfirmed);
    }
}
