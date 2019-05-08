using System;
using BIMAroundClient.ObjectModel.Users;

namespace BIMAroundClient.ObjectModel.Issues
{
    public class CreateIssueRequest
    {
        public string title { get; set; }
        public User assignee { get; set; }
        public DateTime dueDate { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }
}
