using System;
using BIMAroundClient.ObjectModel.Users;

namespace BIMAroundClient.ObjectModel.Notes
{
    public class Note
    {
        public string id { get; set; }
        public string text { get; set; }
        public User author { get; set; }
        public DateTime createdAt { get; set; }
    }
}
