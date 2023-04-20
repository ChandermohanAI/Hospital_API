using Hospital_API.Model;
using System.Linq.Expressions;

namespace Hospital_API.Repository.IRepository
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllDoctorAsync(Expression<Func<Doctor, bool>> filter = null);
        Task<Doctor> GetDoctorAsync(Expression<Func<Doctor, bool>>? filter = null);
        Task CreateDoctorAsync(Doctor entity);
        Task RemoveDoctorAsync(Doctor entity);
        Task SaveDoctorAsync();
        Task<Doctor> UpdateAsync(Doctor entity);

	}
}
