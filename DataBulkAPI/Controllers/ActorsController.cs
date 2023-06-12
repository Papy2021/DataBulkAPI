using DataBulkAPI.DataRepository;
using DataBulkAPI.Models;
using DataBulkAPI.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace DataBulkAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    //[Authorize]
    public class ActorsController : ControllerBase
    {
        private readonly DataBulkDbContext _dbContext;

        public ActorsController(DataBulkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

      
        [HttpGet]
        public async Task<IActionResult> ListActors()
        {
            var listMembers = _dbContext.Actors;
            if (listMembers == null)
            {
                return NotFound();
            }
            return Ok(await _dbContext.Actors.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> Actor([FromRoute]Guid id)
        {
            var actor = await _dbContext.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound($"There is no actor with ID:{id}");
            }
            return Ok(actor);
        }


        [HttpPost]
        public async Task<IActionResult> AddActor(AddActorRequestModel actorToAdd)
        {
            var checkEmail = _dbContext.Actors.FirstOrDefault(e => e.Email == actorToAdd.Email);
            var checkPhone= _dbContext.Actors.FirstOrDefault(e => e.Phone == actorToAdd.Phone);
            if (checkPhone != null)
            {
                ModelState.AddModelError("", $"The number {checkPhone.Phone} is in used");
                return BadRequest(ModelState);
                
            }

            if (checkEmail != null)
            {
                ModelState.AddModelError("", $"The email {checkEmail.Email} is in used");
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                ActorModel actor = new()
                {
                    Id = Guid.NewGuid(),
                    FullName = actorToAdd.FullName,
                    Gender = actorToAdd.Gender,
                    Position = actorToAdd.Position,
                    Email = actorToAdd.Email,
                    Phone = actorToAdd.Phone,
                };

                await _dbContext.Actors.AddAsync(actor);
               await _dbContext.SaveChangesAsync();
                return Ok(actor);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateActor([FromRoute] Guid id, UpdateActorRequestModel actorToUpdate)
        {
            if (ModelState.IsValid)
            {
                var actor = await _dbContext.Actors.FindAsync(id);
                if (actor == null)
                {
                   
                    return NotFound($"There is no actor with ID:{id}");
                }

                var checkEmail=_dbContext.Actors.FirstOrDefault(e=>e.Email == actorToUpdate.Email);
                var checkPhone=_dbContext.Actors.FirstOrDefault(e=>e.Phone == actorToUpdate.Phone);

                if(checkEmail != null)
                {
                    if (checkEmail.Id != actor.Id)
                    {
                        ModelState.AddModelError("", "Email Can't be Updated, is used by an other user");
                        return BadRequest(ModelState);
                    }
                }

                if (checkPhone != null)
                {
                    if (checkPhone.Id != actor.Id)
                    {
                        ModelState.AddModelError("", "Phone Can't be Updated, is used by an other user");
                        return BadRequest(ModelState);
                    }
                }

                actor.FullName = actorToUpdate.FullName;
                actor.Gender = actorToUpdate.Gender;
                actor.Position = actorToUpdate.Position;
                actor.Email = actorToUpdate.Email;
                actor.Phone = actorToUpdate.Phone;

                await _dbContext.SaveChangesAsync();
                return Ok(actor);
            }
            return BadRequest(ModelState);
  
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteActor([FromRoute] Guid id)
        {
            try
            {
            var actor = await _dbContext.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound($"There is no actor with ID:{id}");
            }
           _dbContext.Actors.Remove(actor);
            await _dbContext.SaveChangesAsync();
            return Ok(actor); 
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
 
        }

    }
}
