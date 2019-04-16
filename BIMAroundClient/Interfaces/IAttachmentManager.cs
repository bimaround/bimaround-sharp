using System.Collections.Generic;
using BIMAroundClient.ObjectModel.Attachments;

namespace BIMAroundClient.Interfaces
{
    public interface IAttachmentManager
    {
        List<Attachment> GetAttachments(string token, string projectCode, string issueIid,  string clientUrl = "https://bimaround.com/api");
        void DownloadAttcahment(string token, string projectCode, string issueIid, Attachment attachment, string clientUrl = "https://bimaround.com/api");
        void UploadAttachments(string token, string projectCode, string issueIid, string filePath, string clientUrl = "https://bimaround.com/api");
    }
}
