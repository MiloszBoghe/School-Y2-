using Stage_API.Data.IRepositories;
using Stage_API.Domain.Classes;

namespace Stage_API.Data.Repositories
{
    public class BedrijfRepository : GenericRepository<Bedrijf>, IBedrijfRepository
    {
        public BedrijfRepository(StageContext context) : base(context)
        {

        }
    }
}