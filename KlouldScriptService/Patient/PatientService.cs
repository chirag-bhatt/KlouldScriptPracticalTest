using KlouldScriptCore.Models;
using KlouldScriptRepository.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlouldScriptService.Patient
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            this._patientRepository = patientRepository;
        }
        public async Task<IEnumerable<PatientMeds>> GetAllPatientMedications()
        {
            return await _patientRepository.GetAllPatientMedications();
        }

        public async Task<List<PatientExpense>> CalculatePatientExpense(IEnumerable<PatientMeds> patientMeds)
        {
            List<PatientExpense> patientExpenses = new List<PatientExpense>();
            if (patientMeds != null)
            {
                foreach (PatientMeds patientMed in patientMeds)
                {
                    short totalDays = (short)(DateTime.Now - patientMed.StartDate).TotalDays;
                    decimal revisionCost = totalDays / patientMed.DaysSupply;

                    decimal cost = revisionCost * patientMed.Cost;

                    PatientExpense? patientExpense = patientExpenses.Find(patient => patient.PatientId == patientMed.PatientId);
                    if (patientExpense != null)
                    {
                        patientExpense.Expense = patientExpense.Expense + cost;
                    }
                    else
                    {
                        patientExpenses.Add(new PatientExpense() { PatientId = patientMed.PatientId, Expense = cost });
                    }
                }
            }
            return patientExpenses;
        }
    }
}
