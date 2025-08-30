using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly DataContext _context;

        public TestController(DataContext context)
        {
            _context = context;
        }

        // GET: /Test/{grade}
        [HttpGet("{grade}")]
        public async Task<ActionResult> getRandomTestByGrade(int grade)
        {
            // Lấy danh sách mã đề trong lớp
            var codes = await _context.Tests
                                       .Where(t => t.grade == grade)
                                       .Select(t => t.code)
                                       .Distinct()
                                       .ToListAsync();

            if (!codes.Any())
                return NotFound(new { message = "No test codes found for the given grade!" });

            // Random một mã đề
            var randomCode = codes[new Random().Next(codes.Count)];

            // Lấy toàn bộ câu hỏi thuộc mã đề đã chọn
            var questions = await _context.Tests
                                           .Where(t => t.grade == grade && t.code == randomCode)
                                           .ToListAsync();

            if (!questions.Any())
                return NotFound(new { message = "No questions found for the selected test code!" });

            return Ok(new
            {
                message = "Random test generated successfully!",
                testCode = randomCode,
                questions
            });
        }

        // GET: /Test/{grade}/{code}
        [HttpGet("{grade}/{code}")]
        public async Task<ActionResult> getTestByGradeAndCode(int grade, int code)
        {
            // Lấy câu hỏi theo mã đề
            var questions = await _context.Tests
                                           .Where(t => t.grade == grade && t.code == code)
                                           .ToListAsync();

            if (!questions.Any())
                return NotFound(new { message = "No questions found for the given grade and code!" });

            return Ok(new
            {
                message = "Test retrieved successfully!",
                testCode = code,
                questions
            });
        }

        // POST: /Test/Submit
        [HttpPost("Submit")]
        public async Task<ActionResult> submitAnswers([FromBody] SubmitTestRequest request)
        {
            try
            {
                // Lấy danh sách câu hỏi từ cơ sở dữ liệu
                var testQuestions = await _context.Tests
                                                  .Where(t => request.Answers.Select(a => a.QuestionId).Contains(t.id))
                                                  .ToListAsync();

                if (!testQuestions.Any())
                    return NotFound(new { message = "No questions found for the given IDs!" });

                // Tính điểm
                int score = 0;
                foreach (var answer in request.Answers)
                {
                    var question = testQuestions.FirstOrDefault(q => q.id == answer.QuestionId);
                    if (question != null && question.answer == answer.Answer)
                    {
                        score++;
                    }
                }

                return Ok(new
                {
                    message = "Test submitted successfully!",
                    score,
                    totalQuestions = testQuestions.Count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred: " + ex.Message });
            }
        }
    }

    // Model cho yêu cầu nộp bài
    public class SubmitTestRequest
    {
        public List<UserAnswer> Answers { get; set; } = new List<UserAnswer>();
    }

    public class UserAnswer
    {
        public int QuestionId { get; set; }  // ID của câu hỏi
        public string Answer { get; set; } = string.Empty; // Đáp án của học sinh
    }
}
