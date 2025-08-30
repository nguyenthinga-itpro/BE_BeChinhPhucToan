using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParentNotificationController : ControllerBase
    {
        //private readonly DataContext _context;

        //public ParentNotificationController(DataContext context)
        //{
        //    _context = context;
        //}

        //// GET: /ParentNotification
        //[HttpGet]
        //public async Task<ActionResult<List<UserNotification>>> GetAllNotifications()
        //{
        //    var notifications = await _context.ParentNotifications
        //        .ToListAsync();

        //    return Ok(notifications);
        //}


        //// GET: /ParentNotification/{id}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<UserNotification>> getNotification(int id)
        //{
        //    var notification = await _context.ParentNotifications
        //        .FirstOrDefaultAsync(n => n.id == id);

        //    if (notification is null)
        //        return NotFound(new { message = "Notification not found!" });

        //    return Ok(notification);
        //}

        //// POST: /ParentNotification
        //[HttpPost]
        //public async Task<ActionResult> addNotification([FromBody] UserNotification notification)
        //{
        //    _context.ParentNotifications.Add(notification);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { message = "Notification created successfully!" });
        //}

        //// DELETE: /ParentNotification/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> deleteNotification(int id)
        //{
        //    var notification = await _context.ParentNotifications.FindAsync(id);
        //    if (notification == null)
        //        return NotFound(new { message = "Notification not found!" });

        //    _context.ParentNotifications.Remove(notification);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { message = "Notification deleted successfully!" });
        //}
    }
}
