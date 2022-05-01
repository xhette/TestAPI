using TestAPI.Data.Entities;

namespace TestAPI.Models
{
    public class DoctorUpdateModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public int SpecialityId { get; set; }
        public int CabinetId { get; set; }
        public int? RegionId { get; set; }

        public static explicit operator DoctorUpdateModel (Doctor doctor)
        {
            if (doctor == null)
                return null;
            else return new DoctorUpdateModel
            {
                Id = doctor.Id,
                Surname = doctor.Surname,
                Name = doctor.Name,
                Patronymic = doctor.Patronymic,
                SpecialityId = doctor.SpecialityId,
                CabinetId = doctor.CabinetId,
                RegionId = doctor.RegionId,
            };
        }

        public static explicit operator Doctor (DoctorUpdateModel model)
        {
            if (model == null)
                return null;
            else return new Doctor
            {
                Id = model.Id,
                Surname = model.Surname,
                Name = model.Name,
                Patronymic = model.Patronymic,
                SpecialityId = model.SpecialityId,
                CabinetId = model.CabinetId,
                RegionId = model.RegionId,
            };
        }
    }
}
