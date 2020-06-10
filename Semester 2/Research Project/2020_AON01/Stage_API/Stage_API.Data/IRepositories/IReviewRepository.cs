using Stage_API.Domain.Classes;
using System.Collections.Generic;

namespace Stage_API.Data.IRepositories
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        IEnumerable<Review> GetReviewsByVoorstel(int stagevoorstelId);
        bool PatchStatus(int id, int status);
    }
}
