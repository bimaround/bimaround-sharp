using System;
using System.Collections.Generic;
using BIMAroundClient.Interfaces;
using BIMAroundClient.ObjectModel.Users;
using RestSharp;

namespace BIMAroundClient
{
    public class UsersManager : IUsersManager
    {
        public List<ProjectUsers> GetProjectUsers(string token, string projectCode, string clientUrl)
        {
            try
            {
                var client = new RestClient(clientUrl);
                var request = new RestRequest("/projects/{projectCode}/users");
                request.AddUrlSegment("projectCode", projectCode);
                request.AddHeader("Authorization", "Bearer " + token);

                var response = client.Execute<List<ProjectUsers>>(request);
                return response.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new List<ProjectUsers>();
        }
    }
}
