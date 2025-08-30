using System.ComponentModel.DataAnnotations.Schema;

namespace BeChinhPhucToan_BE.Models
{
    public class Setting : BaseEntity
    {
        public int id { get; set; }
        public int volume { get; set; }

        public int studentID { get; set; }

    }
}
