using KlouldScriptCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlouldScriptService.Plan
{
    public interface IPlanService
    {
        Task<IEnumerable<InsurancePlan>> GetAllInsurancePlans();

        Task<IEnumerable<PatientOptimalInsurancePlan>> CalculatePatientOptimalInsurancePlan(List<PatientExpense> patientExpenses, IEnumerable<InsurancePlan> insurancePlans);
    }
}
