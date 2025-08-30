using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BeChinhPhucToan_BE.Models;
using BeChinhPhucToan_BE.Data;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        //private readonly DataContext _context;

        //public AdministratorController(DataContext context)
        //{
        //    _context = context;
        //}

        //[HttpGet]
        //public async Task<ActionResult<List<Administrator>>> getAllAdmins()
        //{
        //    var admins = await _context.Administrators.ToListAsync();
        //    return Ok(admins);
        //}

        //[HttpGet("{phoneNumber}")]
        //public async Task<ActionResult<Administrator>> getAdmin(string phoneNumber)
        //{
        //    var admin = await _context.Administrators.FindAsync(phoneNumber);
        //    if (admin is null)
        //        return NotFound(new { message = "Administrator is not found!" });
        //    return Ok(admin);
        //}

        //[HttpPost]
        //public async Task<ActionResult<Administrator>> addAdmin([FromBody] Administrator admin)
        //{
        //    try
        //    {
        //        _context.Administrators.Add(admin);
        //        await _context.SaveChangesAsync();

        //        return Ok(new { message = "Created successfully!" });
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        return ExceptionController.primaryKeyException(ex, "phone number");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
        //    }
        //}

        //[HttpPut("{phoneNumber}")]
        //public async Task<ActionResult<Administrator>> updateAdmin([FromBody]Administrator newInfo)
        //{
        //    try 
        //    {
        //        var admin = await _context.Administrators.FindAsync(newInfo.phoneNumber);
        //        if (admin is null)
        //            return NotFound(new { message = "Administrator is not found!" });

        //        admin.fullName = newInfo.fullName;

        //        await _context.SaveChangesAsync();

        //        return Ok(new { message = "Updated successfully!" });
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        return ExceptionController.primaryKeyException(ex, "phone number");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
        //    }
        //}

        //[HttpDelete("{phoneNumber}")]
        //public async Task<IActionResult> deleteAdmin(string phoneNumber)
        //{
        //    var admin = await _context.Administrators.FindAsync(phoneNumber);
            
        //    if (admin is null)
        //        return NotFound(new { message = "Administrator is not found!" });
            
        //    _context.Administrators.Remove(admin);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { message = "Deleted successfully!" });
        //}
    }
}