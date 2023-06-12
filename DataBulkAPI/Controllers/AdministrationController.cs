using DataBulkAPI.DataRepository;
using DataBulkAPI.Models;
using DataBulkAPI.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DataBulkAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin")]
    public class AdministrationController : ControllerBase
    {
        private readonly DataBulkDbContext _db;
        public AdministrationController(DataBulkDbContext db)
        {
            _db = db;
        }
        [HttpPost]
        [Route("SetUserDefautAccess")]
        public async Task<IActionResult> AddDefaultAccess(EditUserRoleRequestModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var userExist = _db.Users.FirstOrDefault(u => u.Username == user.userName);
                    if (userExist == null)
                    {
                        return BadRequest($"The user: {user.userName} do not exist");
                    }

                    var userToUpdate = await _db.Users.FindAsync(userExist.Id);
                    if (userToUpdate != null)
                    {
                        userToUpdate.Role = "Default";
                        await _db.SaveChangesAsync();
                        return Ok($"{userToUpdate.Username} is got default access level");
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("SetUserAdmin")]
        public async Task<IActionResult> AddUserRoles(EditUserRoleRequestModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
           
                    var userExist = _db.Users.FirstOrDefault(u => u.Username == user.userName);
                    if (userExist == null)
                    {
                        return BadRequest($"The user: {user.userName} do not exist");
                    }

                    var userToUpdate =await _db.Users.FindAsync(userExist.Id);
                    if (userToUpdate != null)
                    {
                        userToUpdate.Role = "Admin";
                        await _db.SaveChangesAsync();
                        return Ok($"{userToUpdate.Username} is now an Admin");
                    }
                   
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }



        [HttpPost]
        [Route("SetSuperAdmin")]
        public async Task<IActionResult> SetUserSuperAdmin(EditUserRoleRequestModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var userExist = _db.Users.FirstOrDefault(u => u.Username == user.userName);
                    if (userExist == null)
                    {
                        return BadRequest($"The user: {user.userName} do not exist");
                    }

                    var userToUpdate = await _db.Users.FindAsync(userExist.Id);
                    if (userToUpdate != null)
                    {
                        userToUpdate.Role = "SuperAdmin";
                        await _db.SaveChangesAsync();
                        return Ok($"{userToUpdate.Username} is now an super-admin");
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
