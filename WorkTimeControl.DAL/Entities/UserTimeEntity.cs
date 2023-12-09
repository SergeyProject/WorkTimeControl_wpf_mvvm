using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkTimeControl.DAL.Entities
{

    [Table("UserTimes")]
    public class UserTimeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateTimes { get; set; }
        public string? Descript { get; set; }
        public bool IsOnWork { get; set; }
        public byte[]? Photo { get; set; }
    }
}
