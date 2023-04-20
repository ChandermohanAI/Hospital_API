namespace Hospital_API.Model
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Speciality { get; set; }
        public int Experience { get; set; }
        public double Price { get; set; }
        public IEnumerable<Patient> Patients { get; set; }

    }
}
