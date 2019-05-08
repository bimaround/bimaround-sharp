using System;
using System.Linq;
using BIMAroundClient;
using BIMAroundClient.Interfaces;
using BIMAroundClient.ObjectModel.Issues;
using BIMAroundClient.ObjectModel.Projects;
using BIMAroundClient.ObjectModel.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BIMAroundClientTests
{
    [TestClass()]
    public class IssuesManagerTests
    {
        public TestContext TestContext { get; set; }
        private static string Login => "localUser";//locallly created user
        private static string Password => "Qwerty123";
        private static string ClientUrl => "http://localhost:8080/api";

        private readonly IAuthorize _authorize = new Authorize();
        private readonly IProjectManager _projectManager = new ProjectManager();
        private readonly IIssuesManager _issuesManager = new IssuesManager();

        User testUser1 = new User()
        {
            accountType = "",
            email = "",
            id = 1,
            login = ""
        };

        [TestMethod()]
        private string GetToken()
        {
            return _authorize.GetToken(Login, Password, ClientUrl);
        }
        [TestMethod()]
        private Project GetProject()
        {
            var allProjects = _projectManager.GetProjects(GetToken(), ClientUrl);
            var project = allProjects.FirstOrDefault();
            return project;
        }

        [TestMethod()]
        public void GetIssuesTest()
        {
            var issues = _issuesManager.GetIssues(GetToken(), GetProject().code);

            if(issues != null)
            {
                foreach (var issue in issues)
                {
                    TestContext.WriteLine($"{issue.iid} {issue.title}\n");
                }
            }
            else
            {
                Assert.Fail("Issues NOT FOUND!!!");
            }
        }

        [TestMethod()]
        public void CreateIssueTest()
        {
            var testIssue = new Issue()
            {
                title = $"test issue {DateTime.Now}",
                //assignee = testUser1,//todo edit test user
                dueDate = DateTime.Now.AddDays(10),
                x = 100,
                y = 150,
                z = 200
            };
            var result = _issuesManager.CreateIssue(GetToken(), GetProject().code, testIssue, ClientUrl);

            if (result.iid != null && result.title == testIssue.title)
            {
                TestContext.WriteLine("new issue iid = " + result.iid + " title= " + result.title);
                _issuesManager.DeleteIssue(GetToken(), GetProject().code, result.iid, ClientUrl);
            }
            else
            {
                Assert.Fail("Create Issue Failed");
            }
            
        }

        [TestMethod()]
        public void GetIssueByIdTest()
        {
            var testIssue = new Issue(){title = $"new test issue for find by id {DateTime.Now}"};
            var createdIssue = _issuesManager.CreateIssue(GetToken(), GetProject().code, testIssue, ClientUrl);

            if (createdIssue != null && createdIssue.title == testIssue.title)
            {
                var result = _issuesManager.GetIssueById(GetToken(), GetProject().code, createdIssue.iid, ClientUrl);
                if (result != null && result.iid == createdIssue.iid)
                {
                    TestContext.WriteLine("Succsessful!");
                    _issuesManager.DeleteIssue(GetToken(), GetProject().code, result.iid, ClientUrl);
                }
                else
                {
                    Assert.Fail("backend did not return issue");
                }
            }
            else
            {
                Assert.Fail("failed при создании задачи для метода GetIssueById");
            }
        }

        [TestMethod()]
        public void UpdateIssueTest()
        {
            var testIssue = new Issue()
            {
                title = $"new test issue for find by id {DateTime.Now}",
                dueDate = DateTime.Now
            };
            var createdIssue = _issuesManager.CreateIssue(GetToken(), GetProject().code, testIssue, ClientUrl);

            if (createdIssue != null && createdIssue.title == testIssue.title)
            {
                createdIssue.title += $" edited for update {DateTime.Now}";
                createdIssue.dueDate = DateTime.Now.AddDays(1);
                var updatedIssue = _issuesManager.UpdateIssue(GetToken(), GetProject().code, createdIssue, ClientUrl);
                if (updatedIssue != null && updatedIssue.title == createdIssue.title && updatedIssue.dueDate == createdIssue.dueDate)
                {
                    TestContext.WriteLine("Updated succsessful");
                    _issuesManager.DeleteIssue(GetToken(), GetProject().code, updatedIssue.iid, ClientUrl);
                }
                else
                {
                    Assert.Fail("update not working");
                }
            }
            else
            {
                Assert.Fail("failed при создании задачи для метода UpdateIssue");
            }
        }

        [TestMethod()]
        public void CloseIssueTest()
        {
            var testIssue = new Issue() { title = $"new test issue for find by id {DateTime.Now}" };
            var createdIssue = _issuesManager.CreateIssue(GetToken(), GetProject().code, testIssue, ClientUrl);

            if (createdIssue != null && createdIssue.title == testIssue.title)
            {
                var closedIssue = _issuesManager.CloseIssue(GetToken(), GetProject().code, createdIssue.iid, ClientUrl);

                if (closedIssue != null && closedIssue.status != createdIssue.status)
                {
                    TestContext.WriteLine("Closed succsessful");
                    _issuesManager.DeleteIssue(GetToken(), GetProject().code, closedIssue.iid, ClientUrl);
                }
                else
                {
                    Assert.Fail("test issue didn't close");
                }
            }
            else
            {
                Assert.Fail("failed при создании задачи для метода CloseIssue");
            }
        }

        [TestMethod()]
        public void DeleteIssueTest()
        {
            var testIssue = new Issue() { title = $"new test issue for find by id {DateTime.Now}" };
            var createdIssue = _issuesManager.CreateIssue(GetToken(), GetProject().code, testIssue, ClientUrl);

            if (createdIssue != null && createdIssue.title == testIssue.title)
            {
                _issuesManager.DeleteIssue(GetToken(), GetProject().code, createdIssue.iid, ClientUrl);
                var deletedIssue =
                    _issuesManager.GetIssueById(GetToken(), GetProject().code, createdIssue.iid, ClientUrl);
                if (deletedIssue.iid == null && deletedIssue.title == null)
                {
                    TestContext.WriteLine($"deleted succsessful!");
                }
                else
                {
                    Assert.Fail($" Issue with iid: {deletedIssue.iid} & title: \"{deletedIssue.title}\" NOT DELETED" );
                }
            }
            else
            {
                Assert.Fail("new test issue not created for delete testing in method DeleteIssueTest");
            }
        }
    }
}