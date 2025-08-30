using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BeChinhPhucToan_BE.Models
{
    [PrimaryKey(nameof(badgeID), nameof(studentID))]
    public class Purchase : BaseEntity
    {
        public int badgeID { get; set; }
        public Badge? Badge { get; set; }
        public int studentID { get; set; }
        public Student? Student { get; set; }
    }
}
