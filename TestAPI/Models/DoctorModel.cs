using TestAPI.Data.Models;

namespace TestAPI.Models
{
    public class DoctorModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Specialty { get; set; }
        public string Cabinet { get; set; }
        public string Region { get; set; }

        public static explicit operator DoctorModel (DoctorViewModel doctor)
        {
            if (doctor == null)
            {
                return null;
            }
            else return new DoctorModel
            {
                Id = doctor.Id,
                Surname = doctor.Surname,
                Name = doctor.Name,
                Patronymic = doctor.Patronymic,
                Specialty = doctor.Specialty,
                Cabinet = doctor.Cabinet,
                Region = doctor.Region,
            };
        }
    }
}
