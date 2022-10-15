using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlouldScriptCore.Models
{
    public class PatientOptimalInsurancePlan
    {
        public int PatientId { get; set; }
        public string RecommendedInsurancePlan { get; set; }
        public decimal Cost { get; set; }
    }
}
