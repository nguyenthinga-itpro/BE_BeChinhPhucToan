using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotifyParentController : ControllerBase
    {
        //private readonly DataContext _context;

        //public NotifyParentController(DataContext context)
        //{
        //    _context = context;
        //}

        //// GET: /NotifyParent
        //[HttpGet]
        //public async Task<ActionResult<List<NotifyParent>>> GetAllNotifyParents()
        //{

        //    var notifyParents = await _context.NotifyParents

        //        .Include(np => np.ParentNotification) // Bao gồm thông tin ParentNotification
        //        .Include(np => np.Parent) // Bao gồm thông tin Parent
        //        .ToListAsync();

        //    return Ok(notifyParents);
        //}


        //// POST: /NotifyParent
        //[HttpPost]
        //public async Task<ActionResult> addNotifyParent([FromBody] NotifyParent notifyParent)
        //{
        //    var parent = await _context.Parents.FirstOrDefaultAsync(p => p.phoneNumber == notifyParent.parentPhone);
        //    var notification = await _context.ParentNotifications.FirstOrDefaultAsync(n => n.id == notifyParent.notificationID);

        //    if (parent == null || notification == null)
        //    {
        //        return BadRequest(new { message = "Parent or Notification does not exist!" });
        //    }

        //    var existingNotifyParent = await _context.NotifyParents
        //        .FirstOrDefaultAsync(np => np.parentPhone == notifyParent.parentPhone
        //                                && np.notificationID == notifyParent.notificationID);
        //    if (existingNotifyParent != null)
        //    {
        //        return Conflict(new { message = "NotifyParent already exists!" });
        //    }

        //    _context.NotifyParents.Add(notifyParent);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { message = "NotifyParent created successfully!" });
        //}


        //// DELETE: /NotifyParent/{parentPhone}/{notificationID}
        //[HttpDelete("{parentPhone}/{notificationID}")]
        //public async Task<IActionResult> deleteNotifyParent(string parentPhone, int notificationID)
        //{
        //    var notifyParent = await _context.NotifyParents
        //        .FirstOrDefaultAsync(np => np.parentPhone == parentPhone && np.notificationID == notificationID);

        //    if (notifyParent == null)
        //        return NotFound(new { message = "NotifyParent not found!" });

        //    _context.NotifyParents.Remove(notifyParent);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { message = "NotifyParent deleted successfully!" });
        //}
    }
}
