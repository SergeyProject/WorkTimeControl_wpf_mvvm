namespace WorkTimeControl.BLL.DTO
{
    public class UserTimeDTO
    {       
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateTimes { get; set; }
        public string? Descript { get; set; }
        public bool IsOnWork { get; set; }
        public byte[]? Photo { get; set; }
    }
}
