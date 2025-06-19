namespace Patients.Application.Models
{
    public class PatientNameModel
    {
        public Guid? Id { get; set; }
        public string Use { get; set; } = null!;
        public string Family { get; set; } = null!;
        public string[] Given { get; set; } = Array.Empty<string>();
    }
}