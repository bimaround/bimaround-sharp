using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using BIMAroundClient.Interfaces;
using BIMAroundClient.ObjectModel.Attachments;
using RestSharp;

namespace BIMAroundClient
{
    public class AttacmentsManager : IAttachmentManager
    {
        public List<Attachment> GetAttachments(string token, string projectCode, string issueIid, string clientUrl)
        {
            try
            {
                var client = new RestClient(clientUrl);
                var request = new RestRequest("/projects/{projectCode}/issues/{iid}/attachments");
                request.AddUrlSegment("projectCode", projectCode);
                request.AddUrlSegment("iid", issueIid);
                request.AddHeader("Authorization", "Bearer " + token);
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 |
                                                       (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                var response = client.Execute<List<Attachment>>(request);
                if (response.Data.Count >= 1)
                {
                    return response.Data;
                }
                else
                {
                    //TODO Log
                }
            }
            catch (Exception e)
            {
                //TODO Log
            }
            return new List<Attachment>();
        }

        public void DownloadAttcahment(string token, string projectCode, string issueIid, Attachment attachment, string clientUrl)
        {
            try
            {
                var client = new RestClient(clientUrl);
                var request = new RestRequest("/projects/{projectCode}/issues/{iid}/attachments/{aid}");
                request.AddUrlSegment("projectCode", projectCode);
                request.AddUrlSegment("iid", issueIid);
                request.AddUrlSegment("aid", attachment.id);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Accept", "*/*");

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 |
                                                       (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                var file = client.DownloadData(request);
                if (file != null)
                {
                    var saveFileDialog = new SaveFileDialog { FileName = attachment.fileName };
                    saveFileDialog.ShowDialog();
                    File.WriteAllBytes(saveFileDialog.FileName, file);
                }
                else
                {
                    //TODO Log
                }
            }
            catch (Exception e)
            {
                //TODO Log
            }
        }

        public Attachment UploadAttachments(string token, string projectCode, string issueIid, string filePath, string clientUrl)
        {
            try
            {
                var client = new RestClient(clientUrl);
                var request = new RestRequest("/projects/{projectCode}/issues/{iid}/attachments") { Method = Method.POST };
                request.AddUrlSegment("projectCode", projectCode);
                request.AddUrlSegment("iid", issueIid);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddFile("file", filePath);
                request.AlwaysMultipartFormData = true;

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 |
                                                       (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                var response = client.Execute<Attachment>(request);
                
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //TODO Log
                }
                else
                {
                    return response.Data;
                }
            }
            catch (Exception e)
            {
                //TODO log
            }   
            return new Attachment();
        }

        public void DeleteAttachment(string token, string projectCode, string issueIid, Attachment attachment, string clientUrl)
        {
            try
            {
                var client = new RestClient(clientUrl);
                var request = new RestRequest("/projects/{projectCode}/issues/{iid}/attachments/{aid}") { Method = Method.DELETE };
                request.AddUrlSegment("projectCode", projectCode);
                request.AddUrlSegment("iid", issueIid);
                request.AddUrlSegment("aid", attachment.id);
                request.AddHeader("Authorization", "Bearer " + token);

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 |
                                                       (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                var response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //TODO Log
                }
            }
            catch (Exception e)
            {
                //TODO Log
            }
        }
    }
}
