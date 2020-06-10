using Microsoft.EntityFrameworkCore;
using Stage_API.Data.IRepositories;
using Stage_API.Domain.Classes;
using Stage_API.Domain.Relations;
using System.Collections.Generic;
using System.Linq;
using Stage_API.Business.Models;

namespace Stage_API.Data.Repositories
{
    public class ReviewerRepository : GenericRepository<Reviewer>, IReviewerRepository
    {
        private readonly StageContext _context;
        public ReviewerRepository(StageContext context) : base(context)
        {
            _context = context;
        }

        public override Reviewer GetById(int id)
        {
            //Gets the reviewer including ALL it's internship relations.
            var reviewer = _context.Reviewers
                .Include(r => r.VoorkeurVoorstellen)
                .ThenInclude(s => s.Stagevoorstel.StudentenFavorieten)
                .Include(r => r.VoorkeurVoorstellen)
                .ThenInclude(s => s.Stagevoorstel.Bedrijf)
                .Include(s=>s.VoorkeurVoorstellen)
                .ThenInclude(s => s.Stagevoorstel.Comments)
                .Include(r => r.ToegewezenVoorstellen)
                .ThenInclude(s => s.Stagevoorstel.StudentenFavorieten)
                .Include(r => r.ToegewezenVoorstellen)
                .ThenInclude(s => s.Stagevoorstel.Bedrijf)
                .Include(s=>s.ToegewezenVoorstellen)
                .ThenInclude(s=>s.Stagevoorstel.Comments)
                .FirstOrDefault(r => r.Id == id);

            return reviewer;
        }


        public override IEnumerable<Reviewer> GetAll()
        {
            var reviewers = _context.Reviewers
                .Include(r => r.VoorkeurVoorstellen)
                .ThenInclude(s => s.Stagevoorstel)
                .Include(r => r.ToegewezenVoorstellen)
                .ThenInclude(s => s.Stagevoorstel).ToList();

            return reviewers;
        }
        
        public bool UpdateFavorieten(int id, ReviewerModel reviewer)
        {
            var exists = _context.Reviewers.AsNoTracking().FirstOrDefault(s => s.Id == id) != null;
            if (!exists)
            {
                return false;
            }

            var originalReviewer = _context.Reviewers.Where(s => s.Id == reviewer.Id)
                 .Include(r => r.VoorkeurVoorstellen).FirstOrDefault();

            var newFavorieten = reviewer.VoorkeurVoorstellen.Select(fav => new ReviewerStagevoorstelFavoriet() { ReviewerId = reviewer.Id, StagevoorstelId = fav.Id }).ToList();

            UpdateFavorieten(originalReviewer, newFavorieten);

            Save();
            return true;
        }

        private void UpdateFavorieten(Reviewer originalReviewer, ICollection<ReviewerStagevoorstelFavoriet> newFavorieten)
        {
            //Remove all favoriete voorstellen
            originalReviewer.VoorkeurVoorstellen = new List<ReviewerStagevoorstelFavoriet>();
            Save();

            if (!newFavorieten.Any()) return;

            //clear context and add updated list of favoriete studenten.
            _context.DetachEntries();
            newFavorieten.ToList().ForEach(relation => _context.Entry(relation).State = EntityState.Added);
            Save();
        }
    }
}