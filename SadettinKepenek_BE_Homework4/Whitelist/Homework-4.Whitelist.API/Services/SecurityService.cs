using System.Linq;

namespace Homework_4.Whitelist.API.Services
{
    public class SecurityService:ISecurityService
    {
        private readonly IUserRestrictionService _userRestrictionService;

        public SecurityService(IUserRestrictionService userRestrictionService)
        {
            _userRestrictionService = userRestrictionService;
        }

        public bool CanAccessController(string ipAddress,string controllerName)
        {
            var userIp = _userRestrictionService.GetUserIp(u => u.IpAddress.Equals(ipAddress));
            if (userIp == null)
            {
                return false;
            }
            var userRestrictions = _userRestrictionService.GetUserRestrictions(u => u.UserIpId == userIp.Id);
            return userRestrictions.Any(u => u.ControllerName.Equals(controllerName));
        }
    }
}