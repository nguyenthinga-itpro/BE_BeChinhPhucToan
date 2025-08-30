using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LessonStatusController : ControllerBase
    {
        private readonly DataContext _context;

        public LessonStatusController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetByStudent&Lesson")]
        public async Task<ActionResult<LessonStatus>> getByStudentAndLesson(int studentID, int lessonID)
        {
            var status = await _context.LessonStatuses
                .Where(ls => ls.studentID == studentID && ls.lessonID == lessonID)
                .ToListAsync();
            return Ok(status);
        }

        [HttpPost]
        public async Task<ActionResult<LessonStatus>> addLessonStatus([FromBody] LessonStatus status)
        {
            try
            {
                _context.LessonStatuses.Add(status);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Tạo mới thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã có lỗi xảy ra!" });
            }
        }
    }
}
