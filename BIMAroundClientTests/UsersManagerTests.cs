using System.Linq;
using BIMAroundClient;
using BIMAroundClient.Interfaces;
using BIMAroundClient.ObjectModel.Projects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BIMAroundClientTests
{
    [TestClass()]
    public class UsersManagerTests
    {
        public TestContext TestContext { get; set; }
        private static string Login => "localUser";
        private static string Password => "Qwerty123";
        private static string ClientUrl => "http://localhost:8080/api";

        private readonly IAuthorize _authorize = new Authorize();
        private readonly IProjectManager _projectManager = new ProjectManager();
        private readonly IUsersManager _usersManager = new UsersManager();

        [TestMethod()]
        private string GetToken()
        {
            return _authorize.GetToken(Login, Password, ClientUrl);
        }
        private Project GetProject()
        {
            var allProjects = _projectManager.GetProjects(GetToken(), ClientUrl);
            var project = allProjects.FirstOrDefault();
            return project;
        }
        [TestMethod()]
        public void GetProjectUsersTest()
        {
            var users = _usersManager.GetProjectUsers(GetToken(), GetProject().code, ClientUrl);
            if (users.Count >= 1)
            {
                foreach (var projectUsers in users)
                {
                    TestContext.WriteLine($"project code: {projectUsers.project.code}, user: {projectUsers.user.login}");
                }
            }
            else
            {
                Assert.Fail("нету пользователей в проекте");
            }
        }
    }
}