using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Models
{
    [PrimaryKey(nameof(studentID), nameof(lessonID))]
    public class LessonStatus : BaseEntity
    {
        public int studentID { get; set; }
        public Student? Student { get; set; }
        public int lessonID { get; set; }
        public Lesson? Lesson { get; set; }

        public bool isComplete { get; set; }
    }
}
