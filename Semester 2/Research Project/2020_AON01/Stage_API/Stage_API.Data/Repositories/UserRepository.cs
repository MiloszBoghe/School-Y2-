using Microsoft.EntityFrameworkCore;
using Stage_API.Business.Models;
using Stage_API.Data.IRepositories;
using Stage_API.Domain;
using System.Linq;

namespace Stage_API.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly StageContext _context;

        public UserRepository(StageContext context) : base(context)
        {
            _context = context;
        }


        public bool UpdateUser(int id, ProfileModel entity)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return false;
            }

            user.Voornaam = entity.Voornaam;
            user.Naam = entity.Naam;
            user.Email = entity.Email;
            user.NormalizedEmail = entity.Email.ToUpper();
            user.UserName = user.Email;
            user.NormalizedUserName = user.NormalizedEmail;
            Save();

            return true;
        }

        public bool UpdateBedrijf(int id, ProfileModelBedrijf entity)
        {
            var bedrijf = _context.Bedrijven.Where(b => b.Id == id).Include(b => b.Stagevoorstellen).FirstOrDefault();

            if (bedrijf == null)
            {
                return false;
            }

            foreach (var stagevoorstel in bedrijf.Stagevoorstellen)
            {
                stagevoorstel.Adres = entity.Adres;
                stagevoorstel.StagePostcode = entity.Postcode;
                stagevoorstel.Gemeente = entity.Gemeente;
                stagevoorstel.StageITMedewerkers = entity.AantalITMedewerkers;
            }

            bedrijf.Voornaam = null;
            bedrijf.Naam = entity.Naam;
            bedrijf.Email = entity.Email;
            bedrijf.NormalizedEmail = entity.Email.ToUpper();
            bedrijf.UserName = bedrijf.Email;
            bedrijf.NormalizedUserName = bedrijf.NormalizedEmail;
            bedrijf.Adres = entity.Adres;
            bedrijf.Gemeente = entity.Gemeente;
            bedrijf.Postcode = entity.Postcode;
            bedrijf.AantalMedewerkers = entity.AantalMedewerkers;
            bedrijf.AantalITMedewerkers = entity.AantalITMedewerkers;
            bedrijf.AantalBegeleidendeMedewerkers = entity.AantalBegeleidendeMedewerkers;
            bedrijf.Contactpersoon = entity.Contactpersoon;
            bedrijf.Bedrijfspromotor = entity.Bedrijfspromotor;
            Save();

            return true;
        }

        public bool PatchEmailConfirmed(int id, bool newEmailConfirmed)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return false;
            }

            user.EmailConfirmed = newEmailConfirmed;
            Save();

            return true;
        }

    }
}