using Microsoft.AspNetCore.Mvc;
using KnightsChallenge.Models;
using KnightsChallenge.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace KnightsChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KnightsController : ControllerBase
    {
        private readonly IKnightService _knightService;

        public KnightsController(IKnightService knightService)
        {
            _knightService = knightService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Knight>>> Get() =>
            await _knightService.GetAsync();

        [HttpGet]
        [Route("filter")]
        public async Task<ActionResult<List<Knight>>> GetFiltered([FromQuery] string filter)
        {
            if (string.IsNullOrEmpty(filter) || filter.ToLower() != "heroes")
            {
                return BadRequest("Invalid filter parameter.");
            }
            else
            {
              
                var heroes = await _knightService.GetAsync();
                return heroes.Where(k => k.IsHero).ToList();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Knight>> Create(Knight knight)
        {
           
            knight.Attributes = new Attributes(); 
            knight.Weapons = new List<Weapon>(); 

        
            knight.Attributes.Strength = 10; 
            knight.KeyAttribute = "strength"; 

          
            knight.Id = ObjectId.GenerateNewId().ToString();

            await _knightService.CreateAsync(knight);

            return CreatedAtRoute("GetKnight", new { id = knight.Id.ToString() }, knight);
        }

        [HttpGet("{id:length(24)}", Name = "GetKnight")]
        public async Task<ActionResult<Knight>> Get(string id)
        {
            var knight = await _knightService.GetAsync(id);

            if (knight == null)
            {
                return NotFound();
            }

            return knight;
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var knight = await _knightService.GetAsync(id);

            if (knight == null)
            {
                return NotFound();
            }

            knight.IsHero = true; 
            await _knightService.RemoveAsync(id);

          
            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Knight updatedKnight)
        {
            var knight = await _knightService.GetAsync(id);

            if (knight == null)
            {
                return NotFound();
            }

        
            knight.Name = updatedKnight.Name;
            knight.Nickname = updatedKnight.Nickname;
         
            knight.Attributes.Strength = 15; 
            knight.KeyAttribute = "strength"; 

            await _knightService.UpdateAsync(id, knight);

            return NoContent();
        }
    }
}
