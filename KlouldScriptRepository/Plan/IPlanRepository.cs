using KlouldScriptCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlouldScriptRepository.Plan
{
    public interface IPlanRepository
    {
        Task<IEnumerable<InsurancePlan>> GetAllInsurancePlans();
        Task InsertAllInsurancePlans(IEnumerable<PatientOptimalInsurancePlan> patientOptimalInsurancePlans);
    }
}
