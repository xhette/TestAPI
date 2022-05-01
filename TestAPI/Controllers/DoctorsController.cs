using Microsoft.AspNetCore.Mvc;

using TestAPI.Data.Entities;
using TestAPI.Data.Services;
using TestAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorDbService _doctorDbService;

        public DoctorsController (IConfiguration configuration)
        {
            _doctorDbService = new DoctorDbService(configuration.GetConnectionString("DefaultDbConnection"));
        }

        // GET: api/<DoctorsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorModel>>> GetAsync()
        {
            try
            {
                var dbServiceResult = await _doctorDbService.GetAllAsync();

                if (dbServiceResult == null)
                {
                    return NotFound();
                }

                var values = dbServiceResult.Select(x => (DoctorModel)x).ToList();

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<DoctorsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorUpdateModel>> Get(int id)
        {
            try
            {
                var dbServiceResult = await _doctorDbService.GetByIdAsync(id);

                if (dbServiceResult == null)
                {
                    return NotFound();
                }

                var value = (DoctorUpdateModel)dbServiceResult;

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST api/<DoctorsController>
        [HttpPost]
        public async Task<ActionResult<DoctorUpdateModel>> Post(DoctorUpdateModel updateModel)
        {
            if (updateModel == null)
            {
                return BadRequest();
            }

            try
            {
                await _doctorDbService.AddAsync((Doctor)updateModel);

                return Ok(updateModel);
            }
            catch (Exception ex)
            {
                string message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;

                return BadRequest(message);
            }
        }

        // PUT api/<DoctorsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorUpdateModel>> Put(int id, [FromBody]DoctorUpdateModel updateModel)
        {
            if (updateModel == null)
            {
                return BadRequest();
            }

            try
            {
                await _doctorDbService.UpdateAsync((Doctor)updateModel);

                return Ok(updateModel);
            }
            catch (Exception ex)
            {
                string message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;

                return BadRequest(message);
            }
        }

        // DELETE api/<DoctorsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _doctorDbService.DeleteAsync(id);
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
