using Stage_API.Business.Models;
using Stage_API.Domain.Classes;

namespace Stage_API.Data.IRepositories
{
    public interface IReviewerRepository : IGenericRepository<Reviewer>
    {
        bool UpdateFavorieten(int id, ReviewerModel reviewer);
    }
}
