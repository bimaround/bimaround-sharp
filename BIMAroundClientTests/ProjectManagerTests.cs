using BIMAroundClient;
using BIMAroundClient.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BIMAroundClientTests
{
    [TestClass()]
    public class ProjectManagerTests
    {
        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }
        private static string Login => "localUser";
        private static string Password => "Qwerty123";
        private static string ClientUrl => "http://localhost:8080/api";

        private readonly IAuthorize _authorize = new Authorize();
        private readonly IProjectManager _projectManager = new ProjectManager();

        [TestMethod()]
        private string GetToken()
        {
            return _authorize.GetToken(Login, Password, ClientUrl);
        }
        
        [TestMethod()]
        public void GetProjectsTest()
        {
            var projects = _projectManager.GetProjects(GetToken());
            if (projects == null)
            {
                Assert.Fail("нет проектов");
            }
            else
            {
                foreach (var project in projects)
                {
                    TestContext.WriteLine("code= " + project.code + " name= " + project.name + "\n");
                }
            }
        }
    }
}