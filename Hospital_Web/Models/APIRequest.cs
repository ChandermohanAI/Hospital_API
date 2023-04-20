using Hospital_Utility;
using Microsoft.AspNetCore.Mvc;
using static Hospital_Utility.SD;

namespace Hospital_Web.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
