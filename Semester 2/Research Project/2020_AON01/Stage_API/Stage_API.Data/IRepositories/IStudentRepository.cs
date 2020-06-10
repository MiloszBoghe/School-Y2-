using Stage_API.Business.Models;
using Stage_API.Domain.Classes;

namespace Stage_API.Data.IRepositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        bool UpdateToegewezen(int id, StudentModel student);
        bool UpdateFavorieten(int id, StudentModel student);
    }
}
