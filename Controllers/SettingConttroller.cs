using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeChinhPhucToan_BE.Models;
using BeChinhPhucToan_BE.Data;

namespace BeChinhPhucToan_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly DataContext _context;

        public SettingController(DataContext context)
        {
            _context = context;
        }

        // GET: /Setting
        [HttpGet]
        public async Task<ActionResult<List<Setting>>> GetAllSettings()
        {
            var settings = await _context.Settings.ToListAsync();
            return Ok(settings);
        }

        // GET: /Setting/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Setting>> GetSettingById(int id)
        {
            var setting = await _context.Settings.FindAsync(id);

            if (setting == null)
                return NotFound(new { message = "Setting not found!" });

            return Ok(setting);
        }

        // POST: /Setting
        [HttpPost]
        public async Task<ActionResult> CreateSetting([FromBody] Setting setting)
        {
            if (setting == null || setting.volume < 0 || setting.volume > 100)
                return BadRequest(new { message = "Invalid input data! Volume must be between 0 and 100." });

            // Kiểm tra nếu `studentID` đã tồn tại
            var existingSetting = await _context.Settings.FirstOrDefaultAsync(s => s.studentID == setting.studentID);
            if (existingSetting != null)
                return Conflict(new { message = "Setting for this student already exists!" });

            _context.Settings.Add(setting);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSettingById), new { id = setting.id }, setting);
        }

        // PUT: /Setting/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSetting(int id, [FromBody] Setting updatedSetting)
        {
            var setting = await _context.Settings.FindAsync(id);

            if (setting == null)
                return NotFound(new { message = "Setting not found!" });

            if (updatedSetting.volume < 0 || updatedSetting.volume > 100)
                return BadRequest(new { message = "Invalid volume! It must be between 0 and 100." });

            // Cập nhật các thuộc tính
            setting.volume = updatedSetting.volume;

            _context.Settings.Update(setting);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Setting updated successfully!" });
        }

        // DELETE: /Setting/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSetting(int id)
        {
            var setting = await _context.Settings.FindAsync(id);

            if (setting == null)
                return NotFound(new { message = "Setting not found!" });

            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Setting deleted successfully!" });
        }
    }
}
