using AutoMapper;
using Hospital_API.Model.DTO.Doctor;
using Hospital_API.Model;
using Hospital_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Hospital_API.Model.DTO.Patient;

namespace Hospital_API.Controllers
{
    [Route("api/PatientAPI")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly IPatientRepository _db;
        private readonly IDoctorRepository _dbdoc;
        private readonly IMapper _mapper;

		//dependency injection for Patientrepository and Automapper
		public PatientController(IPatientRepository db, IMapper mapper,IDoctorRepository dbdoc)
        {
            _db = db;
            _mapper = mapper;
            _dbdoc = dbdoc;
        }

		// Patient Get EndPoint To Retrieve all Patients
		// Endpoint With No Input 
		[HttpGet("Patients", Name = "Patients")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IEnumerable<Patient_DTO>>> GetPatient()
        {
            IEnumerable<Patient> Patientlist = await _db.GetAllPatientAsync();
            return Ok(Patientlist);
        }


		// Patient Get EndPoint To Create Patients
		// Endpoint With Patient_DTO type as Input 
		[HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Patient_DTO>> CreateDoctor([FromBody] Patient_DTO dto)
        {
            if (dto == null)
            {
                ModelState.AddModelError("Custom Error", "Empty Error");
                return BadRequest(ModelState);
            }
            if (dto.PatientId > 0)
            {
                ModelState.AddModelError("Custom Error", "Id is not Zero");
                return BadRequest(ModelState);
            }
            var docExist = await _dbdoc.GetDoctorAsync(u => u.DoctorId == dto.DoctorId);
            if(docExist==null)
            {
                ModelState.AddModelError("Custom Error", "Doctor Does Not Esist With The Given Id");
                return BadRequest(ModelState);

            }

            else if (await _db.GetPatientAsync(u => u.Name.ToLower() == dto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Custom Error", "Patient Already Exist with Same Name");
                return BadRequest(ModelState);
            }

            else if(docExist!=null && await _db.GetPatientAsync(u => u.Name.ToLower() == dto.Name.ToLower()) == null)
            {
                Patient model = _mapper.Map<Patient>(dto);
                await _db.CreatePatientAsync(model);
                return Ok();
            }
            return Ok();
            
        }

		[HttpPut("{id:int}", Name = "UpdatePatient")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Patient_DTO>> UpdatePatient(int id, [FromBody] PatientUpdate_DTO updateDTO)
		{

			if (updateDTO == null)
			{
				ModelState.AddModelError("Custom Error", "Empty Input");
				return BadRequest(ModelState);
			}
			if (id != updateDTO.PatientId)
			{
				ModelState.AddModelError("Custom Error", "Input Id is not Same");
				return BadRequest(ModelState);
			}
			
			
			Patient model = _mapper.Map<Patient>(updateDTO);
			await _db.UpdateAsync(model);
			return Ok(model);
		}



	}
}
