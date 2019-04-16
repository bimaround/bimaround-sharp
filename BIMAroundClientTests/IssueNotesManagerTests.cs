using System;
using System.Linq;
using BIMAroundClient;
using BIMAroundClient.Interfaces;
using BIMAroundClient.ObjectModel.Issues;
using BIMAroundClient.ObjectModel.Notes;
using BIMAroundClient.ObjectModel.Projects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BIMAroundClientTests
{
    [TestClass()]
    public class IssueNotesManagerTests
    {
        public TestContext TestContext { get; set; }
        private static string Login => "localUser";
        private static string Password => "Qwerty123";
        private static string ClientUrl => "http://localhost:8080/api";

        private readonly IAuthorize _authorize = new Authorize();
        private readonly IProjectManager _projectManager = new ProjectManager();
        private readonly IIssuesManager _issuesManager = new IssuesManager();
        private readonly IIssueNotesManager _notesManager = new IssueNotesManager();

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
        private Issue GetIssue()
        {
            var allIssues = _issuesManager.GetIssues(GetToken(), GetProject().code, ClientUrl);
            var issue = allIssues.FirstOrDefault();
            return issue;
        }

        [TestMethod()]
        public void GetAllNotesByIssueTest()
        {
            var allNotes = _notesManager.GetAllNotesByIssue(GetToken(), GetProject().code, GetIssue().iid, ClientUrl);
            if (allNotes != null)
            {
                foreach (var note in allNotes)
                {
                    TestContext.WriteLine($"noteId: {note.id} noteText: {note.text}");
                }
            }
            else
            {
                Assert.Fail("note not found");
            }
        }

        [TestMethod()]
        public void CreateNoteTest()
        {
            var testNote = new Note() {text = $"new test note{DateTime.Now}"};
            var result = _notesManager.CreateNote(GetToken(), GetProject().code, GetIssue().iid, testNote, ClientUrl);
            if (result != null && result.text == testNote.text)
            {
                TestContext.WriteLine($"noteid: {result.id} text: {result.text}");
            }
            else
            {
                Assert.Fail("new test note not created");
            }
        }

        [TestMethod()]
        public void GetNoteByIdTest()
        {
            var testNote = new Note() { text = $"new test note{DateTime.Now}" };
            var createdNote = _notesManager.CreateNote(GetToken(), GetProject().code, GetIssue().iid, testNote, ClientUrl);
            if (createdNote != null && createdNote.text == testNote.text)
            {
                var result = _notesManager.GetNoteById(GetToken(), GetProject().code, GetIssue().iid, createdNote.id, ClientUrl);
                if (result != null && result.id == createdNote.id)
                {
                    TestContext.WriteLine("Succsessful!");
                    _notesManager.DeleteNote(GetToken(), GetProject().code, GetIssue().iid, result.id, ClientUrl);
                }
                else
                {
                    Assert.Fail("");
                }

            }
            else
            {
                Assert.Fail("new testNote not created in method GetNoteById");
            }
        }

        [TestMethod()]
        public void DeleteNoteTest()
        {
            var testNote = new Note() { text = $"new test note{DateTime.Now} created for delete testing" };
            var createdNote = _notesManager.CreateNote(GetToken(), GetProject().code, GetIssue().iid, testNote, ClientUrl);
            if (createdNote != null && createdNote.text == testNote.text)
            {
                _notesManager.DeleteNote(GetToken(), GetProject().code, GetIssue().iid, createdNote.id, ClientUrl);// deleting a createdNote
                var afterDeletetingNote = _notesManager.GetNoteById(GetToken(), GetProject().code, GetIssue().iid, createdNote.id, ClientUrl);
                if (afterDeletetingNote.id == null && afterDeletetingNote.text == null)
                {
                    TestContext.WriteLine($"deleted succsessful!");
                }
                else
                {
                    Assert.Fail($"note id: {afterDeletetingNote.id} & text: {afterDeletetingNote.text} NOT DELETED!");
                }
            }
            else
            {
                Assert.Fail("new test note not created for delete testing in method DeleteNoteTest");
            }
        }
    }
}