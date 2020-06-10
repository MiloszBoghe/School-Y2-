using Stage_API.Domain.Classes;

namespace Stage_API.Data.IRepositories
{
    public interface IStagevoorstelRepository : IGenericRepository<Stagevoorstel>
    {
        bool PatchStatus(int id, int status);
        void RemoveRange(Stagevoorstel[] stagevoorstellen);
    }
}
