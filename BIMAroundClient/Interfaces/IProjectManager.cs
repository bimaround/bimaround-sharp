using System.Collections.Generic;
using BIMAroundClient.ObjectModel.Project;

namespace BIMAroundClient.Interfaces
{
    public interface IProjectManager
    {
        List<Project> GetProjects(string token, string clientUrl = "https://bimaround.com/api");
    }
}
