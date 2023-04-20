using Hospital_Utility;
using Hospital_Web.Models;
using Hospital_Web.Models.DTO.Doctor;
using Hospital_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Web.Services
{
    public class HospitalService : BaseService, IHospitalService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string hospitalUrl;
        public HospitalService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            hospitalUrl = configuration.GetValue<string>("Serviceurls:Hospital_API");
        }
        public Task<T> CreateDoctorAsync<T>(Doctor_DTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = hospitalUrl + "api/HospitalAPI"

            });
        }

        public Task<T> DeleteDoctorAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = hospitalUrl + "api/HospitalAPI/"+id

            });
        }

        public Task<T> GetAllDoctorAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                
                Url = hospitalUrl + "api/HospitalAPI"

            });
        }

        public Task<T> GetDoctorAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                
                Url = hospitalUrl + "api/HospitalAPI/"+id

            });
        }
    }
}
