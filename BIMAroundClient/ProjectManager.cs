using System;
using System.Collections.Generic;
using BIMAroundClient.Interfaces;
using BIMAroundClient.ObjectModel;
using RestSharp;

namespace BIMAroundClient
{
    public class ProjectManager : IProjectManager
    {
        public List<Project> GetProjects(string token)
        {
            try
            {
                var client = new RestClient("https://bimaround.com/api");
                var request = new RestRequest("/projects");
                request.AddHeader("Authorization", "Bearer " + token);

                var restResponse = client.Execute<ProjectResponse>(request);

                var projects = new List<Project>();
                projects.AddRange(restResponse.Data.data);
                return projects;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
