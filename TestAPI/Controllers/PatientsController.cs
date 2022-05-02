using Microsoft.AspNetCore.Mvc;

using TestAPI.Data.Entities;
using TestAPI.Data.Services;
using TestAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientDbService _patientDbService;

        public PatientsController(IConfiguration configuration)
        {
            _patientDbService = new PatientDbService(configuration.GetConnectionString("DefaultDbConnection"));
        }

        // GET: api/<PatientsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientModel>>> GetAsync()
        {
            try
            {
                var dbServiceResult = await _patientDbService.GetAllAsync();

                if (dbServiceResult == null)
                {
                    return NotFound();
                }

                var values = dbServiceResult.Select(x => (PatientModel)x).ToList();

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientModel>>> GetAsync(int from, int perPage, string fieldName)
        {
            try
            {
                var dbServiceResult = await _patientDbService.GetAllAsync(from, perPage, fieldName);

                if (dbServiceResult == null)
                {
                    return NotFound();
                }

                var values = dbServiceResult.Select(x => (PatientModel)x).ToList();

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<PatientsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientUpdateModel>> Get(int id)
        {
            try
            {
                var dbServiceResult = await _patientDbService.GetByIdAsync(id);

                if (dbServiceResult == null)
                {
                    return NotFound();
                }

                var value = (PatientUpdateModel)dbServiceResult;

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST api/<PatientsController>
        [HttpPost]
        public async Task<ActionResult<PatientUpdateModel>> Post(PatientUpdateModel updateModel)
        {
            if (updateModel == null)
            {
                return BadRequest();
            }

            try
            {
                await _patientDbService.AddAsync((Patient)updateModel);

                return Ok(updateModel);
            }
            catch (Exception ex)
            {
                string message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;

                return BadRequest(message);
            }
        }

        // PUT api/<PatientsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorUpdateModel>> Put(int id, [FromBody]PatientUpdateModel updateModel)
        {
            if (updateModel == null)
            {
                return BadRequest();
            }

            try
            {
                await _patientDbService.UpdateAsync((Patient)updateModel);

                return Ok(updateModel);
            }
            catch (Exception ex)
            {
                string message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;

                return BadRequest(message);
            }
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _patientDbService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                return BadRequest(message);
            }
        }
    }
}
