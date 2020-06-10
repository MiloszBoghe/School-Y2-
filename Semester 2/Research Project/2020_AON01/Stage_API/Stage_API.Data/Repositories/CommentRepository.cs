using Stage_API.Data.IRepositories;
using Stage_API.Domain.Classes;

namespace Stage_API.Data.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(StageContext context) : base(context)
        {

        }
    }
}
