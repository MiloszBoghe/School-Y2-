namespace Stage_API.Business._Models
{
    public class ResetPassWordModel
    {
        public string NewPassWord { get; set; }

        public ResetPassWordModel()
        {

        }

        public ResetPassWordModel(string nPassWord)
        {
            NewPassWord = nPassWord;
        }
    }
}
