using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Models
{
    [PrimaryKey(nameof(studentID), nameof(exerciseID))]
    public class ExerciseStatus : BaseEntity
    {
        public int studentID { get; set; }
        public Student? Student { get; set; }
        public int exerciseID { get; set; }
        public Exercise? Exercise { get; set; }

        public int point { get; set; }
        public bool isComplete { get; set; }
    }
}
