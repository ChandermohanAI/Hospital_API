using AutoMapper;
using Hospital_API.Model;
using Hospital_API.Model.DTO.Doctor;
using Hospital_API.Model.DTO.Patient;

namespace Hospital_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Doctor, Doctor_DTO>();
            CreateMap<Doctor_DTO,Doctor>();
            CreateMap<Doctor, DoctorUpdate_DTO>().ReverseMap();
			CreateMap<Patient_DTO, Patient>();
            CreateMap<Patient, Patient_DTO>();
			CreateMap<Patient, PatientUpdate_DTO>().ReverseMap();


		}
        
    }
}
