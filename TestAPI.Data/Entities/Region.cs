using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAPI.Data.Entities
{
    public class Region
    {
        public int Id { get; set; }
        public string RegionNumber { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
