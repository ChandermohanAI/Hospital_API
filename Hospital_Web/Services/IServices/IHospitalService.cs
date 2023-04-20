using Hospital_Web.Models.DTO.Doctor;

namespace Hospital_Web.Services.IServices
{
    public interface IHospitalService
    {
        Task<T> GetAllDoctorAsync<T>();
        Task<T> GetDoctorAsync<T>(int id);
        Task<T> CreateDoctorAsync<T>(Doctor_DTO dto);
        //Task<T> UpdateDoctorAsync<T>(Doctor_DTO dto);
        Task<T> DeleteDoctorAsync<T>(int id);
    }
}
