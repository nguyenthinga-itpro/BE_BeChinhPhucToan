using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly DataContext _context;

        public ExerciseController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Exercise>>> getAllExercises()
        {
            var exercises = await _context.Exercises.ToListAsync();
            return Ok(exercises);
        }

        [HttpGet("GetByLessonType")]
        public async Task<ActionResult<List<Exercise>>> getByLessonType(int lessonTypeID)
        {
            var exercises = await _context.Exercises
                                .Where(e => e.lessonTypeID == lessonTypeID)
                                .ToListAsync();
            return Ok(exercises);
        }

        [HttpGet("GetExerciseCount")]
        public async Task<ActionResult<int>> GetLessonCountByType(int lessonTypeID)
        {
            // Đếm số lượng bài học dựa trên lessonTypeID
            var exerciseCount = await _context.Exercises
                                    .Where(e => e.lessonTypeID == lessonTypeID)
                                    .CountAsync();

            // Trả về số lượng
            return Ok(exerciseCount);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exercise>> getExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise is null)
                return NotFound(new { message = "Exercise is not found!" });
            return Ok(exercise);
        }

        [HttpPost]
        public async Task<ActionResult<Exercise>> addExercise([FromBody] Exercise exercise)
        {
            try
            {
                _context.Exercises.Add(exercise);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Created successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
            }
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Exercise>> updateExercise([FromBody] Exercise newInfo)
        //{
        //    try
        //    {
        //        var exercise = await _context.Exercises.FindAsync(newInfo.id);
        //        if (exercise is null)
        //            return NotFound(new { message = "Exercise is not found!" });

        //        exercise.question = newInfo.question;
        //        exercise.answer = newInfo.answer;
        //        exercise.lessonID = newInfo.lessonID;

        //        await _context.SaveChangesAsync();

        //        return Ok(new { message = "Updated successfully!" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
        //    }
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);

            if (exercise is null)
                return NotFound(new { message = "Exercise is not found!" });

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted successfully!" });
        }
    }
}
