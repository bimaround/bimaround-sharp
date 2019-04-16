using BIMAroundClient.ObjectModel.Projects;

namespace BIMAroundClient.ObjectModel.Users
{
    public class ProjectUsers
    {
        public int id { get; set; }
        public Project project { get; set; }
        public User user { get; set; }
        public string role { get; set; }
        public bool activated { get; set; }
    }
}
