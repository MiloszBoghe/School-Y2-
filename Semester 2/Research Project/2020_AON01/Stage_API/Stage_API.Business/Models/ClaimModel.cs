using System.Collections.Generic;
using System.Security.Claims;

namespace Stage_API.Business.Models
{
    public class ClaimModel
    {
        public int Id { get; set; }
        public string Jti { get; set; }
        public string Email { get; set; }
        public string Voornaam { get; set; }
        public string Naam { get; set; }
        public string Role { get; set; }
        public string Exp { get; set; }
        public string Iss { get; set; }
        public string Aud { get; set; }

        public ClaimModel(IEnumerable<Claim> claims)
        {
            foreach (var c in claims)
            {
                switch (c.Type)
                {
                    case "id":
                        Id = int.Parse(c.Value);
                        break;
                    case "jti":
                        Jti = c.Value;
                        break;
                    case "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress":
                        Email = c.Value;
                        break;
                    case "voornaam":
                        Voornaam = c.Value;
                        break;
                    case "naam":
                        Naam = c.Value;
                        break;
                    case "http://schemas.microsoft.com/ws/2008/06/identity/claims/role":
                        Role = c.Value;
                        break;
                    case "exp":
                        Exp = c.Value;
                        break;
                    case "iss":
                        Iss = c.Value;
                        break;
                    case "aud":
                        Aud = c.Value;
                        break;

                }
            }
        }
    }
}
