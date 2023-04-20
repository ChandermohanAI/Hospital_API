namespace Hospital_API.Model
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Disease { get; set; }
        public int Bill { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }


    }
}
