using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAPI.Data.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public int SpecialityId { get; set; }
        public virtual Specialty Specialty { get; set; }
        public int CabinetId { get; set; }
        public virtual Cabinet Cabinet { get; set; }
        public int? RegionId { get; set; }
        public virtual Region? Region { get; set; }
    }
}
