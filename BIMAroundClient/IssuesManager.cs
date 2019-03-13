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
            var client = Client;
            var request = new RestRequest("/projects/{projectCode}/issues");
            request.AddUrlSegment("projectCode", projectCode);
            request.AddHeader("Authorization", "Bearer " + token);

            var restResponse = client.Execute<GetIssuesResponse>(request);

            var issues = restResponse.Data;

            return issues.content;
        }

        public Issue CreateIssue(string token, string projectCode, Issue issue)
        {
            var client = Client;
            var createIssueRequest = new CreateIssueRequest {title = issue.title};

            var request = new RestRequest("/projects/{projectCode}/issues") {Method = Method.POST};
            request.AddUrlSegment("projectCode", projectCode);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddJsonBody(createIssueRequest);

            var response = client.Execute<Issue>(request);

            return response.Data;
        }

        public Issue GetIssueById(string token, string projectCode, string issueIid)
        {
            var client = Client;
            var request = new RestRequest("/projects/{projectCode}/issues/{iid}");
            request.AddUrlSegment("projectCode", projectCode);
            request.AddUrlSegment("iid", issueIid);
            request.AddHeader("Authorization", "Bearer " + token);

            var response = client.Execute<Issue>(request);

            return response.Data;
        }

        public Issue UpdateIssue(string token, string projectCode, Issue issue)
        {
            var client = Client;
            var request = new RestRequest("/projects/{projectCode}/issues/{iid}"){ Method = Method.PUT };
            request.AddUrlSegment("projectCode", projectCode);
            request.AddUrlSegment("iid", issue.iid);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddJsonBody(issue);

            var response = client.Execute<Issue>(request);

            return response.Data;
        }

        public Issue CloseIssue(string token, string projectCode, string issueIid)
        {
            var client = Client;
            var request = new RestRequest("/projects/{projectCode}/issues/{iid}/close") { Method = Method.POST };
            request.AddUrlSegment("projectCode", projectCode);
            request.AddUrlSegment("iid", issueIid);
            request.AddHeader("Authorization", "Bearer " + token);

            var response = client.Execute<Issue>(request);

            return response.Data;
        }

        public string DeleteIssue(string token, string projectCode, string issueIid)
        {
            var client = Client;
            var request = new RestRequest("/projects/{projectCode}/issues/{iid}") { Method = Method.DELETE };
            request.AddUrlSegment("projectCode", projectCode);
            request.AddUrlSegment("iid", issueIid);
            request.AddHeader("Authorization", "Bearer " + token);

            var response = client.Execute<Issue>(request);

            return response.StatusCode.ToString();
        }

        private RestClient Client => new RestClient("https://bimaround.com/api");
    }
}
