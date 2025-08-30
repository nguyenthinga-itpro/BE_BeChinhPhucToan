using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly DataContext _context;

        public LessonController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Lesson>>> getAllLessons()
        {
            var lessons = await _context.Lessons.ToListAsync();
            return Ok(lessons);
        }

        [HttpGet("GetByLessonType")]
        public async Task<ActionResult<List<Lesson>>> getByLessonType(int lessonTypeID)
        {
            var lessons = await _context.Lessons
                                .Where(l => l.lessonTypeID == lessonTypeID)
                                .ToListAsync();
            return Ok(lessons);
        }

        [HttpGet("GetLessonCount")]
        public async Task<ActionResult<int>> GetLessonCountByType(int lessonTypeID)
        {
            // Đếm số lượng bài học dựa trên lessonTypeID
            var lessonCount = await _context.Lessons
                                    .Where(l => l.lessonTypeID == lessonTypeID)
                                    .CountAsync();

            // Trả về số lượng
            return Ok(lessonCount);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> getLesson(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson is null)
                return NotFound(new { message = "Lesson is not found!" });
            return Ok(lesson);
        }

        [HttpPost]
        public async Task<ActionResult<Lesson>> addLesson([FromBody] Lesson lesson)
        {
            try
            {
                _context.Lessons.Add(lesson);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Created successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
            }
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Lesson>> updateLesson([FromBody] Lesson newInfo)
        //{
        //    try
        //    {
        //        var lesson = await _context.Lessons.FindAsync(newInfo.id);
        //        if (lesson is null)
        //            return NotFound(new { message = "Lesson is not found!" });

        //        lesson.name = newInfo.name;
        //        lesson.chapterID = newInfo.chapterID;

        //        await _context.SaveChangesAsync();

        //        return Ok(new { message = "Updated successfully!" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
        //    }
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteLesson(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);

            if (lesson is null)
                return NotFound(new { message = "Lesson is not found!" });

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted successfully!" });
        }
    }
}
