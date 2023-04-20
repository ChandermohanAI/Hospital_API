using Hospital_API.Model;
using System.Linq.Expressions;

namespace Hospital_API.Repository.IRepository
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatientAsync(Expression<Func<Patient, bool>> filter = null);
        Task<Patient> GetPatientAsync(Expression<Func<Patient, bool>>? filter = null);
        Task CreatePatientAsync(Patient entity);
        Task RemovePatientAsync(Patient entity);
        Task SavePatientAsync();
		Task<Patient> UpdateAsync(Patient entity);

	}
}
