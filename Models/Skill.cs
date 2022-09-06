using System.Text.Json.Serialization;

namespace DotNetRelationship.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }

        //n:n Many to Many relationship between Character and Skill(One character can have many skill and One skill can be owned by many character)
        [JsonIgnore]
        public List<Character> Characters { get; set; }
    }
}
