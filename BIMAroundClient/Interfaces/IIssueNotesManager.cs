using System.Collections.Generic;
using BIMAroundClient.ObjectModel.Notes;

namespace BIMAroundClient.Interfaces
{
    public interface IIssueNotesManager
    {
        List<Note> GetAllNotesByIssue(string token, string projectCode, string issueIid, string clientUrl = "https://bimaround.com/api");
        Note CreateNote(string token, string projectCode, string issueIid, Note note, string clientUrl = "https://bimaround.com/api");
        Note GetNoteById(string token, string projectCode, string issueIid,string noteId, string clientUrl = "https://bimaround.com/api");
        void DeleteNote(string token, string projectCode, string issueIid, string noteId, string clientUrl = "https://bimaround.com/api");
    }
}
