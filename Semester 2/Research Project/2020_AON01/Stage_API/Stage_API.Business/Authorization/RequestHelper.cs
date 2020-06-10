using Microsoft.AspNetCore.Http;
using Stage_API.Business.Interfaces;
using System.Linq;
using System.Security.Claims;


namespace Stage_API.Business.Authorization
{
    public class RequestHelper : IRequestHelper
    {
        public UserObject GetUser(HttpContext context)
        {
            var id = GetUserId(context);
            var role = GetRole(context);
            return new UserObject(id, role);
        }

        private int GetUserId(HttpContext context)
        {
            return int.Parse(context.User.FindFirst("id").Value);
        }

        private string GetRole(HttpContext context)
        {
            return context.User.Claims.First(x => x.Type == ClaimTypes.Role).Value;
        }
    }
}
