using Stage_API.Business.Services.PasswordReset;
using Stage_API.Data.IRepositories;
using Stage_API.Domain;
using Stage_API.Domain.Classes;
using System;
using System.Linq;

namespace Stage_API.Data.Repositories
{
    public class ResetPasswordRequestRepository : GenericRepository<ResetPasswordRequest>, IResetPasswordRequestRepository
    {
        private readonly StageContext _context;
        private readonly IPasswordResetService _passwordResetService;
        public ResetPasswordRequestRepository(StageContext context, IPasswordResetService passwordResetService) : base(context)
        {
            _context = context;
            _passwordResetService = passwordResetService;
        }

        public void Add(User user)
        {
            ResetPasswordRequest nRequest = new ResetPasswordRequest
            {
                PasswordResetToken = Guid.NewGuid(),
                Email = user.Email,
                ResetRequestDateTime = DateTime.Now,
                IsConsumed = false
            };

            _passwordResetService.SendResetToken(user, nRequest);

            base.Add(nRequest);
        }
    }
}
