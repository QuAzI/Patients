using Patients.Domain.Enums;

namespace Patients.Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; init; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public Active Active { get; set; }
        public string Use { get; set; } = null!;
        public string Family { get; set; } = null!;
        public string[] Given { get; set; }
    }
}