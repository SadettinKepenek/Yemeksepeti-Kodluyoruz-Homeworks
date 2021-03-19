namespace Homework_4.Whitelist.API.Entities
{
    public class UserRestriction
    {
        public int Id { get; set; }
        public int UserIpId { get; set; }
        public string ControllerName { get; set; }
        
        
    }
}