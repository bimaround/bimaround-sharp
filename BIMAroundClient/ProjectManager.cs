using System;
using System.Collections.Generic;
using BIMAroundClient.Interfaces;
using BIMAroundClient.ObjectModel.Project;
using RestSharp;

namespace BIMAroundClient
{
    public class ProjectManager : IProjectManager
    {
        /// <summary>
        /// Basic authentication method. If you want to use a different client address, you need to pass the clientUrl
        /// </summary>
        /// <param name="token"></param>
        /// <param name="clientUrl"></param>
        /// <returns></returns>
        public List<Project> GetProjects(string token, string clientUrl)
        {
            try
            {
                var client = new RestClient(clientUrl);
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
