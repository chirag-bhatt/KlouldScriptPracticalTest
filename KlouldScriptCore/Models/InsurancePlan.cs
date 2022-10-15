using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlouldScriptCore.Models
{
    public class InsurancePlan
    {
        public string InsurancePlanName { get; set; }
        public decimal Premium { get; set; }
        public decimal MaximumOutOfPocketExpenses { get; set; }
        public decimal Coinsurance { get; set; }
        public decimal Deductible { get; set; }
    }
}
