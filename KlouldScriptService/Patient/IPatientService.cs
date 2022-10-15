using KlouldScriptCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlouldScriptService.Patient
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientMeds>> GetAllPatientMedications();
        Task<List<PatientExpense>> CalculatePatientExpense(IEnumerable<PatientMeds> patientMeds);
    }
}
