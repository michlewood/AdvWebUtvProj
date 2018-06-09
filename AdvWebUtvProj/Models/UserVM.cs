using System.Collections.Generic;

namespace AdvWebUtvProj.Models
{
    public class UserVM
    {
        public string Name { get; }
        public string Email { get; }
        public IList<string> Role { get; set; } = new List<string>();

        public UserVM(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}