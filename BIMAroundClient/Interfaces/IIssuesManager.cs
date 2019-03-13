using System.Collections.Generic;
using BIMAroundClient.ObjectModel;

namespace BIMAroundClient.Interfaces
{
    public interface IIssuesManager
    {
        List<Issue> GetIssues(string token, string projectCode);

        Issue CreateIssue(string token, string projectCode, Issue issue);

        Issue GetIssueById(string token, string projectCode, string issueIid);

        Issue UpdateIssue(string token, string projectCode, Issue issue);

        Issue CloseIssue(string token, string projectCode, string issueIid);

        string DeleteIssue(string token, string projectCode, string issueIid);
    }
}
