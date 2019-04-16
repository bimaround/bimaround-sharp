using System.Collections.Generic;
using BIMAroundClient.ObjectModel.Projects;

namespace BIMAroundClient.Interfaces
{
    public interface IProjectManager
    {
        List<Project> GetProjects(string token, string clientUrl = "https://bimaround.com/api");
    }
}
