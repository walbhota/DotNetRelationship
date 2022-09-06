using Newtonsoft.Json;

namespace DotNetRelationship.Models
{
    public class User
    {
        public int  Id { get; set; }
        public string Username { get; set; } = string.Empty;

        //For 1-n( A user 1 to many relationship with character: 1 user can have many character)
        
        public List<Character> Characters { get; set; }
    }
}
