using TestAPI.Data.Entities;

namespace TestAPI.Models
{
    public class PatientUpdateModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int RegionId { get; set; }

        public static explicit operator PatientUpdateModel (Patient patient)
        {
            if (patient == null)
                return null;
            else return new PatientUpdateModel
            {
                Id = patient.Id,
                Surname = patient.Surname,
                Name = patient.Name,
                Patronymic = patient.Patronymic,
                Birthday = patient.Birthday,
                Address = patient.Address,
                Gender = patient.Gender,
                RegionId = patient.RegionId,
            };
        }

        public static explicit operator Patient (PatientUpdateModel patient)
        {
            if (patient == null)
                return null;
            else return new Patient
            {
                Id = patient.Id,
                Surname = patient.Surname,
                Name = patient.Name,
                Patronymic = patient.Patronymic,
                Gender = patient.Gender,
                Address = patient.Address,
                Birthday = patient.Birthday,
                RegionId = patient.RegionId,
            };
        }
    }
}
