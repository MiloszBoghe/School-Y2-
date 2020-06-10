using Microsoft.EntityFrameworkCore;
using Stage_API.Business.Services.Mail.MailService;
using Stage_API.Data.IRepositories;
using Stage_API.Domain.Classes;
using Stage_API.Domain.enums;
using Stage_API.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stage_API.Data.Repositories
{
    public class StagevoorstelRepository : GenericRepository<Stagevoorstel>, IStagevoorstelRepository
    {
        private readonly StageContext _context;
        private readonly IMailService _mailService;

        public StagevoorstelRepository(StageContext context, IMailService mailService) : base(context)
        {
            _context = context;
            _mailService = mailService;
        }

        public override Stagevoorstel GetById(int id)
        {
            var stagevoorstel = _context.Stagevoorstellen.Where(s => s.Id == id)
                .Include(s => s.Bedrijf)
                .Include(s => s.Reviews)
                .Include(s => s.ReviewersToegewezen).ThenInclude(r => r.Reviewer)
                .Include(s => s.ReviewersFavorieten).ThenInclude(r => r.Reviewer)
                .Include(s => s.StudentenFavorieten).ThenInclude(s => s.Student)
                .Include(s => s.Comments).ThenInclude(c => c.User)
                .Include(s => s.StudentenToegewezen).FirstOrDefault();

            return stagevoorstel;
        }

        public bool PatchStatus(int id, int status)
        {
            var stagevoorstel = _context.Stagevoorstellen.Where(s => s.Id == id)
                .Include(s => s.Bedrijf).ThenInclude(b => b.Contactpersoon)
                .Include(s => s.Reviews).ThenInclude(r => r.Reviewer)
                .FirstOrDefault();

            if (stagevoorstel == null) return false;

            var oldStatus = stagevoorstel.Status;
            stagevoorstel.Status = (BeoordelingStatus)status;

            if ((BeoordelingStatus)status != oldStatus && status != 0) _mailService.SendMail(stagevoorstel, oldStatus);

            Save();
            return true;
        }

        public override void Add(Stagevoorstel entity)
        {
            var bedrijf = _context.Bedrijven.Find(entity.BedrijfId);
            entity.Date = DateTime.Now;
            entity.Adres = bedrijf.Adres;
            entity.StagePostcode = bedrijf.Postcode;
            entity.Gemeente = bedrijf.Gemeente;
            entity.StageITMedewerkers = bedrijf.AantalITMedewerkers;
            base.Add(entity);
        }

        public override bool Update(int id, Stagevoorstel stagevoorstel)
        {
            var exists = _context.Stagevoorstellen.AsNoTracking().FirstOrDefault(s => s.Id == id) != null;
            if (!exists) return false;
            if (stagevoorstel.ReviewersToegewezen == null)
            {
                base.Update(id, stagevoorstel);
                return true;
            }

            UpdateManyToMany(stagevoorstel);
            return true;
        }



        public void RemoveRange(Stagevoorstel[] stagevoorstellen)
        {
            foreach (var voorstel in stagevoorstellen)
            {
                Remove(voorstel);
            }
        }

        private void UpdateManyToMany(Stagevoorstel newStagevoorstel)
        {
            var originalVoorstel = _context.Stagevoorstellen
                .Include(s => s.StudentenToegewezen)
                .Include(s => s.ReviewersToegewezen).First(s => s.Id == newStagevoorstel.Id);

            //Remove all toegewezen reviewers
            originalVoorstel.ReviewersToegewezen = new List<ReviewerStagevoorstelToegewezen>();
            Save();

            if (!newStagevoorstel.ReviewersToegewezen.Any()) return;

            //clear context and add updated list of toegewezen reviewers.
            _context.DetachEntries();
            newStagevoorstel.ReviewersToegewezen.ToList()
                .ForEach(relation => _context.Entry(relation).State = EntityState.Added);
            Save();

        }

    }
}

