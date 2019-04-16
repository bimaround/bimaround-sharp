using System.Collections.Generic;
using BIMAroundClient.ObjectModel.Users;

namespace BIMAroundClient.Interfaces
{
    public interface IUsersManager
    {
        List<ProjectUsers> GetProjectUsers(string token, string projectCode, string clientUrl = "https://bimaround.com/api");
    }
}
