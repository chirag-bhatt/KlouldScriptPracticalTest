using CsvHelper;
using KlouldScriptCore.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace KlouldScriptRepository.Plan
{
    public class PlanRepository : IPlanRepository
    {
        public async Task<IEnumerable<InsurancePlan>> GetAllInsurancePlans()
        {
            IEnumerable<InsurancePlan> insurancePlans = Enumerable.Empty<InsurancePlan>();
            using (var reader = new StreamReader($"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}/CSVData/InsurancePlans.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    insurancePlans = await csv.GetRecordsAsync<InsurancePlan>().ToListAsync();
                }
            }
            return insurancePlans;
        }

        public async Task InsertAllInsurancePlans(IEnumerable<PatientOptimalInsurancePlan> patientOptimalInsurancePlans)
        {
            using (var writer = new StreamWriter($"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}/CSVData/PatientOptimalInsurancePlans.csv", false, Encoding.UTF8))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    await csv.WriteRecordsAsync(patientOptimalInsurancePlans);
                }
            }
        }
    }
}