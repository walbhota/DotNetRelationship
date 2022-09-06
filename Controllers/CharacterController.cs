using DotNetRelationship.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DotNetRelationship.Controllers
{
    [Route("relationship/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DataContext _context;

        public CharacterController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        //Getting Characters by the UserId **take note of the relationship**
        public async Task<ActionResult<List<Character>>> GetCharacters(int userId)
        {
            var characters = await _context.Characters
                .Where(x => x.UserId == userId)
                .Include(c => c.Weapon) //So the weapon will show when the character is called
                .Include(s => s.Skills)
                .ToListAsync();

            return characters;
        }

        [HttpPost]
        //Creating new Character using Dto
        public async Task<ActionResult<List<Character>>> Create(CreateCharacterDto request)
        {
                var user = await _context.Users.FindAsync(request.UserId);
                if (user == null)
                    return NotFound();

                var newCharacter = new Character
                {
                    Name = request.Name,
                    RpgClass = request.RpgClass,
                    User = user
                };

                _context.Characters.Add(newCharacter);
                await _context.SaveChangesAsync();

                return await GetCharacters(newCharacter.UserId);
        }


        [HttpPost("weapon")]
        //Creating new Weapon using Dto
        public async Task<ActionResult<Character>> AddWeapon(AddWeaponDto request)
        {
            var character = await _context.Characters.FindAsync(request.CharacterId);
            if (character == null)
                return NotFound();

            var newWeapon = new Weapon
            {
                Name = request.Name,
                Damage = request.Damage,
                Character = character
            };

            _context.Weapons.Add(newWeapon);
            await _context.SaveChangesAsync();

            return character;
        }

        [HttpPost("skill")]
        //Creating Add skill to characters and character to skills using Dto
        public async Task<ActionResult<Character>> AddSkillCharacter(AddSkillToCharacterDto request)
        {
            var character = await _context.Characters
                .Where(c => c.Id == request.CharacterId)
                .Include(t => t.Skills)
                .FirstOrDefaultAsync();
            if (character == null)
                return NotFound();

            var skill = await _context.Skills.FindAsync(request.SkillId);
            if (skill == null)
                return NotFound();


            character.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return character;
        }

    }
}
