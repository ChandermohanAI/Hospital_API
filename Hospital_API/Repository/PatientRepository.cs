using Hospital_API.Data;
using Hospital_API.Model;
using Hospital_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace Hospital_API.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _db;

        // dependency Injection of Database
        public PatientRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        // this method will Return List of all the patient in the table
        public async Task<List<Patient>> GetAllPatientAsync(Expression<Func<Patient, bool>> filter = null)
        {
            IQueryable<Patient> query = _db.Patients;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

		// this method will Return a entry of patient in the table based on the query
		public async Task<Patient> GetPatientAsync(Expression<Func<Patient, bool>>? filter = null)
        {
            IQueryable<Patient> query = _db.Patients;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        //This method is responsible for the new patient entry creation
        public async Task CreatePatientAsync(Patient entity)
        {
            await _db.Patients.AddAsync(entity);
            await SavePatientAsync();
        }

        // This method deletes the existing user in the databse
        public async Task RemovePatientAsync(Patient entity)
        {
            _db.Patients.Remove(entity);
            await SavePatientAsync();
        }

        // This method Updates the patient database Table after any change
        public async Task SavePatientAsync()
        {
            await _db.SaveChangesAsync();
        }

		public async Task<Patient> UpdateAsync(Patient entity)
		{

			_db.Patients.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}

	}


}

