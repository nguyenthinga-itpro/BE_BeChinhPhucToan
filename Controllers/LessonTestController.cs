using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LessonTestController : ControllerBase
    {
        private readonly DataContext _context;

        public LessonTestController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetByLessonType")]
        public async Task<ActionResult<List<LessonTest>>> getByLessonType(int lessonTypeID)
        {
            var lessonTests = await _context.LessonTests
                                .Where(e => e.lessonTypeID == lessonTypeID)
                                .ToListAsync();
            return Ok(lessonTests);
        }
    }
}
