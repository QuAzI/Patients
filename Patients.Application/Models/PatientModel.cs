using System.Text.Json.Serialization;
using Patients.Domain.Enums;

namespace Patients.Application.Models
{
    public class PatientModel
    {
        public PatientNameModel Name { get; set; } = null!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender? Gender { get; set; }

        public DateTime BirthDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Active Active { get; set; }
    }
}
