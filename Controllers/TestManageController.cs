using BeChinhPhucToan_BE.Data;
using BeChinhPhucToan_BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestManageController : ControllerBase
    {
        private readonly DataContext _context;

        public TestManageController(DataContext context)
        {
            _context = context;
        }

        // GET: /Test
        // Lấy danh sách tất cả bài test
        [HttpGet]
        public async Task<ActionResult<List<Test>>> getAllTests()
        {
            var tests = await _context.Tests.AsNoTracking().ToListAsync();
            return Ok(tests);
        }

        // GET: /Test/{id}
        // Lấy thông tin một bài test theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> getTestById(int id)
        {
            var test = await _context.Tests.AsNoTracking().FirstOrDefaultAsync(t => t.id == id);
            if (test == null)
            {
                return NotFound(new { message = "Test not found!" });
            }
            return Ok(test);
        }

        // GET: /Test/ByCode/{grade}/{code}
        // Lấy danh sách câu hỏi theo mã đề trong một lớp
        [HttpGet("ByCode/{grade}/{code}")]
        public async Task<ActionResult<List<Test>>> getTestsByCode(int grade, int code)
        {
            var tests = await _context.Tests
                                       .AsNoTracking()
                                       .Where(t => t.grade == grade && t.code == code)
                                       .ToListAsync();

            if (!tests.Any())
            {
                return NotFound(new { message = $"No tests found for grade {grade} and code {code}!" });
            }

            return Ok(tests);
        }

        // POST: /Test
        // Thêm mới nhiều bài test
        [HttpPost]
        public async Task<ActionResult> addMultipleTests([FromBody] List<Test> tests)
        {
            try
            {
                if (tests == null || !tests.Any())
                {
                    return BadRequest(new { message = "No tests provided!" });
                }

                foreach (var test in tests)
                {
                    if (string.IsNullOrEmpty(test.question) || string.IsNullOrEmpty(test.answer))
                    {
                        return BadRequest(new { message = "Each test must have a question and an answer!" });
                    }
                }

                // Thêm tất cả bài kiểm tra vào cơ sở dữ liệu
                _context.Tests.AddRange(tests);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Tests added successfully!", tests });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        // PUT: /Test/{id}
        // Cập nhật thông tin một bài test
        [HttpPut("{id}")]
        public async Task<ActionResult> updateTest(int id, [FromBody] Test updatedTest)
        {
            try
            {
                var existingTest = await _context.Tests.FindAsync(id);
                if (existingTest == null)
                {
                    return NotFound(new { message = "Test not found!" });
                }

                // Cập nhật thông tin
                existingTest.grade = updatedTest.grade;
                existingTest.code = updatedTest.code; // Cập nhật mã đề
                existingTest.question = !string.IsNullOrEmpty(updatedTest.question) ? updatedTest.question : existingTest.question;
                existingTest.answer = !string.IsNullOrEmpty(updatedTest.answer) ? updatedTest.answer : existingTest.answer;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Test updated successfully!", updatedTest });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        // DELETE: /Test/{id}
        // Xóa một bài test
        [HttpDelete("{id}")]
        public async Task<ActionResult> deleteTest(int id)
        {
            try
            {
                var test = await _context.Tests.FindAsync(id);
                if (test == null)
                {
                    return NotFound(new { message = "Test not found!" });
                }

                _context.Tests.Remove(test);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Test deleted successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }
        // DELETE: /Test/DeleteAll
        // Xóa tất cả bài kiểm tra
        [HttpDelete("DeleteAll")]
        public async Task<ActionResult> deleteAllTests()
        {
            try
            {
                var allTests = await _context.Tests.ToListAsync();

                if (!allTests.Any())
                {
                    return Ok(new { message = "No tests available to delete. The table is already empty!" });
                }

                _context.Tests.RemoveRange(allTests);
                await _context.SaveChangesAsync();

                return Ok(new { message = "All tests deleted successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

    }
}
