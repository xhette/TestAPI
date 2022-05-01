using TestAPI.Data.Models;

namespace TestAPI.Models
{
    public class PatientModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Region { get; set; }

        public static explicit operator PatientModel(PatientViewModel patient)
        {
            if (patient == null)
                return null;
            else return new PatientModel
            {
                Id = patient.Id,
                Surname = patient.Surname,
                Name = patient.Name,
                Patronymic = patient.Patronymic,
                Birthday = patient.Birthday,
                Address = patient.Address,
                Gender = patient.Gender,
                Region = patient.Region,
            };
        }
    }
}
