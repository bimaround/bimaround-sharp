using System.Collections.Generic;
using BIMAroundClient.ObjectModel.Issues;

namespace BIMAroundClient.Interfaces
{
    public interface IIssuesManager
    {
        List<Issue> GetIssues(string token, string projectCode, string clientUrl = "https://bimaround.com/api");

        Issue CreateIssue(string token, string projectCode, Issue issue, string clientUrl = "https://bimaround.com/api");

        Issue GetIssueById(string token, string projectCode, string issueIid, string clientUrl = "https://bimaround.com/api");

        Issue UpdateIssue(string token, string projectCode, Issue issue, string clientUrl = "https://bimaround.com/api");

        Issue CloseIssue(string token, string projectCode, string issueIid, string clientUrl = "https://bimaround.com/api");

        void DeleteIssue(string token, string projectCode, string issueIid, string clientUrl = "https://bimaround.com/api");
    }
}
