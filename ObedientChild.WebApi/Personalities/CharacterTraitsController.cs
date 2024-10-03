using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObedientChild.App.Personalities;
using ObedientChild.Domain.Personalities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.WebApi.Personalities
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CharacterTraitsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICharacterTraitsService _service;

        public CharacterTraitsController(IMapper mapper, ICharacterTraitsService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<CharacterTrait>>> GetList()
        {
            var list = await _service.GetListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<CharacterTrait>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<CharacterTrait>> Add([FromBody] CharacterTraitInputModel model)
        {
            if (ModelState.IsValid)
            {
                return await _service.AddAsync(model.PersonalityIds, model.characterTrait);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CharacterTrait>> Update(int id, [FromBody] CharacterTrait characterTrait)
        {
            if (ModelState.IsValid && id == characterTrait.Id)
            {
                return await _service.UpdateAsync(characterTrait);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("levels")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<CharacterTraitLevel>>> GetLevelList()
        {
            var list = await _service.GeLeveltListAsync();
            return Ok(list);
        }

        [HttpGet("levels/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<CharacterTraitLevel>> GetLevelById(int id)
        {
            var item = await _service.GetLevelByIdAsync(id);
            return Ok(item);
        }

        [HttpPost("levels")]
        public async Task<ActionResult<CharacterTraitLevel>> AddLevel([FromBody] CharacterTraitLevel characterTraitLevel)
        {
            if (ModelState.IsValid)
            {
                return await _service.AddLevelAsync(characterTraitLevel);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("levels/{id}")]
        public async Task<ActionResult<CharacterTraitLevel>> UpdateLevel(int id, [FromBody] CharacterTraitLevel characterTraitLevel)
        {
            if (ModelState.IsValid && id == characterTraitLevel.Id)
            {
                return await _service.UpdateLevelAsync(characterTraitLevel);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("levels/{id}")]
        public async Task<ActionResult> DeleteLevel(int id)
        {
            await _service.DeleteLevelAsync(id);
            return NoContent();
        }

        [HttpGet("child/{childId}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ChildCharacterTrait>>> GetListByChildId(int childId)
        {
            var list = await _service.GetListByChildIdAsync(childId);
            return Ok(list);
        }

        [HttpGet("childtraits/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ChildCharacterTrait>> GetChildCharacterTraitById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return Ok(item);
        }
    }
}
