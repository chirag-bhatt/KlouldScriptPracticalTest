using CsvHelper;
using KlouldScriptCore.Models;
using KlouldScriptService.Patient;
using KlouldScriptService.Plan;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.IO;
using System.Net.Mime;

namespace KlouldScriptPracticalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private readonly IPatientService _patientService;
        private readonly IPlanService _planservice;

        public PatientController(ILogger<PatientController> logger, IPatientService patientService,
            IPlanService planservice)
        {
            this._logger = logger;
            this._patientService = patientService;
            this._planservice = planservice;
        }

        [HttpGet(Name = "GetOptimalInsurancePlans")]
        public async Task<ActionResult> GetOptimalInsurancePlans()
        {
            #region Get Patient Medications
            IEnumerable<PatientMeds> patientMeds = Enumerable.Empty<PatientMeds>();
            try
            {
                _logger.LogDebug("Start getting patient medication file");
                patientMeds = await _patientService.GetAllPatientMedications();
                _logger.LogDebug($"Patient medications no of record found : {patientMeds.Count()}");
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                _logger.LogError("Patient medication file not found", fileNotFoundException);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error While get Patient medications", ex);
                throw;
            }
            #endregion

            #region Get Insurance Plans
            IEnumerable<InsurancePlan> insurancePlans = Enumerable.Empty<InsurancePlan>();
            try
            {
                _logger.LogDebug("Start getting insurance plan file");
                insurancePlans = await _planservice.GetAllInsurancePlans();
                _logger.LogDebug($"No of Insurance plan found {insurancePlans.Count()}");
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                _logger.LogError("File not found for Insurance Plans", fileNotFoundException);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while getting Insurance plans", ex);
                throw;
            }
            #endregion

            #region Calculate Patient expenses

            _logger.LogDebug("Start calculating patient Expenses");
            List<PatientExpense> patientExpenses = await _patientService.CalculatePatientExpense(patientMeds);
            _logger.LogDebug("Patient expenses Calculated");

            #endregion


            #region Calculate Patient Optimal Insurance plan
            _logger.LogDebug("Start calculating patient Optimal Insurance plan");
            IEnumerable<PatientOptimalInsurancePlan> patientOptimalInsurancePlans = await _planservice.CalculatePatientOptimalInsurancePlan(patientExpenses,
                insurancePlans);


            _logger.LogDebug("Patient Optimal insurance plan Calculated");
            #endregion


            #region Write Generated File

            MemoryStream memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords<PatientOptimalInsurancePlan>(patientOptimalInsurancePlans);
                }
            }
            return File(memoryStream.ToArray(), "text/csv", "PatientOptimalInsurancePlan.csv");


            #endregion
        }
    }
}
