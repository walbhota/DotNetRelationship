using System.Text.Json.Serialization;

namespace DotNetRelationship.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RpgClass { get; set; } = "Knight";
     
        //For 1:n One to Many Relationship( A user 1 to many relationship with character: 1 user can have many character)
        [JsonIgnore]
        public User User { get; set; }
        public int UserId { get; set; }

        //1:1 One to One relationship between weapon and character(One character can have only one weapon)
        //There is no weaponId here because the weapon is dependent on the character
        //The weapon can not exist without the character but the character can exist without the weapon
        public Weapon Weapon { get; set; }

        //n:n Many to Many relationship between Character and Skill(One character can have many skill and One skill can be owned by many character)
        public List<Skill> Skills { get; set; }

    }
}
