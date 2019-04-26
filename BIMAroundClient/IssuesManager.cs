using System.Collections.Generic;
using System.Net;
using BIMAroundClient.Interfaces;
using BIMAroundClient.ObjectModel.Issues;
using RestSharp;

namespace BIMAroundClient
{
    public class IssuesManager : IIssuesManager
    {
        public List<Issue> GetIssues(string token, string projectCode, string clientUrl)
        {
            var client = new RestClient(clientUrl);
            var request = new RestRequest("/projects/{projectCode}/issues");
            request.AddUrlSegment("projectCode", projectCode);
            request.AddHeader("Authorization", "Bearer " + token);

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 |
                                                   (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            var restResponse = client.Execute<GetIssuesResponse>(request);

            var issues = restResponse.Data;

            return issues.content;
        }

        public Issue CreateIssue(string token, string projectCode, Issue issue, string clientUrl)
        {
            var client = new RestClient(clientUrl);
            var createIssueRequest = new CreateIssueRequest
            {
                title = issue.title,
                assignee = issue.assignee,
                dueDate = issue.dueDate
            };

            var request = new RestRequest("/projects/{projectCode}/issues") {Method = Method.POST};
            request.AddUrlSegment("projectCode", projectCode);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddJsonBody(createIssueRequest);

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 |
                                                   (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            var response = client.Execute<Issue>(request);

            return response.Data;
        }

        public Issue GetIssueById(string token, string projectCode, string issueIid, string clientUrl)
        {
            var client = new RestClient(clientUrl);
            var request = new RestRequest("/projects/{projectCode}/issues/{iid}");
            request.AddUrlSegment("projectCode", projectCode);
            request.AddUrlSegment("iid", issueIid);
            request.AddHeader("Authorization", "Bearer " + token);

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 |
                                                   (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            var response = client.Execute<Issue>(request);

            return response.Data;
        }

        public Issue UpdateIssue(string token, string projectCode, Issue issue, string clientUrl)
        {
            var client = new RestClient(clientUrl);
            var request = new RestRequest("/projects/{projectCode}/issues/{iid}"){ Method = Method.PUT };
            request.AddUrlSegment("projectCode", projectCode);
            request.AddUrlSegment("iid", issue.iid);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddJsonBody(issue);

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 |
                                                   (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            var response = client.Execute<Issue>(request);

            return response.Data;
        }

        public Issue CloseIssue(string token, string projectCode, string issueIid, string clientUrl)
        {
            var client = new RestClient(clientUrl);
            var request = new RestRequest("/projects/{projectCode}/issues/{iid}/close") { Method = Method.POST };
            request.AddUrlSegment("projectCode", projectCode);
            request.AddUrlSegment("iid", issueIid);
            request.AddHeader("Authorization", "Bearer " + token);

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 |
                                                   (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            var response = client.Execute<Issue>(request);

            return response.Data;
        }

        public void DeleteIssue(string token, string projectCode, string issueIid, string clientUrl)
        {
            var client = new RestClient(clientUrl);
            var request = new RestRequest("/projects/{projectCode}/issues/{iid}") { Method = Method.DELETE };
            request.AddUrlSegment("projectCode", projectCode);
            request.AddUrlSegment("iid", issueIid);
            request.AddHeader("Authorization", "Bearer " + token);

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 |
                                                   (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            client.Execute(request);
        }
    }
}
