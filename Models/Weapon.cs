using System.Text.Json.Serialization;

namespace DotNetRelationship.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int Damage { get; set; } = 10;

        //1:1 One to One relationship between weapon and character(One character can have only one weapon)

        [JsonIgnore] //To prevent possible object cycle error.
        public Character Character { get; set; }
        public int CharacterId { get; set; }
    }
}
