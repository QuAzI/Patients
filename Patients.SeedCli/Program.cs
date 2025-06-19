using Patients.Clients;

class Program
{
    static void Main()
    {
        var apiInstance = new PatientsApiClient("http://localhost:8000", new HttpClient());
        for (var patientNumber = 0; patientNumber < 100; patientNumber++)
        {
            var entity = new CreatePatientCommand
            {
                Name = new PatientNameModel
                {
                    Family = RandomString(randomizer.Next(12)),
                    Given = new List<string>
                    {
                        RandomString(randomizer.Next(12)),
                        RandomString(randomizer.Next(12))
                    },
                    Use = "official"
                },
                Gender = RandomEnumValue<Gender>(),
                BirthDate = new DateTime(
                    1900 + randomizer.Next(125), 1 + randomizer.Next(11), 1+ randomizer.Next(27),
                    randomizer.Next(23), randomizer.Next(59), 00),
                Active = RandomEnumValue<Active>()
            };

            try
            {
                var result = apiInstance.CreatePatientAsync(entity).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception on creating item {patientNumber}: {ex.Message}");
            }
        }
    }

    private static readonly Random randomizer = new Random();
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[randomizer.Next(s.Length)]).ToArray());
    }

    static T RandomEnumValue<T> ()
    {
        var v = Enum.GetValues (typeof (T));
        return (T) v.GetValue (randomizer.Next(v.Length));
    }
}