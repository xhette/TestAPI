using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAPI.Data.Models
{
    public class DoctorViewModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Specialty { get; set; }
        public string Cabinet { get; set; }
        public string Region { get; set; }
    }
}
