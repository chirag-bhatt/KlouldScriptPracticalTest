using KlouldScriptCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlouldScriptRepository.Patient
{
    public interface IPatientRepository
    {
        Task<IEnumerable<PatientMeds>> GetAllPatientMedications();
    }
}
