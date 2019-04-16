using System.Collections.Generic;
using System.Net;
using BIMAroundClient.Interfaces;
using BIMAroundClient.ObjectModel.Notes;
using RestSharp;
using RestSharp.Serialization.Json;

namespace BIMAroundClient
{
    public class IssueNotesManager : IIssueNotesManager
    {
        public List<Note> GetAllNotesByIssue(string token, string projectCode, string issueIid, string clientUrl)
        {
            var client = new RestClient(clientUrl);
            var request = new RestRequest("/projects/{projectCode}/issues/{iid}/notes");
            request.AddUrlSegment("projectCode", projectCode);
            request.AddUrlSegment("iid", issueIid);
            request.AddHeader("Authorization", "Bearer " + token);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 |
                                                   (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            var response = client.Execute(request);
            
            var notes = new JsonDeserializer().Deserialize<List<Note>>(response);
            return notes;
        }

        public Note CreateNote(string token, string projectCode, string issueIid, Note note, string clientUrl)
        {
            var client = new RestClient(clientUrl);
            var createNoteRequest = new CreateNoteRequest(){text = note.text};

            var request = new RestRequest("/projects/{projectCode}/issues/{iid}/notes"){ Method = Method.POST };
            request.AddUrlSegment("projectCode", projectCode);
            request.AddUrlSegment("iid", issueIid);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddJsonBody(createNoteRequest);

            var response = client.Execute<Note>(request);

            return response.Data;
        }

        public Note GetNoteById(string token, string projectCode, string issueIid, string noteId, string clientUrl)
        {
            var client = new RestClient(clientUrl);
            var request = new RestRequest("/projects/{projectCode}/issues/{iid}/notes/{id}");
            request.AddUrlSegment("projectCode", projectCode);
            request.AddUrlSegment("iid", issueIid);
            request.AddUrlSegment("id", noteId);
            request.AddHeader("Authorization", "Bearer " + token);

            var response = client.Execute<Note>(request);

            return response.Data;
        }

        public void DeleteNote(string token, string projectCode, string issueIid, string noteId, string clientUrl)
        {
            var client = new RestClient(clientUrl);
            var request = new RestRequest("/projects/{projectCode}/issues/{iid}/notes/{id}"){ Method = Method.DELETE };
            request.AddUrlSegment("projectCode", projectCode);
            request.AddUrlSegment("iid", issueIid);
            request.AddUrlSegment("id", noteId);
            request.AddHeader("Authorization", "Bearer " + token);

            var response = client.Execute(request);//used for debugging
        }
    }
}
