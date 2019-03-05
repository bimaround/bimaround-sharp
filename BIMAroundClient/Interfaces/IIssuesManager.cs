
using System.Collections.Generic;
using BIMAroundClient.ObjectModel;

namespace BIMAroundClient.Interfaces
{
    public interface IIssuesManager
    {
        List<Issue> GetIssues(string token, string projectCode);
    }
}
