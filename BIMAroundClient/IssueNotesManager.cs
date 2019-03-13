using System.Collections.Generic;
using BIMAroundClient.Interfaces;
using BIMAroundClient.ObjectModel.Notes;
using RestSharp;

namespace BIMAroundClient
{
    public class IssueNotesManager : IIssueNotesManager
    {
        public List<Note> GetAllNotesByIssue(string token, string projectCode, string issueIid)
        {
            var client = Client;
            var request = new RestRequest("/projects/{projectCode}/issues/{iid}/notes");
            request.AddUrlSegment("projectCode", projectCode);
            request.AddUrlSegment("iid", issueIid);
            request.AddHeader("Authorization", "Bearer " + token);

            var response = client.Execute<GetNotesResponse>(request);
            var notes = response.Data;

            return notes.Notes;
        }

        public Note CreateNote(string token, string projectCode, string issueIid, Note note)
        {
            var client = Client;
            var createNoteRequest = new CreateNoteRequest(){text = note.text};

            var request = new RestRequest("/projects/{projectCode}/issues/{iid}/notes"){ Method = Method.POST };
            request.AddUrlSegment("projectCode", projectCode);
            request.AddUrlSegment("iid", issueIid);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddJsonBody(createNoteRequest);

            var response = client.Execute<Note>(request);

            return response.Data;
        }

        public Note GetNoteById(string token, string projectCode, string issueIid, string noteId)
        {
            var client = Client;
            var request = new RestRequest("/projects/{projectCode}/issues/{iid}/notes/{id}");
            request.AddUrlSegment("projectCode", projectCode);
            request.AddUrlSegment("iid", issueIid);
            request.AddUrlSegment("id", noteId);
            request.AddHeader("Authorization", "Bearer " + token);

            var response = client.Execute<Note>(request);

            return response.Data;
        }

        public void DeleteNote(string token, string projectCode, string issueIid, string noteId)
        {
            var client = Client;
            var request = new RestRequest("/projects/{projectCode}/issues/{iid}/notes/{id}");
            request.AddUrlSegment("projectCode", projectCode);
            request.AddUrlSegment("iid", issueIid);
            request.AddUrlSegment("id", noteId);
            request.AddHeader("Authorization", "Bearer " + token);

            client.Execute(request);
        }
        private RestClient Client => new RestClient("https://bimaround.com/api");
    }
}
