using Microsoft.AspNetCore.Http;
using Stage_API.Business.Authorization;

namespace Stage_API.Business.Interfaces
{
    public interface IRequestHelper
    {
        UserObject GetUser(HttpContext context);
    }
}
