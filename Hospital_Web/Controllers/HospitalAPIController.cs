using AutoMapper;
using Hospital_Web.Models;
using Hospital_Web.Models.DTO.Doctor;
using Hospital_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hospital_Web.Controllers
{
    public class HospitalAPIController : Controller
    {
        private readonly IHospitalService _hospitalService;
        private readonly IMapper _mapper;

        public HospitalAPIController(IHospitalService hospitalService, IMapper mapper)
        {
            _hospitalService = hospitalService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexHospital()
        {
            List<Doctor_DTO> list = new();
            var response = await _hospitalService.GetAllDoctorAsync<APIResponse>();
            if (response != null && response.IsSuccess) 
            {
                list = JsonConvert.DeserializeObject<List<Doctor_DTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}
