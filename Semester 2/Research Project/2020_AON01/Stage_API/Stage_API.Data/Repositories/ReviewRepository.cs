using Microsoft.EntityFrameworkCore;
using Stage_API.Business.Services.Mail.MailService;
using Stage_API.Data.IRepositories;
using Stage_API.Domain.Classes;
using Stage_API.Domain.enums;
using System.Collections.Generic;
using System.Linq;

namespace Stage_API.Data.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly StageContext _context;
        private readonly IMailService _mailService;

        public ReviewRepository(StageContext context, IMailService mailService) : base(context)
        {
            _context = context;
            _mailService = mailService;
        }


        public override void Add(Review review)
        {
            _context.Entry(review).State = EntityState.Added;
            Save();
        }

        public IEnumerable<Review> GetReviewsByVoorstel(int stagevoorstelId)
        {
            var review = _context.Reviews.Where(r => r.Stagevoorstel.Id == stagevoorstelId).Include(r => r.Reviewer);
            return review;
        }


        public bool PatchStatus(int id, int status)
        {

            var review = _context.Reviews.Where(r => r.Id == id).Include(r => r.Reviewer)
                .Include(r => r.Stagevoorstel)
                .ThenInclude(s => s.Bedrijf).ThenInclude(b => b.Contactpersoon).FirstOrDefault();

            if (review == null)
            {
                return false;
            }

            var oldStatus = review.Status;
            review.Status = (BeoordelingStatus)status;
            if ((BeoordelingStatus)status != oldStatus)
            {
                _mailService.SendMail(review, oldStatus);
            }
            Save();

            return true;
        }

    }
}
