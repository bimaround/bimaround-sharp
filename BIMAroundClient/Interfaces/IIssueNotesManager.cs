using System.Collections.Generic;
using BIMAroundClient.ObjectModel;
using BIMAroundClient.ObjectModel.Notes;

namespace BIMAroundClient.Interfaces
{
    public interface IIssueNotesManager
    {
        List<Note> GetAllNotesByIssue(string token, string projectCode, string issueIid);
        Note CreateNote(string token, string projectCode, string issueIid, Note note);
        Note GetNoteById(string token, string projectCode, string issueIid,string noteId);
        void DeleteNote(string token, string projectCode, string issueIid, string noteId);
    }
}
