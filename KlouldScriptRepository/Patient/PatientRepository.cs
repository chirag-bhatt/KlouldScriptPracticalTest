using CsvHelper;
using KlouldScriptCore.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KlouldScriptRepository.Patient
{
    public class PatientRepository : IPatientRepository
    {
        public async Task<IEnumerable<PatientMeds>> GetAllPatientMedications()
        {
            IEnumerable<PatientMeds> patientMeds = Enumerable.Empty<PatientMeds>();
            using (var reader = new StreamReader($"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}/CSVData/PatientMeds.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    patientMeds = await csv.GetRecordsAsync<PatientMeds>().ToListAsync();
                }
            }
            return patientMeds;
        }
    }
}
