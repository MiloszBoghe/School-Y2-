using Stage_API.Domain;
using Stage_API.Domain.Classes;
using System.Collections.Generic;
using System.Linq;

namespace Stage_API.Business.Models
{
    public class ReviewerModel
    {
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public ICollection<StagevoorstelModel> ToegewezenVoorstellen { get; set; }
        public ICollection<StagevoorstelModel> VoorkeurVoorstellen { get; set; }

        public ReviewerModel()
        {

        }

        public ReviewerModel(Reviewer reviewer, string role)
        {
            if (role == "coordinator")
            {
                Coordinator(reviewer);
            }
            else
            {
                Reviewer(reviewer, role);
            }

        }

        private void Coordinator(User user)
        {
            Id = user.Id;
            Voornaam = user.Voornaam;
            Naam = user.Naam;
            Email = user.Email;
        }

        private void Reviewer(Reviewer reviewer, string role)
        {
            Id = reviewer.Id;
            Voornaam = reviewer.Voornaam;
            Naam = reviewer.Naam;
            Email = reviewer.Email;
            ToegewezenVoorstellen = reviewer.ToegewezenVoorstellen?.Select(s => new StagevoorstelModel(s.Stagevoorstel, role)).ToList();
            VoorkeurVoorstellen = reviewer.VoorkeurVoorstellen?.Select(s => new StagevoorstelModel(s.Stagevoorstel, role)).ToList();

        }
    }
}
