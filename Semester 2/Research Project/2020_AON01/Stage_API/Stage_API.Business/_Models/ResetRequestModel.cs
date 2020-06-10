namespace Stage_API.Business._Models
{
    public class ResetRequestModel
    {
        public string Email { get; set; }

        public ResetRequestModel()
        {

        }

        public ResetRequestModel(string email)
        {
            Email = email;
        }
    }
}
