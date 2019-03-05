using System.Collections.Generic;
using BIMAroundClient.ObjectModel;

namespace BIMAroundClient.Interfaces
{
    public interface IProjectManager
    {
        List<Project> GetProjects(string token);
    }
}
