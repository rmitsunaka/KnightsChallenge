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
                // Retorna apenas os knights que são heróis
                var heroes = await _knightService.GetAsync();
                return heroes.Where(k => k.IsHero).ToList();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Knight>> Create(Knight knight)
        {
            // Antes de persistir o Knight, defina seus atributos, armas, etc.
            knight.Attributes = new Attributes(); // Certifique-se de inicializar os atributos adequadamente
            knight.Weapons = new List<Weapon>(); // Certifique-se de inicializar a lista de armas

            // Agora, calcule o ataque e a experiência
            knight.Attributes.Strength = 10; // Defina os atributos necessários
            knight.KeyAttribute = "strength"; // Defina o atributo chave

            // Gerar um novo ObjectId manualmente
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

            knight.IsHero = true; // Define o Knight como um herói antes de removê-lo
            await _knightService.RemoveAsync(id);

            // Implemente a lógica para mover o Knight para o Hall of Heroes

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

            // Antes de atualizar o Knight, defina seus atributos, armas, etc.
            knight.Name = updatedKnight.Name;
            knight.Nickname = updatedKnight.Nickname;
            // Faça o mesmo para outros atributos, armas, etc.

            // Agora, calcule o ataque e a experiência
            knight.Attributes.Strength = 15; // Defina os atributos necessários
            knight.KeyAttribute = "strength"; // Defina o atributo chave

            await _knightService.UpdateAsync(id, knight);

            return NoContent();
        }
    }
}
