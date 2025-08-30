using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly DataContext _context;

        public QuestionsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetByExerciseId")]
        public async Task<ActionResult<ExerciseStatus>> getByStudentAndExercise(int exerciseID)
        {
            var questions = await _context.Questions
                .Where(q => q.exerciseID == exerciseID)
                .ToListAsync();
            return Ok(questions);
        }
    }
}
