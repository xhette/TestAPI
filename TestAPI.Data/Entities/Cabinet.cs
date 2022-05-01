﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAPI.Data.Entities
{
    public class Cabinet
    {
        public int Id { get; set; }
        public string CabinetNumber { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
