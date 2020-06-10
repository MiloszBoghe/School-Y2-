namespace Stage_API.Business.Authorization
{
    public class UserObject
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public bool IsCoordinator { get; set; }
        public UserObject()
        {

        }
        public UserObject(int id, string role)
        {
            Id = id;
            Role = role;
            IsCoordinator = Role == "coordinator";
        }
    }
}
