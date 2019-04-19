using System;
using System.IO;
using System.Linq;
using BIMAroundClient;
using BIMAroundClient.Interfaces;
using BIMAroundClient.ObjectModel.Attachments;
using BIMAroundClient.ObjectModel.Issues;
using BIMAroundClient.ObjectModel.Projects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BIMAroundClientTests
{
    [TestClass()]
    public class AttacmentsManagerTests
    {
        public TestContext TestContext { get; set; }
        private static string Login => "localUser";//locallly created user
        private static string Password => "Qwerty123";
        private static string ClientUrl => "http://localhost:8080/api";

        private readonly IAuthorize _authorize = new Authorize();
        private readonly IProjectManager _projectManager = new ProjectManager();
        private readonly IIssuesManager _issuesManager = new IssuesManager();
        private readonly IAttachmentManager _attachmentManager = new AttacmentsManager();

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
        private Issue GetIssue()
        {
            var allIssues = _issuesManager.GetIssues(GetToken(), GetProject().code, ClientUrl);
            var issue = allIssues.FirstOrDefault();
            return issue;
        }

        private Attachment GetTestAttachment()
        {
            return _attachmentManager.UploadAttachments(GetToken(), GetProject().code, GetIssue().iid, "C:/Users/Admin/Documents/Capture.PNG", ClientUrl);
        }

        [TestMethod]
        public void DownloadAttachmentTest()
        {
            var attachments = _attachmentManager.GetAttachments(GetToken(), GetProject().code, GetIssue().iid, ClientUrl);
            if (attachments.Count >= 1)
            {
                foreach (var attachment in attachments)
                {
                    _attachmentManager.DownloadAttcahment(GetToken(), GetProject().code, GetIssue().iid, attachment, ClientUrl);
                    TestContext.WriteLine($"{attachment.id}   {attachment.fileName} downloaded successful!");
                }
            }
            else
            {
                Assert.Fail("attachments not found");
            }
        }

        [TestMethod]
        public void GetAttachmentsTest()
        {
            var attachments = _attachmentManager.GetAttachments(GetToken(), GetProject().code, GetIssue().iid, ClientUrl);
            if (attachments.Count >= 1)
            {
                foreach (var attachment in attachments)
                {
                    TestContext.WriteLine($"{attachment.id}   {attachment.fileName}");
                }
            }
            else
            {
                Assert.Fail("attachments not found");
            }
        }

        [TestMethod()]
        public void UploadAttachmentsTest()
        {
            _attachmentManager.UploadAttachments(GetToken(), GetProject().code, GetIssue().iid, "C:/Users/Admin/Documents/Capture.PNG", ClientUrl);
        }
        
        [TestMethod]
        public void DeleteAttachmentTest()
        {
            //TODO еще не доделан
            _attachmentManager.DeleteAttachment(GetToken(), GetProject().code, GetIssue().iid, GetTestAttachment(), ClientUrl);
        }
    }
}