using System.Collections.Generic;
using BIMAroundClient.Interfaces;
using BIMAroundClient.ObjectModel;
using RestSharp;

namespace BIMAroundClient
{
    public class IssuesManager : IIssuesManager
    {
        public List<Issue> GetIssues(string token, string projectCode)
        {
            var client = new RestClient("https://bimaround.com/api");
            var request = new RestRequest("/projects/{projectCode}/issues");
            request.AddUrlSegment("projectCode", projectCode);
            request.AddHeader("Authorization", "Bearer " + token);

            var restResponse = client.Execute<GetIssuesResponse>(request);

            var issues = restResponse.Data;

            return issues.content;
        }
    }
}
