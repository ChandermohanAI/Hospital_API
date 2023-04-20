using Hospital_Web.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Hospital_Web.Services.IServices
{
    public interface IBaseService
    {
        ApiResponseFormat responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest); 
    }
}
