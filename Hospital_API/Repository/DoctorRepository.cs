using Hospital_API.Data;
using Hospital_API.Model;
using Hospital_API.Model.DTO.Doctor;
using Hospital_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hospital_API.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _db;

        // dependency injection for Database
        public DoctorRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        // This method will return the list of all doctors
        public async Task<List<Doctor>> GetAllDoctorAsync(Expression<Func<Doctor, bool>> filter = null)
        {
            IQueryable<Doctor> query = _db.Doctors;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

		// This method will return an entry in doctors database 
		public async Task<Doctor> GetDoctorAsync(Expression<Func<Doctor, bool>>? filter = null)
        {
            IQueryable<Doctor> query = _db.Doctors;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

		// This method will Create an entry in doctors database 
		public async Task CreateDoctorAsync(Doctor entity)
        {
            await _db.Doctors.AddAsync(entity);
            await SaveDoctorAsync();
        }

		// This method will Remove an entry in doctors database 
		public async Task RemoveDoctorAsync(Doctor entity)
        {
            _db.Doctors.Remove(entity);
            await SaveDoctorAsync();
        }

		// This method will update changes in doctors database 
		public async Task SaveDoctorAsync()
        {
            await _db.SaveChangesAsync();
        }

		public async Task<Doctor> UpdateAsync(Doctor entity)
		{
			
			_db.Doctors.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}

	}
}
