using AutoMapper;
using Azure;
using Hospital_API.Data;
using Hospital_API.Model;
using Hospital_API.Model.DTO;
using Hospital_API.Model.DTO.Doctor;
using Hospital_API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace Hospital_API.Controllers
{
    [Route("api/HospitalAPI")]
    [ApiController]
    public class HospitalController : Controller
    {
        private readonly IDoctorRepository _db;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        //dependency injection for Doctorrepository and Automapper
        public HospitalController(IDoctorRepository db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            this._response = new();
        }

        // Doctor Get EndPoint To Retrieve all Doctors
        // Endpoint With No Input 
        [HttpGet("Doctors", Name ="Doctors")]
        public async Task<ActionResult<APIResponse>> GetDoctor()
        {
			try
			{
				IEnumerable<Doctor> doctorlist = await _db.GetAllDoctorAsync();
				_response.Result= _mapper.Map<List<Doctor>>(doctorlist);
				_response.StatusCode = HttpStatusCode.OK;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return _response;

		}

		// Doctor Get EndPoint To Retrieve Doctor Using Id as Input
		// Endpoint With Id as Input 
		[HttpGet("Doctors/id", Name = "Doctor")]
        //[Authorize(Roles="Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		
		public async Task <ActionResult<APIResponse>> GetDoctor(int id)
        {
            try
            {
				if (id == 0)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					ModelState.AddModelError("Custom Error","Id is Empty");
					return BadRequest(ModelState);
				}
				var doc = await _db.GetDoctorAsync(u => u.DoctorId == id);
				if (doc == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					ModelState.AddModelError("Custom Error", "Id does not Exist");
					return NotFound(_response);
				}
				_response.Result = _mapper.Map<Doctor>(doc);
				_response.IsSuccess = true;
				_response.StatusCode = HttpStatusCode.OK;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;


		}


		// Doctor Delete EndPoint To Delete Doctor
		// Endpoint With Id as Input 
		[HttpDelete("Doctors/id", Name = "Delete Doctor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteDoctor(int id)
        {
			try
			{
				if (id == 0)
				{
					ModelState.AddModelError("Custom Error", "Empty Id");
					return BadRequest(ModelState);
				}

				var doc = await _db.GetDoctorAsync(u => u.DoctorId == id);
				if (doc == null)
				{
					ModelState.AddModelError("Custom Error", "Id Does not Exist");
					return Ok(ModelState);
				}
				await _db.RemoveDoctorAsync(doc);
				_response.StatusCode = HttpStatusCode.NoContent;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;


		}

		// Doctor Get EndPoint To Retrieve all Doctors in Ascending Order
		// Endpoint With No Input 
		[HttpGet("Sort Doctor",Name = "Asc Doctor")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public  async Task<ActionResult<Doctor_DTO>> AscDoctor()
        {
            var doctorlist = await _db.GetAllDoctorAsync();
            var doc = doctorlist.OrderBy(d => d.Name);
            return Ok(doc);

        }

		// Doctor Post EndPoint To Create Doctors
		// Endpoint With Doctor_DTO type as Input 
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult <APIResponse>> CreateDoctor([FromBody] Doctor_DTO dto)
        {
			try
			{
				if (dto == null)
				{
					ModelState.AddModelError("Custom Error", "Empty Error");
					return BadRequest(ModelState);
				}
				if (dto.DoctorId > 0)
				{
					ModelState.AddModelError("Custom Error", "Id is not Zero");
					return BadRequest(ModelState);
				}
				else if (await _db.GetDoctorAsync(u => u.Name.ToLower() == dto.Name.ToLower()) != null)
				{
					ModelState.AddModelError("Custom Error", "Doctor Already Exist with Same Name");
					return BadRequest(ModelState);
				}

				Doctor model = _mapper.Map<Doctor>(dto);
				await _db.CreateDoctorAsync(model);

				_response.Result = _mapper.Map<Doctor_DTO>(model);
				_response.StatusCode = HttpStatusCode.Created;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;


		}

		[HttpPut("{id:int}", Name = "UpdateDoctorr")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> UpdateDoctor(int id, [FromBody] DoctorUpdate_DTO updateDTO)
		{
			try
			{
				if (updateDTO == null)
				{
					ModelState.AddModelError("Custom Error", "Empty Input");
					return BadRequest(ModelState);
				}

				if (id != updateDTO.DoctorId)
				{
					ModelState.AddModelError("Custom Error", "Input Id is not Same");
					return BadRequest(ModelState);
				}
				Doctor model = _mapper.Map<Doctor>(updateDTO);

				await _db.UpdateAsync(model);
				_response.StatusCode = HttpStatusCode.OK;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}


	}
}
