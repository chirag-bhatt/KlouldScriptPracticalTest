using KlouldScriptCore.Models;
using KlouldScriptRepository.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlouldScriptService.Plan
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;

        public PlanService(IPlanRepository planRepository)
        {
            this._planRepository = planRepository;
        }
        public async Task<IEnumerable<InsurancePlan>> GetAllInsurancePlans()
        {
            return await this._planRepository.GetAllInsurancePlans();
        }

        public async Task<IEnumerable<PatientOptimalInsurancePlan>> CalculatePatientOptimalInsurancePlan(List<PatientExpense> patientExpenses, IEnumerable<InsurancePlan> insurancePlans)
        {
            List<PatientOptimalInsurancePlan> patientOptimalInsurancePlans = new List<PatientOptimalInsurancePlan>();
            foreach (var patientExpense in patientExpenses)
            {
                List<PatientOptimalInsurancePlan> patientInsurancePlans = new List<PatientOptimalInsurancePlan>();
                foreach (InsurancePlan insurancePlan in insurancePlans)
                {
                    PatientOptimalInsurancePlan patientInsurancePlan = new PatientOptimalInsurancePlan();
                    patientInsurancePlan.PatientId = patientExpense.PatientId;
                    patientInsurancePlan.RecommendedInsurancePlan = insurancePlan.InsurancePlanName;
                    patientInsurancePlan.Cost = GetActualSpentByInsurancePlan(insurancePlan, patientExpense.Expense);

                    patientInsurancePlans.Add(patientInsurancePlan);
                }
                var minCostInsurancePlan = patientInsurancePlans.OrderBy(x => x.Cost).FirstOrDefault();
                patientOptimalInsurancePlans.Add(minCostInsurancePlan);
            }

            return patientOptimalInsurancePlans;
        }


        private decimal GetActualSpentByInsurancePlan(InsurancePlan insurancePlan, decimal expense)
        {
            decimal expensePaidByPatient = 0;

            if (expense > insurancePlan.MaximumOutOfPocketExpenses)
            {
                expensePaidByPatient = 0;
            }
            else if (expense < insurancePlan.Deductible)
            {
                expensePaidByPatient = expense;
            }
            else if (expense >= insurancePlan.Deductible && expense <= insurancePlan.MaximumOutOfPocketExpenses)
            {
                expensePaidByPatient = expense * insurancePlan.Coinsurance / 100;
            }

            return expensePaidByPatient + insurancePlan.Premium;
        }
    }
}
