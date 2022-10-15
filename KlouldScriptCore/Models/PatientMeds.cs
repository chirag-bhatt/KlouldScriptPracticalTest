using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlouldScriptCore.Models
{
    public class PatientMeds
    {
        public int PatientId { get; set; }
        public string MedicationName { get; set; }
        public short DaysSupply { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Cost { get; set; }
    }
}
